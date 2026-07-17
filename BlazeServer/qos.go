package main

import (
	"encoding/xml"
	"fmt"
	"log"
	"net/http"
)

type QoSResponse struct {
	XMLName   xml.Name `xml:"qos"`
	NumProbes int      `xml:"numprobes"`
	QosPort   int      `xml:"qosport"`
	ProbeSize int      `xml:"probesize"`
	QosIp     int      `xml:"qosip"`
	RequestID int      `xml:"requestid"`
	ReqSecret int      `xml:"reqsecret"`
}

type FirewallResponse struct {
	XMLName       xml.Name `xml:"firewall"`
	Ips           Ips      `xml:"ips"`
	NumInterfaces int      `xml:"numinterfaces"`
	Ports         Ports    `xml:"ports"`
	RequestID     int      `xml:"requestid"`
	ReqSecret     int      `xml:"reqsecret"`
}

type Ips struct {
	Ips []int `xml:"ips"`
}

type Ports struct {
	Ports []int `xml:"ports"`
}

type FiretypeResponse struct {
	XMLName  xml.Name `xml:"firetype"`
	Firetype int      `xml:"firetype"`
}

func HandleQoS(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /qos/qos from", req.RemoteAddr)

	var resp QoSResponse

	if req.URL.Query().Get("qtyp") == "1" {
		resp = QoSResponse{
			NumProbes: 0,
			QosPort:   42231,
			ProbeSize: 0,
			QosIp:     2130706433,
			RequestID: 1,
			ReqSecret: 0,
		}
	} else if req.URL.Query().Get("qtyp") == "2" {
		resp = QoSResponse{
			NumProbes: 10,
			QosPort:   42231,
			ProbeSize: 1200,
			QosIp:     2130706433,
			RequestID: 1240,
			ReqSecret: 2045,
		}
	} else {
		http.Error(res, "Invalid request", http.StatusInternalServerError)
		return
	}

	data, err := xml.MarshalIndent(resp, "", "  ")
	if err != nil {
		log.Fatal(err)
	}

	res.Header().Set("Content-Type", "application/xml")
	res.Write([]byte(xml.Header))
	res.Write(data)
}

func HandleFirewall(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /qos/firewall from", req.RemoteAddr)

	var resp FirewallResponse

	// Two interfaces/ports, matching the C++ QoS UDP firewall ports (probe+1, probe+2 =
	// 42232, 42233). qosip = the C++ server (127.0.0.1 = 2130706433). The client probes
	// these to determine its NAT type.
	resp = FirewallResponse{
		Ips: Ips{
			Ips: []int{2130706433, 2130706433},
		},
		NumInterfaces: 2,
		Ports: Ports{
			Ports: []int{42232, 42233},
		},
		RequestID: 382,
		ReqSecret: 428,
	}

	data, err := xml.MarshalIndent(resp, "", "  ")
	if err != nil {
		log.Fatal(err)
	}

	res.Header().Set("Content-Type", "application/xml")
	res.Write([]byte(xml.Header))
	res.Write(data)
}

func HandleFiretype(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodGet {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	fmt.Println("GET /qos/firetype from", req.RemoteAddr)

	var resp FiretypeResponse

	resp = FiretypeResponse{
		Firetype: 4,
	}

	data, err := xml.MarshalIndent(resp, "", "  ")
	if err != nil {
		log.Fatal(err)
	}

	res.Header().Set("Content-Type", "application/xml")
	res.Write([]byte(xml.Header))
	res.Write(data)
}
