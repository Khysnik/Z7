package main

import (
	"bytes"
	"encoding/json"
	"fmt"
	"net/http"
	"os"
	"strconv"
	"strings"
	"time"
)

func HandleBytevault(res http.ResponseWriter, req *http.Request) {
	if req.Method == http.MethodGet {
		data, err := os.ReadFile("./data/bytevault.json")
		if err != nil {
			http.Error(res, "Failed to load JSON", http.StatusInternalServerError)
			return
		}

		var obj map[string]any
		dec := json.NewDecoder(bytes.NewReader(data))
		dec.UseNumber()
		if err := dec.Decode(&obj); err != nil {
			http.Error(res, "Invalid JSON", http.StatusInternalServerError)
			return
		}

		dataSection, ok := obj["data"]
		if !ok {
			http.Error(res, "Missing data field", http.StatusInternalServerError)
			return
		}

		out, err := json.Marshal(dataSection)
		if err != nil {
			http.Error(res, "Failed to encode JSON", http.StatusInternalServerError)
			return
		}

		creation := fmt.Sprintf("%v", obj["creationDate"])
		lastUpdate := fmt.Sprintf("%v", obj["lastUpdate"])

		fmt.Println("GET .../PlayerProfile from", req.RemoteAddr)

		res.Header().Set("Content-Type", "application/json")
		res.Header().Set("x-blaze-errorcode", "0")
		res.Header().Set("x-blaze-component", "bytevault")
		res.Header().Set("x-blaze-command", "getRecord")
		res.Header().Set("x-creation", creation)
		res.Header().Set("x-lastupdate", lastUpdate)
		res.Header().Set("server", "istio-envoy")
		res.Header().Set("Content-Length", strconv.Itoa(len(out)))
		res.WriteHeader(http.StatusOK)
		res.Write(out)
	} else if req.Method == http.MethodPut {
		userID := req.URL.Query().Get("ownerId")

		data, err := os.ReadFile("./data/bytevault.json")
		if err != nil {
			http.Error(res, err.Error(), http.StatusInternalServerError)
			return
		}

		fmt.Println("POST .../PlayerProfile from", req.RemoteAddr)

		var profile map[string]any

		dec := json.NewDecoder(bytes.NewReader(data))
		dec.UseNumber()

		if err := dec.Decode(&profile); err != nil {
			http.Error(res, err.Error(), http.StatusInternalServerError)
			return
		}

		var updates map[string]any

		dec = json.NewDecoder(req.Body)
		dec.UseNumber()

		if err := dec.Decode(&updates); err != nil {
			http.Error(res, err.Error(), http.StatusBadRequest)
			return
		}

		dataMap, ok := profile["data"].(map[string]any)
		if !ok {
			dataMap = make(map[string]any)
			profile["data"] = dataMap
		}

		for pathString, value := range updates {
			keys := strings.Split(pathString, ".")

			current := dataMap

			for _, key := range keys[:len(keys)-1] {
				next, ok := current[key].(map[string]any)
				if !ok {
					next = make(map[string]any)
					current[key] = next
				}

				current = next
			}

			current[keys[len(keys)-1]] = value
		}

		profile["lastUpdate"] = strconv.FormatInt(time.Now().UnixMicro(), 10)

		out, err := json.MarshalIndent(profile, "", "    ")
		if err != nil {
			http.Error(res, err.Error(), http.StatusInternalServerError)
			return
		}

		if err := os.WriteFile("./data/bytevault.json", out, 0644); err != nil {
			http.Error(res, err.Error(), http.StatusInternalServerError)
			return
		}

		resp := map[string]any{
			"lastUpdateTime": profile["lastUpdate"],
			"recordName":     "PlayerProfile",
			"creationTime":   profile["creationDate"],
			"owner": map[string]any{
				"type": "NUCLEUS_PERSONA",
				"id":   userID,
			},
		}
		body, _ := json.Marshal(resp)

		res.Header().Set("Content-Type", "application/json")
		res.Header().Set("x-blaze-errorcode", "0")
		res.Header().Set("x-blaze-component", "bytevault")
		res.Header().Set("x-blaze-command", "upsertRecord")
		res.Header().Set("x-lastupdate", fmt.Sprintf("%v", profile["lastUpdate"]))
		res.Header().Set("server", "istio-envoy")
		res.Header().Set("Content-Length", strconv.Itoa(len(body)))
		res.WriteHeader(http.StatusOK)
		res.Write(body)
	} else {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}
}
