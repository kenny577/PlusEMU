using Plus.Communication.Packets.Outgoing.Rooms.Settings;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Rooms.Action;

internal class UnbanUserFromRoomEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var instance = session.GetHabbo().CurrentRoom;
        if (instance == null || !instance.CheckRights(session, true))
            return Task.CompletedTask;
        var userId = packet.ReadInt();
        var roomId = packet.ReadInt();
        if (instance.GetBans().IsBanned(userId))
        {
            instance.GetBans().Unban(userId);
            session.Send(new UnbanUserFromRoomComposer(roomId, userId));
        }
        return Task.CompletedTask;
    }
}