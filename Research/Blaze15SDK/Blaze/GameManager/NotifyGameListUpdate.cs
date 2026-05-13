using EATDF;
using EATDF.Members;
using EATDF.Types;

namespace Blaze15SDK.Blaze.GameManager;

public class NotifyGameListUpdate : Tdf
{
    static readonly TdfMemberInfo[] __typeInfos = [
        new TdfMemberInfo("IsFinalUpdate", "mIsFinalUpdate", 0x92FBA500, TdfType.UInt8, 0, true), // Tag: DONE
        new TdfMemberInfo("ListId", "mListId", 0x9ECA6400, TdfType.UInt64, 1, true), // Tag: GLID
        new TdfMemberInfo("RemovedGameList", "mRemovedGameList", 0xCA5B7600, TdfType.List, 2, true), // Tag: REMV
        new TdfMemberInfo("UpdatedGames", "mUpdatedGames", 0xD7093400, TdfType.List, 3, true), // Tag: UPDT
    ];
    private ITdfMember[] __members;

    private TdfUInt8 _isFinalUpdate = new(__typeInfos[0]);
    private TdfUInt64 _listId = new(__typeInfos[1]);
    private TdfList<ulong> _removedGameList = new(__typeInfos[2]);
    private TdfList<Blaze15SDK.Blaze.GameManager.GameBrowserMatchData> _updatedGames = new(__typeInfos[3]);

    public NotifyGameListUpdate()
    {
        __members = [ _isFinalUpdate, _listId, _removedGameList, _updatedGames ];
    }

    public override Tdf CreateNew() => new NotifyGameListUpdate();
    public override ITdfMember[] GetMembers() => __members;
    public override TdfMemberInfo[] GetMemberInfos() => __typeInfos;
    public static TdfMemberInfo[] GetTdfMemberInfos() => __typeInfos;
    public override string GetClassName() => "NotifyGameListUpdate";
    public override string GetFullClassName() => "Blaze::GameManager::NotifyGameListUpdate";

    public byte IsFinalUpdate
    {
        get => _isFinalUpdate.Value;
        set => _isFinalUpdate.Value = value;
    }

    public ulong ListId
    {
        get => _listId.Value;
        set => _listId.Value = value;
    }

    public IList<ulong> RemovedGameList
    {
        get => _removedGameList.Value;
        set => _removedGameList.Value = value;
    }

    public IList<Blaze15SDK.Blaze.GameManager.GameBrowserMatchData> UpdatedGames
    {
        get => _updatedGames.Value;
        set => _updatedGames.Value = value;
    }

}
