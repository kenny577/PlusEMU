using Plus.Communication.Packets.Outgoing.Moderation;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Rooms;

namespace Plus.Communication.Packets.Incoming.Moderator;

internal class GetModeratorRoomInfoEvent : IPacketEvent
{
    private readonly IRoomManager _roomManager;

    public GetModeratorRoomInfoEvent(IRoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().GetPermissions().HasRight("mod_tool"))
            return Task.CompletedTask;
        var roomId = packet.ReadInt();
        if (!RoomFactory.TryGetData(roomId, out var data))
            return Task.CompletedTask;
        if (!_roomManager.TryGetRoom(roomId, out var room))
            return Task.CompletedTask;
        session.Send(new ModeratorRoomInfoComposer(data, room.GetRoomUserManager().GetRoomUserByHabbo(data.OwnerName) != null));
        return Task.CompletedTask;
    }
}