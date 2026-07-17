package main

import (
	"crypto/md5"
	"encoding/hex"
	"encoding/json"
	"fmt"
	"net"
	"os"
	"sort"
	"strings"
	"sync"
	"time"
)

const liveDir = "./data/live/"

// blackmarket

type bmTier struct {
	ID         string   `json:"id"`
	Kind       string   `json:"kind"`
	PriceMin   int      `json:"priceMin"`
	PriceMax   int      `json:"priceMax"`
	Price      int      `json:"price"`
	Candidates []string `json:"candidates"`
}

type blackmarketConfig struct {
	Acid                string   `json:"acid"`
	OwnedLicenses       []string `json:"ownedLicenses"`
	HaveItemsDialog     []string `json:"haveItemsDialog"`
	NoItemsDialog       []string `json:"noItemsDialog"`
	StillDecidingDialog []string `json:"stillDecidingDialog"`
	RotationBaseUnix    int64    `json:"rotationBaseUnix"`
	RotationPeriodSecs  int64    `json:"rotationPeriodSecs"`
	Slots               int      `json:"slots"`
	PriceRoundTo        int      `json:"priceRoundTo"`
	Tiers               []bmTier `json:"tiers"`
}

type bmSlot struct {
	Kind   string `json:"kind"`
	ItemID string `json:"itemId"`
	Price  int    `json:"price"`
	Slid   string `json:"slid"`
	Name   string `json:"name"`
	Desc   string `json:"desc"`
}

type blackmarketResponse struct {
	Acid                string   `json:"acid"`
	OwnedLicenses       []string `json:"ownedLicenses"`
	HaveItemsDialog     []string `json:"haveItemsDialog"`
	NoItemsDialog       []string `json:"noItemsDialog"`
	StillDecidingDialog []string `json:"stillDecidingDialog"`
	RotationIndex       int64    `json:"rotationIndex"`
	ActivationUnix      int64    `json:"activationUnix"`
	StateChangeTimeUnix int64    `json:"stateChangeTimeUnix"`
	Slots               []bmSlot `json:"slots"`
}

func computeBlackmarket(now int64) (blackmarketResponse, error) {
	var cfg blackmarketConfig
	if err := readLive("blackmarket.json", &cfg); err != nil {
		return blackmarketResponse{}, err
	}

	index := rotationIndex(cfg.RotationBaseUnix, cfg.RotationPeriodSecs, now)
	activationUnix := cfg.RotationBaseUnix + index*cfg.RotationPeriodSecs
	stateChangeTimeUnix := activationUnix + cfg.RotationPeriodSecs

	tiers := make([]bmTier, len(cfg.Tiers))
	copy(tiers, cfg.Tiers)

	orderRng := mulberry32(uint32(index) * 2654435761)
	for i := len(tiers) - 1; i > 0; i-- {
		j := int(orderRng() * float64(i+1))
		tiers[i], tiers[j] = tiers[j], tiers[i]
	}

	count := cfg.Slots
	if count > len(tiers) {
		count = len(tiers)
	}
	round := cfg.PriceRoundTo
	if round <= 0 {
		round = 1
	}

	slots := make([]bmSlot, 0, count)
	for i := 0; i < count; i++ {
		tier := tiers[i]
		rng := mulberry32(uint32(index) ^ hashStr(tier.ID))
		itemID := tier.Candidates[int(rng()*float64(len(tier.Candidates)))]

		price := tier.Price
		if price <= 0 {
			span := (tier.PriceMax-tier.PriceMin)/round + 1
			price = tier.PriceMin + int(rng()*float64(span))*round
		}
		slots = append(slots, bmSlot{Kind: tier.Kind, ItemID: itemID, Price: price})
	}

	sort.SliceStable(slots, func(a, b int) bool { return slots[a].Price > slots[b].Price })
	for i := range slots {
		slots[i].Slid = fmt.Sprintf("z7bm%d", i+1)
		slots[i].Name = ""
		slots[i].Desc = ""
	}

	return blackmarketResponse{
		Acid:                cfg.Acid,
		OwnedLicenses:       cfg.OwnedLicenses,
		HaveItemsDialog:     cfg.HaveItemsDialog,
		NoItemsDialog:       cfg.NoItemsDialog,
		StillDecidingDialog: cfg.StillDecidingDialog,
		RotationIndex:       index,
		ActivationUnix:      activationUnix,
		StateChangeTimeUnix: stateChangeTimeUnix,
		Slots:               slots,
	}, nil
}

// daily quests

type dailyQuestsConfig struct {
	RotationBaseUnix   int64    `json:"rotationBaseUnix"`
	RotationPeriodSecs int64    `json:"rotationPeriodSecs"`
	PickPerCategory    int      `json:"pickPerCategory"`
	Multiplayer        []string `json:"multiplayer"`
	Plant              []string `json:"plant"`
	Zombie             []string `json:"zombie"`
}

type dailyQuestsResponse struct {
	Quests         []string `json:"quests"`
	ActivationUnix int64    `json:"activationUnix"`
	ExpiryUnix     int64    `json:"expiryUnix"`
	RotationIndex  int64    `json:"rotationIndex"`
}

func computeDailyQuests(now int64) (dailyQuestsResponse, error) {
	var cfg dailyQuestsConfig
	if err := readLive("dailyquests.json", &cfg); err != nil {
		return dailyQuestsResponse{}, err
	}

	index := rotationIndex(cfg.RotationBaseUnix, cfg.RotationPeriodSecs, now)
	activationUnix := cfg.RotationBaseUnix + index*cfg.RotationPeriodSecs
	expiryUnix := activationUnix + cfg.RotationPeriodSecs

	pick := func(pool []string, category string) []string {
		arr := make([]string, len(pool))
		copy(arr, pool)
		rng := mulberry32(uint32(index) ^ hashStr(category))
		for i := len(arr) - 1; i > 0; i-- {
			j := int(rng() * float64(i+1))
			arr[i], arr[j] = arr[j], arr[i]
		}
		n := cfg.PickPerCategory
		if n > len(arr) {
			n = len(arr)
		}
		return arr[:n]
	}

	quests := []string{}
	quests = append(quests, pick(cfg.Multiplayer, "multiplayer")...)
	quests = append(quests, pick(cfg.Plant, "plant")...)
	quests = append(quests, pick(cfg.Zombie, "zombie")...)

	return dailyQuestsResponse{
		Quests:         quests,
		ActivationUnix: activationUnix,
		ExpiryUnix:     expiryUnix,
		RotationIndex:  index,
	}, nil
}

// community event

type communityEventConfig struct {
	Featured           bool   `json:"featured"`
	EventID            string `json:"eventId"`
	Name               string `json:"name"`
	Description        string `json:"description"`
	ImageURL           string `json:"imageUrl"`
	FirstChestScore    int    `json:"firstChestScore"`
	SecondChestScore   int    `json:"secondChestScore"`
	ThirdChestScore    int    `json:"thirdChestScore"`
	ChestReward        string `json:"chestReward"`
	EventReward        string `json:"eventReward"`
	RotationBaseUnix   int64  `json:"rotationBaseUnix"`
	RotationPeriodSecs int64  `json:"rotationPeriodSecs"`
}

type communityEventResponse struct {
	Featured          bool   `json:"featured"`
	EventID           string `json:"eventId"`
	Name              string `json:"name"`
	Description       string `json:"description"`
	ImageURL          string `json:"imageUrl"`
	StartUnix         int64  `json:"startUnix"`
	EndUnix           int64  `json:"endUnix"`
	GrandPrizeEndUnix int64  `json:"grandPrizeEndUnix"`
	FirstChestScore   int    `json:"firstChestScore"`
	SecondChestScore  int    `json:"secondChestScore"`
	ThirdChestScore   int    `json:"thirdChestScore"`
	ChestReward       string `json:"chestReward"`
	EventReward       string `json:"eventReward"`
	RotationIndex     int64  `json:"rotationIndex"`
}

func computeCommunityEvent(now int64) (communityEventResponse, error) {
	var cfg communityEventConfig
	if err := readLive("communityevent.json", &cfg); err != nil {
		return communityEventResponse{}, err
	}

	index := rotationIndex(cfg.RotationBaseUnix, cfg.RotationPeriodSecs, now)
	startUnix := cfg.RotationBaseUnix + index*cfg.RotationPeriodSecs
	endUnix := startUnix + cfg.RotationPeriodSecs

	return communityEventResponse{
		Featured:          cfg.Featured,
		EventID:           cfg.EventID,
		Name:              cfg.Name,
		Description:       cfg.Description,
		ImageURL:          cfg.ImageURL,
		StartUnix:         startUnix,
		EndUnix:           endUnix,
		GrandPrizeEndUnix: endUnix,
		FirstChestScore:   cfg.FirstChestScore,
		SecondChestScore:  cfg.SecondChestScore,
		ThirdChestScore:   cfg.ThirdChestScore,
		ChestReward:       cfg.ChestReward,
		EventReward:       cfg.EventReward,
		RotationIndex:     index,
	}, nil
}

// community challenge

type communityChallengeConfig struct {
	AchievementID            string `json:"achievementId"`
	Name                     string `json:"name"`
	Desc                     string `json:"desc"`
	Image                    string `json:"image"`
	PersonalHeader           string `json:"personalHeader"`
	RewardHeader             string `json:"rewardHeader"`
	BronzeThreshold          int64  `json:"bronzeThreshold"`
	SilverThreshold          int64  `json:"silverThreshold"`
	GoldThreshold            int64  `json:"goldThreshold"`
	BronzeReward             string `json:"bronzeReward"`
	SilverReward             string `json:"silverReward"`
	GoldReward               string `json:"goldReward"`
	UserThreshold            int64  `json:"userThreshold"`
	RefreshRateSeconds       int    `json:"refreshRateSeconds"`
	ProximityCooldownSeconds int    `json:"proximityCooldownSeconds"`
	RotationBaseUnix         int64  `json:"rotationBaseUnix"`
	RotationPeriodSecs       int64  `json:"rotationPeriodSecs"`
	Tracks                   struct {
		Type string   `json:"type"`
		Keys []string `json:"keys"`
	} `json:"tracks"`
}

type challengeState struct {
	Window    int64            `json:"window"`
	Community int64            `json:"community"`
	Users     map[string]int64 `json:"users"`
}

var challengeMu sync.Mutex

func readChallengeState() challengeState {
	var s challengeState
	if err := readLive("challenge_state.json", &s); err != nil {
		return challengeState{Window: -1, Community: 0, Users: map[string]int64{}}
	}
	if s.Users == nil {
		s.Users = map[string]int64{}
	}
	return s
}

func writeChallengeState(s challengeState) {
	out, _ := json.MarshalIndent(s, "", " ")
	os.WriteFile(liveDir+"challenge_state.json", out, 0644)
}

type communityChallengeResponse struct {
	AchievementID             string `json:"achievementId"`
	Name                      string `json:"name"`
	Desc                      string `json:"desc"`
	Image                     string `json:"image"`
	PersonalHeader            string `json:"personalHeader"`
	RewardHeader              string `json:"rewardHeader"`
	ChallengeActive           int    `json:"challengeActive"`
	ContributionActive        int    `json:"contributionActive"`
	CommunityProgress         int64  `json:"communityProgress"`
	BronzeThreshold           int64  `json:"bronzeThreshold"`
	SilverThreshold           int64  `json:"silverThreshold"`
	GoldThreshold             int64  `json:"goldThreshold"`
	BronzeReward              string `json:"bronzeReward"`
	SilverReward              string `json:"silverReward"`
	GoldReward                string `json:"goldReward"`
	BronzeCollected           int    `json:"bronzeCollected"`
	SilverCollected           int    `json:"silverCollected"`
	GoldCollected             int    `json:"goldCollected"`
	UserThreshold             int64  `json:"userThreshold"`
	UserProgress              int64  `json:"userProgress"`
	SecondsFromStart          int64  `json:"secondsFromStart"`
	SecondsToNextEvent        int64  `json:"secondsToNextEvent"`
	SecondsToCollectionExpiry int64  `json:"secondsToCollectionExpiry"`
	SecondsToRewardExpiry     int64  `json:"secondsToRewardExpiry"`
	RefreshRateSeconds        int    `json:"refreshRateSeconds"`
	ProximityCooldownSeconds  int    `json:"proximityCooldownSeconds"`
	RotationIndex             int64  `json:"rotationIndex"`
}

func computeCommunityChallenge(now int64, user string) (communityChallengeResponse, error) {
	var c communityChallengeConfig
	if err := readLive("communitychallenge.json", &c); err != nil {
		return communityChallengeResponse{}, err
	}

	index := rotationIndex(c.RotationBaseUnix, c.RotationPeriodSecs, now)
	startUnix := c.RotationBaseUnix + index*c.RotationPeriodSecs
	endUnix := startUnix + c.RotationPeriodSecs
	elapsed := max64(0, now-startUnix)

	challengeMu.Lock()
	state := readChallengeState()
	challengeMu.Unlock()

	var community, userProgress int64
	if state.Window == index {
		community = state.Community
		if user != "" {
			userProgress = state.Users[user]
		}
	}

	return communityChallengeResponse{
		AchievementID:             c.AchievementID,
		Name:                      c.Name,
		Desc:                      c.Desc,
		Image:                     c.Image,
		PersonalHeader:            c.PersonalHeader,
		RewardHeader:              c.RewardHeader,
		ChallengeActive:           1,
		ContributionActive:        1,
		CommunityProgress:         community,
		BronzeThreshold:           c.BronzeThreshold,
		SilverThreshold:           c.SilverThreshold,
		GoldThreshold:             c.GoldThreshold,
		BronzeReward:              c.BronzeReward,
		SilverReward:              c.SilverReward,
		GoldReward:                c.GoldReward,
		UserThreshold:             c.UserThreshold,
		UserProgress:              userProgress,
		SecondsFromStart:          elapsed,
		SecondsToNextEvent:        max64(0, endUnix-now),
		SecondsToCollectionExpiry: max64(0, endUnix-now),
		SecondsToRewardExpiry:     max64(0, endUnix-now),
		RefreshRateSeconds:        c.RefreshRateSeconds,
		ProximityCooldownSeconds:  c.ProximityCooldownSeconds,
		RotationIndex:             index,
	}, nil
}

type contributeResponse struct {
	Matched   int64 `json:"matched"`
	Community int64 `json:"community"`
	Window    int64 `json:"window"`
}

func contributeChallenge(body map[string]any, playerId string, now int64) (contributeResponse, error) {
	var c communityChallengeConfig
	if err := readLive("communitychallenge.json", &c); err != nil {
		return contributeResponse{}, err
	}
	index := rotationIndex(c.RotationBaseUnix, c.RotationPeriodSecs, now)

	challengeMu.Lock()
	defer challengeMu.Unlock()

	state := readChallengeState()
	if state.Window != index {
		state = challengeState{Window: index, Community: 0, Users: map[string]int64{}}
	}

	bucket, _ := body[c.Tracks.Type].(map[string]any)
	var matched int64
	for _, k := range c.Tracks.Keys {
		matched += toInt64(bucket[k])
	}

	if matched > 0 {
		state.Community += matched
		uid := playerId
		if uid == "" {
			uid = fmt.Sprintf("%v", body["user"])
		}
		state.Users[uid] += matched
		writeChallengeState(state)
	}

	return contributeResponse{Matched: matched, Community: state.Community, Window: index}, nil
}

// leaderboards

const noTime = 1000000.0

type lbEntry struct {
	Value     float64 `json:"value"`
	Character int     `json:"character"`
	Name      string  `json:"name"`
	Blaze     string  `json:"blaze,omitempty"`
	Order     string  `json:"order,omitempty"` // "high" = higher is better (TSHOOT), else lower (GR times)
}

type leaderboardState struct {
	Boards map[string]map[string]*lbEntry `json:"boards"`
}

var leaderboardMu sync.Mutex

func readLeaderboardState() leaderboardState {
	var s leaderboardState
	if err := readLive("leaderboard_state.json", &s); err != nil {
		return leaderboardState{Boards: map[string]map[string]*lbEntry{}}
	}
	if s.Boards == nil {
		s.Boards = map[string]map[string]*lbEntry{}
	}
	return s
}

func writeLeaderboardState(s leaderboardState) {
	out, _ := json.MarshalIndent(s, "", " ")
	os.WriteFile(liveDir+"leaderboard_state.json", out, 0644)
}

type lbSubmitScore struct {
	Value     float64 `json:"value"`
	Character int     `json:"character"`
	Order     string  `json:"order"` // "high" = higher is better; default "" = lower is better
}

type lbSubmitBody struct {
	Scores map[string]lbSubmitScore `json:"scores"`
	User   *json.Number             `json:"user"`
	Name   string                   `json:"name"`
}

type lbSubmitResponse struct {
	OK      bool `json:"ok"`
	Updated int  `json:"updated,omitempty"`
}

func submitLeaderboard(body lbSubmitBody, playerId string) lbSubmitResponse {
	if body.Scores == nil {
		return lbSubmitResponse{OK: false}
	}

	user := playerId
	var blaze string
	if body.User != nil {
		blaze = body.User.String()
		if user == "" {
			user = blaze
		}
	}
	if user == "" {
		user = "0"
	}
	name := body.Name
	if name == "" {
		name = "Player" + lastN(user, 4)
	}

	leaderboardMu.Lock()
	defer leaderboardMu.Unlock()

	state := readLeaderboardState()
	updated := 0
	for board, s := range body.Scores {
		high := s.Order == "high"
		if !high && s.Value >= noTime { // GR "no time" sentinel (only for low boards)
			continue
		}
		if state.Boards[board] == nil {
			state.Boards[board] = map[string]*lbEntry{}
		}
		prev := state.Boards[board][user]
		// "best" = max for high boards (TSHOOT score), min for low boards (GR time).
		better := prev == nil || (high && s.Value > prev.Value) || (!high && s.Value < prev.Value)
		if better {
			state.Boards[board][user] = &lbEntry{Value: s.Value, Character: s.Character, Name: name, Blaze: blaze, Order: s.Order}
			updated++
		} else {
			prev.Name = name
			prev.Order = s.Order
			if blaze != "" {
				prev.Blaze = blaze
			}
		}
	}
	writeLeaderboardState(state)
	return lbSubmitResponse{OK: true, Updated: updated}
}

type lbOutEntry struct {
	User      string  `json:"user"`
	Player    string  `json:"player"`
	Name      string  `json:"name"`
	Value     float64 `json:"value"`
	Character int     `json:"character"`
	Rank      int     `json:"rank"`
}

type leaderboardResponse struct {
	Board   string       `json:"board"`
	Size    int          `json:"size"`
	Entries []lbOutEntry `json:"entries"`
}

func computeLeaderboard(board string, limit int) leaderboardResponse {
	leaderboardMu.Lock()
	state := readLeaderboardState()
	leaderboardMu.Unlock()

	tbl := state.Boards[board]
	high := false
	for _, e := range tbl {
		if e.Order == "high" {
			high = true
		}
		break
	}
	entries := make([]lbOutEntry, 0, len(tbl))
	for player, e := range tbl {
		if !high && e.Value >= noTime {
			continue
		}
		u := e.Blaze
		if u == "" {
			u = player
		}
		entries = append(entries, lbOutEntry{User: u, Player: player, Name: e.Name, Value: e.Value, Character: e.Character})
	}
	// high boards rank descending (higher score first); low boards ascending (lower time first).
	sort.SliceStable(entries, func(a, b int) bool {
		if high {
			return entries[a].Value > entries[b].Value
		}
		return entries[a].Value < entries[b].Value
	})

	if limit < 1 {
		limit = 1
	}
	if len(entries) > limit {
		entries = entries[:limit]
	}
	for i := range entries {
		entries[i].Rank = i + 1
	}
	return leaderboardResponse{Board: board, Size: len(entries), Entries: entries}
}

// helper functions

func max64(a, b int64) int64 {
	if a > b {
		return a
	}
	return b
}

func lastN(s string, n int) string {
	if len(s) <= n {
		return s
	}
	return s[len(s)-n:]
}

func toInt64(v any) int64 {
	switch n := v.(type) {
	case float64:
		return int64(n)
	case json.Number:
		i, _ := n.Int64()
		return i
	}
	return 0
}

func nowUnix() int64 { return time.Now().Unix() }

func rotationIndex(base, period, now int64) int64 {
	if period <= 0 {
		return 0
	}
	return (now - base) / period
}

func mulberry32(seed uint32) func() float64 {
	s := seed
	return func() float64 {
		s = s + 0x6d2b79f5
		h := (s ^ (s >> 15)) * (1 | s)
		h = (h + ((h ^ (h >> 7)) * (61 | h))) ^ h
		return float64(h^(h>>14)) / 4294967296.0
	}
}

func hashStr(str string) uint32 {
	h := uint32(2166136261)
	for i := 0; i < len(str); i++ {
		h ^= uint32(str[i])
		h *= 16777619
	}
	return h
}

func ipHash(ip string) string {
	norm := strings.TrimPrefix(ip, "::ffff:")
	sum := md5.Sum([]byte(norm))
	return hex.EncodeToString(sum[:])
}

func playerID(remoteAddr string) string {
	host, _, err := net.SplitHostPort(remoteAddr)
	if err != nil {
		host = remoteAddr
	}
	return ipHash(host)
}

func readLive(file string, out any) error {
	data, err := os.ReadFile(liveDir + file)
	if err != nil {
		return err
	}
	return json.Unmarshal(data, out)
}
