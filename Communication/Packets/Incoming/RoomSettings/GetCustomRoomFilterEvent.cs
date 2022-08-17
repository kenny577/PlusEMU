using Plus.Communication.Packets.Outgoing.Rooms.Settings;
using Plus.HabboHotel.Achievements;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.RoomSettings;

internal class GetCustomRoomFilterEvent : IPacketEvent
{
    private readonly IAchievementManager _achievementManager;

    public GetCustomRoomFilterEvent(IAchievementManager achievementManager)
    {
        _achievementManager = achievementManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var instance = session.GetHabbo().CurrentRoom;
        if (instance == null)
            return Task.CompletedTask;
        if (!instance.CheckRights(session))
            return Task.CompletedTask;
        session.Send(new GetRoomFilterListComposer(instance));
        _achievementManager.ProgressAchievement(session, "ACH_SelfModRoomFilterSeen", 1);
        return Task.CompletedTask;
    }
}