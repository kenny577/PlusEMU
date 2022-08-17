using Plus.Communication.Packets.Outgoing.Rooms.Settings;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.RoomSettings;

internal class GetBannedUsersFromRoomEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var instance = session.GetHabbo().CurrentRoom;
        if (instance == null || !instance.CheckRights(session, true))
            return Task.CompletedTask;
        if (instance.GetBans().BannedUsers().Count > 0)
            session.Send(new GetRoomBannedUsersComposer(instance));
        return Task.CompletedTask;
    }
}