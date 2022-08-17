using Plus.Communication.Packets.Outgoing.Rooms.Settings;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Rooms;

namespace Plus.Communication.Packets.Incoming.RoomSettings;

internal class GetRoomSettingsEvent : IPacketEvent
{
    private readonly IRoomManager _roomManager;

    public GetRoomSettingsEvent(IRoomManager roomManager)
    {
        _roomManager = roomManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var roomId = packet.ReadInt();
        if (!_roomManager.TryLoadRoom(roomId, out var room))
            return Task.CompletedTask;
        if (!room.CheckRights(session, true))
            return Task.CompletedTask;
        session.Send(new RoomSettingsDataComposer(room));
        return Task.CompletedTask;
    }
}