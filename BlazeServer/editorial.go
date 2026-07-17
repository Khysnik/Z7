package main

import (
	"encoding/json"
	"fmt"
	"net/http"
	"strconv"
)

func writeJSON(res http.ResponseWriter, v any) {
	out, err := json.Marshal(v)
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}
	res.Header().Set("Content-Type", "application/json")
	res.Header().Set("Content-Length", strconv.Itoa(len(out)))
	res.WriteHeader(http.StatusOK)
	res.Write(out)
}

func HandleBlackmarket(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /gw2/live/blackmarket from", req.RemoteAddr)

	out, err := computeBlackmarket(nowUnix())
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(res, out)
}

func HandleDailyquest(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /gw2/live/dailyquests from", req.RemoteAddr)

	out, err := computeDailyQuests(nowUnix())
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(res, out)
}

func HandleCommevent(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /gw2/live/communityevent from", req.RemoteAddr)

	out, err := computeCommunityEvent(nowUnix())
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(res, out)
}

func HandleCommchal(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /gw2/live/communitychallenge from", req.RemoteAddr)

	out, err := computeCommunityChallenge(nowUnix(), playerID(req.RemoteAddr))
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(res, out)
}

func HandlePostCommchal(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodPost {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("POST /gw2/live/challenge/contribute from", req.RemoteAddr)

	body := map[string]any{}
	if req.Body != nil {
		json.NewDecoder(req.Body).Decode(&body)
	}

	out, err := contributeChallenge(body, playerID(req.RemoteAddr), nowUnix())
	if err != nil {
		http.Error(res, err.Error(), http.StatusInternalServerError)
		return
	}

	writeJSON(res, out)
}

func HandleLeaderboard(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /gw2/live/leaderboard from", req.RemoteAddr)

	board := req.URL.Query().Get("board")
	limit := 100
	if v, err := strconv.Atoi(req.URL.Query().Get("limit")); err == nil && v > 0 {
		limit = v
	}

	writeJSON(res, computeLeaderboard(board, limit))
}

func HandlePostLeaderboard(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodPost {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("POST /gw2/live/leaderboard/submit from", req.RemoteAddr)

	var body lbSubmitBody
	if req.Body != nil {
		json.NewDecoder(req.Body).Decode(&body)
	}

	writeJSON(res, submitLeaderboard(body, playerID(req.RemoteAddr)))
}
