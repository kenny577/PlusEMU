using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Rooms.Connection;

public class OpenFlatConnectionEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var roomId = packet.ReadInt();
        var password = packet.ReadString();
        session.GetHabbo().PrepareRoom(roomId, password);
        return Task.CompletedTask;
    }
}