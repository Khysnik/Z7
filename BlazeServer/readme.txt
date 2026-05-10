hacky as shit, but it's working

lots of placeholder data for now just to get it working, progresses up to Blaze::Packs::GetPacksRequest and the game client throws a weird error

progress:

>> Blaze::Util::PreAuthRequest
<< Blaze::Util::PreAuthResponse
>> Blaze::Util::Ping
<< Blaze::Util::PingResponse
>> Blaze::Util::FetchClientConfigRequest (IdentityParams)
<< Blaze::Util::FetchConfigResponse (IdentityParams)
>> Blaze::Authentication::LoginRequest
<< Blaze::Authentication::LoginResponse
<< Blaze::Authentication::UserAuthenticated (notif)
>> Blaze::Util::PostAuthRequest
<< Blaze::Util::PostAuthResponse
<< Blaze::Util::UserAdded (notif)
<< Blaze::Util::UserSessionExtendedDataUpdate (notif)
>> Blaze::UserSessions::updateNetworkInfo
>> Blaze::UserSessions::updateHardwareFlags
>> Blaze::UserSessions::updateNetworkInfo
>> Blaze::Util::GetTelemetryServerRequest
<< Blaze::Util::GetTelemetryServerResponse
>> Blaze::Util::SetClientState
>> Blaze::PvzGw::GetDailyQuests
<< Blaze::PvzGw::GetDailyQuestsResponse
>> Blaze::PvzGw::getClientSettingsRequest
<< Blaze::PvzGw::getClientSettingsResponse
>> Blaze::PvzGw::SetXPMultiplierRequest
<< Blaze::PvzGw::SetXPMultiplierResponse
>> Blaze::Util::FetchClientConfigRequest (KILL_SWITCHES)
<< Blaze::Util::FetchClientConfigResponse (KILL_SWITCHES)
>> Blaze::Util::FetchClientConfigRequest (PVZ_ONLINE_PLAYLISTS)
<< Blaze::Util::FetchClientConfigResponse (PVZ_ONLINE_PLAYLISTS) 
>> Blaze::Util::FetchClientConfigRequest (EDITORIAL)
<< Blaze::Util::FetchClientConfigResponse (EDITORIAL)
>> Blaze::PvzGw::ForceClientNotificationRequest
<< Blaze::PvzGw::ForceClientNotificationResponse
>> Blaze::Inventory::GetItemsRequest
>> Blaze::PvzGw::GetPersistedLicensesRequest
<< Blaze::PvzGw::GetPersistedLicensesResponse
>> Blaze::UserSessions::UpdateHardwareFlags
>> Blaze::PvzGw::CheckUserMessagesRequest
<< Blaze::PvzGw::CheckUserMessagesResponse
>> Blaze::PvzGw::SetOnlineAccessEntitlementsRequest
<< Blaze::PvzGw::SetOnlineAccessEntitlementsResponse
>> Blaze::Util::FetchClientConfigRequest (SPOTLIGHT)
<< Blaze::Util::FetchClientConfigResponse (SPOTLIGHT)
>> Blaze::Util::FetchClientConfigRequest (PVZ_NEWS)
<< Blaze::Util::FetchClientConfigResponse (PVZ_NEWS)
>> Blaze::Packs::GetTemplateRequest
<< Blaze::Packs::GetTemplateResponse
>> Blaze::Packs::GetPacksRequest
<< Blaze::Packs::GetPacksResponse

-- keepalive loop (~30s) --
>> Blaze::Util::Ping
<< Blaze::Util::PingResponse
>> Blaze::PvzGw::ForceClientNotificationRequest
<< Blaze::PvzGw::ForceClientNotificationResponse

-- on disconnect --
>> Blaze::Authentication::LogoutRequest   (comp=0x0001 cmd=0x46)