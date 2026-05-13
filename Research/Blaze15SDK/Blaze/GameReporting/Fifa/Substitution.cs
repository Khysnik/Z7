using EATDF;
using EATDF.Members;

namespace Blaze15SDK.Blaze.GameReporting.Fifa;

public class Substitution : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("PlayerSentOff", "playerSentOff", 0xC33BE600, TdfType.Int32, 0, true), // Tag: PSOF
        new TdfMemberInfo("PlayerSubbedIn", "playerSubbedIn", 0xC33BEE00, TdfType.Int32, 1, true), // Tag: PSON
        new TdfMemberInfo("SubTime", "subTime", 0xD33D6200, TdfType.Int32, 2, true), // Tag: TSUB
    ];
    private ITdfMember[] __members;

    private TdfInt32 _playerSentOff = new(__typeInfos[0]);
    private TdfInt32 _playerSubbedIn = new(__typeInfos[1]);
    private TdfInt32 _subTime = new(__typeInfos[2]);

    public Substitution()
    {
        __members = [_playerSentOff, _playerSubbedIn, _subTime];
    }

    public override Tdf CreateNew() => new Substitution();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "Substitution";
    public override string GetFullClassName() => "Blaze::GameReporting::Fifa::Substitution";

    public int PlayerSentOff { get => _playerSentOff.Value; set => _playerSentOff.Value = value; }
    public int PlayerSubbedIn { get => _playerSubbedIn.Value; set => _playerSubbedIn.Value = value; }
    public int SubTime { get => _subTime.Value; set => _subTime.Value = value; }
}
