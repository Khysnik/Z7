package main

import (
	"crypto/tls"
	"log"
	"net/http"
)

func main() {
	mux := http.NewServeMux()

	cert, err := tls.LoadX509KeyPair("certs/server.crt", "certs/server.key")
	if err != nil {
		log.Fatal(err)
	}

	cfg := &tls.Config{
		Certificates: []tls.Certificate{cert},
		MinVersion:   tls.VersionTLS10,
		MaxVersion:   tls.VersionTLS13,
		ClientAuth:   tls.NoClientCert,
		CipherSuites: []uint16{
			tls.TLS_ECDHE_RSA_WITH_AES_128_GCM_SHA256,
			tls.TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384,
			tls.TLS_ECDHE_RSA_WITH_AES_128_CBC_SHA,
			tls.TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA,
			tls.TLS_RSA_WITH_AES_128_GCM_SHA256,
			tls.TLS_RSA_WITH_AES_256_GCM_SHA384,
			tls.TLS_RSA_WITH_AES_128_CBC_SHA,
			tls.TLS_RSA_WITH_AES_256_CBC_SHA,
		},
	}
	// Redirector
	mux.HandleFunc("/redirector/getServerInstance", HandleRedirector)
	// Editorial
	mux.Handle("/PlantsVsZombies/GW2/", http.StripPrefix("/PlantsVsZombies/GW2/", http.FileServer(http.Dir("./files/GW2"))))
	mux.HandleFunc("/gw2/live/blackmarket", HandleBlackmarket)
	mux.HandleFunc("/gw2/live/dailyquests", HandleDailyquest)
	mux.HandleFunc("/gw2/live/communityevent", HandleCommevent)
	mux.HandleFunc("/gw2/live/communitychallenge", HandleCommchal)
	mux.HandleFunc("/gw2/live/challenge/contribute", HandlePostCommchal)
	mux.HandleFunc("/gw2/live/leaderboard", HandleLeaderboard)
	mux.HandleFunc("/gw2/live/leaderboard/submit", HandlePostLeaderboard)
	// Bytevault
	mux.HandleFunc("/1.0/contexts/plantsvszombies-gw2-pc/categories/PVZ/records/PlayerProfile", HandleBytevault)
	// QoS
	mux.HandleFunc("/qos/qos", HandleQoS)
	mux.HandleFunc("/qos/firewall", HandleFirewall)
	mux.HandleFunc("/qos/firetype", HandleFiretype)

	srv := &http.Server{
		Addr:         ":42230",
		Handler:      mux,
		TLSConfig:    cfg,
		TLSNextProto: make(map[string]func(*http.Server, *tls.Conn, http.Handler)),
	}

	log.Print("Server listening on port 42230")

	log.Fatal(srv.ListenAndServeTLS("", ""))

}
