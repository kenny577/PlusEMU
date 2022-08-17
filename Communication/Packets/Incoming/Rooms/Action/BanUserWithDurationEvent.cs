using Plus.HabboHotel.Achievements;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Rooms.Action;

internal class BanUserWithDurationEvent : IPacketEvent
{
    private readonly IAchievementManager _achievementManager;

    public BanUserWithDurationEvent(IAchievementManager achievementManager)
    {
        _achievementManager = achievementManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var room = session.GetHabbo().CurrentRoom;
        if (room == null)
            return Task.CompletedTask;
        if (room.WhoCanBan == 0 && !room.CheckRights(session, true) && room.Group == null || room.WhoCanBan == 1 && !room.CheckRights(session) && room.Group == null ||
            room.Group != null && !room.CheckRights(session, false, true))
            return Task.CompletedTask;
        var userId = packet.ReadInt();
        packet.ReadInt(); //roomId
        var r = packet.ReadString();
        var user = room.GetRoomUserManager().GetRoomUserByHabbo(Convert.ToInt32(userId));
        if (user == null || user.IsBot)
            return Task.CompletedTask;
        if (room.OwnerId == userId)
            return Task.CompletedTask;
        if (user.GetClient().GetHabbo().GetPermissions().HasRight("mod_tool"))
            return Task.CompletedTask;
        long time = 0;
        if (r.ToLower().Contains("hour"))
            time = 3600;
        else if (r.ToLower().Contains("day"))
            time = 86400;
        else if (r.ToLower().Contains("perm"))
            time = 78892200;
        room.GetBans().Ban(user, time);
        _achievementManager.ProgressAchievement(session, "ACH_SelfModBanSeen", 1);
        return Task.CompletedTask;
    }
}