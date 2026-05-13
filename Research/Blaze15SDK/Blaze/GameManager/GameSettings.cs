namespace Blaze15SDK.Blaze.GameManager;

[Flags]
public enum GameSettings : int
{
    openToBrowsing = 0x1,
    openToMatchmaking = 0x2,
    openToInvites = 0x4,
    openToJoinByPlayer = 0x8,
    hostMigratable = 0x10,
    ranked = 0x20,
    adminOnlyInvites = 0x40,
    enforceSingleGroupJoin = 0x80,
    joinInProgressSupported = 0x100,
    adminInvitesOnlyIgnoreEntryChecks = 0x200,
    ignoreEntryCriteriaWithInvite = 0x400,
    enablePersistedGameId = 0x800,
    allowSameTeamId = 0x1000,
    virtualized = 0x2000,
    sendOrphanedGameReportEvent = 0x4000,
    allowAnyReputation = 0x8000,
    lockedAsBusy = 0x10000,
    disconnectReservation = 0x20000,
    dynamicReputationRequirement = 0x40000,
    friendsBypassClosedToJoinByPlayer = 0x80000,
    allowMemberGameAttributeEdit = 0x100000,
    autoDemoteReservedPlayers = 0x200000,
    updateQueueCapacityOnReset = 0x400000,
    spectatorBypassClosedToJoin = 0x800000,
}
