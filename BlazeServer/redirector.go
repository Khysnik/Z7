package main

import (
	"encoding/xml"
	"fmt"
	"log"
	"net/http"
)

type ServerInstanceRequest struct {
	XMLName           xml.Name `xml:"serverinstancerequest"`
	BlazeSDKVersion   string   `xml:"blazesdkversion"`
	BlazeSDKBuildDate string   `xml:"blazesdkbuilddate"`
	ClientName        string   `xml:"clientname"`
	ClientType        string   `xml:"clienttype"`
	ClientPlatform    string   `xml:"clientplatform"`
	ClientSKUId       string   `xml:"clientskuid"`
	ClientVersion     string   `xml:"clientversion"`
	DirtySDKVersion   string   `xml:"dirtysdkversion"`
	Environment       string   `xml:"environment"`
	ClientLocale      uint32   `xml:"clientlocale"`
	Name              string   `xml:"name"`
	Platform          string   `xml:"platform"`
	ConnectionProfile string   `xml:"connectionprofile"`
	IsTrial           int      `xml:"istrial"`
}

type ServerInstanceInfo struct {
	XMLName           xml.Name `xml:"serverinstanceinfo"`
	Address           Address  `xml:"address"`
	Secure            int      `xml:"secure"`
	TrialServiceName  string   `xml:"trialservicename"`
	DefaultDNSAddress uint32   `xml:"defaultdnsaddress"`
	Messages          Messages `xml:"messages"`
}

type Address struct {
	Member int          `xml:"member,attr"`
	Value  AddressValue `xml:"valu"`
}

type AddressValue struct {
	Hostname string `xml:"hostname"`
	IP       uint32 `xml:"ip"`
	Port     uint16 `xml:"port"`
}

type Messages struct {
	WarnMessage string `xml:"warnMessage"`
}

func HandleRedirector(res http.ResponseWriter, req *http.Request) {
	if req.Method != http.MethodPost {
		http.Error(res, "Method not allowed", http.StatusMethodNotAllowed)
		return
	}

	var schema ServerInstanceRequest

	if err := xml.NewDecoder(req.Body).Decode(&schema); err != nil {
		http.Error(res, err.Error(), http.StatusBadRequest)
		return
	}

	fmt.Println("POST /redirector/getServerInstance from", req.RemoteAddr)

	/*
		Hostname: "localhost",
		IP:       2130706433,
		Port:     10041,
	*/

	resp := ServerInstanceInfo{
		Address: Address{
			Member: 0,
			Value: AddressValue{
				Hostname: "localhost",
				IP:       2130706433,
				Port:     10041,
			},
		},
		Secure:            1,
		TrialServiceName:  "",
		DefaultDNSAddress: 0,
		Messages: Messages{
			WarnMessage: "Z7 Emulator v1.1.0",
		},
	}

	data, err := xml.MarshalIndent(resp, "", "  ")
	if err != nil {
		log.Fatal(err)
	}

	res.Header().Set("Content-Type", "application/xml")
	res.Write([]byte(xml.Header))
	res.Write(data)
}
