using Plus.Communication.Packets.Outgoing.Rooms.Session;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Rooms.Connection;

internal class GoToFlatEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        if (!session.GetHabbo().EnterRoom(session.GetHabbo().CurrentRoom))
            session.Send(new CloseConnectionComposer());
        return Task.CompletedTask;
    }
}