namespace Blaze15SDK.Blaze.ByteVault;

[Flags]
public enum Permission : int
{
    PERMISSION_READ = 0x1,
    PERMISSION_WRITE = 0x2,
    PERMISSION_VIEW_DELETED = 0x4,
}
