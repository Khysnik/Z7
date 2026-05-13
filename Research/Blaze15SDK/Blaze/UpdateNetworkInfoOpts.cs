namespace Blaze15SDK.Blaze;

[Flags]
public enum UpdateNetworkInfoOpts : int {
    None = 0,
    NetworkAddressOnly = 1,
    NatInfoOnly = 2,
    UpdateMetrics = 4,
}
