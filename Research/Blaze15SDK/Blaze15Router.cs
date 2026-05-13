using Blaze.Core;
using Blaze15SDK.Blaze.GameReporting.Fifa;
using Blaze15SDK.Blaze.GameReporting.OSDKGameReportBase;
using Blaze15SDK.Components;
using EATDF.Serialization;

namespace Blaze15SDK;

public class Blaze15Router : BlazeRouter
{
    public Blaze15Router()
    {
        ConfigureServerErrors<ServerError>();
        ConfigureSdkErrors<SdkError>();

        // Register all 20 Blaze v15 components
        AddComponent(new AuthenticationComponentBase.Server());      // Id = 1
        AddComponent(new ExampleComponentBase.Server());             // Id = 3
        AddComponent(new GameManagerComponentBase.Server());         // Id = 4
        AddComponent(new RedirectorComponentBase.Server());          // Id = 5
        AddComponent(new StatsComponentBase.Server());               // Id = 7
        AddComponent(new UtilComponentBase.Server());                // Id = 9
        AddComponent(new CensusDataComponentBase.Server());          // Id = 10
        AddComponent(new ClubsComponentBase.Server());               // Id = 11
        AddComponent(new MailComponentBase.Server());                // Id = 14
        AddComponent(new MessagingComponentBase.Server());           // Id = 15
        AddComponent(new AssociationListsComponentBase.Server());    // Id = 25
        AddComponent(new GpsContentControllerComponentBase.Server()); // Id = 27
        AddComponent(new GameReportingComponentBase.Server());       // Id = 28
        AddComponent(new ByteVaultComponentBase.Server());           // Id = 31
        AddComponent(new AchievementsComponentBase.Server());        // Id = 33
        AddComponent(new XblSystemConfigsComponentBase.Server());    // Id = 1025
        AddComponent(new FriendsComponentBase.Server());             // Id = 1031
        AddComponent(new UserSessionsBase.Server());                 // Id = 30722
        AddComponent(new DynamicInetFilterComponentBase.Server());   // Id = 30732
        AddComponent(new OSDKSettingsComponentBase.Server());        // Id = 2249 (custom/201)
        AddComponent(new SponsoredEventsComponentBase.Server());    // Id = 2076 (custom/28)
    }

    /// <summary>
    /// Registers all known Blaze v15 TDF variant types with the given registry.
    /// Call this during server configuration so the Heat2 deserializer can resolve variant fields
    /// (e.g. GameReport.Report, OSDKGameReport.CustomGameReport, OSDKPlayerReport.CustomPlayerReport).
    /// </summary>
    public static void RegisterTdfTypes(ITdfRegistry registry)
    {
        // OSDK game report base (top-level report variant for GameReport.Report)
        registry.RegisterTdf<OSDKReport>();

        // FIFA Solo game report types (variants for custom game/player reports)
        registry.RegisterTdf<SoloGameReport>();
        registry.RegisterTdf<SoloPlayerReport>();
    }
}
