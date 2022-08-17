using Plus.Communication.Packets.Outgoing.Rooms.Polls;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Poll;

internal class PollStartEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        session.Send(new PollContentsComposer());
        return Task.CompletedTask;
    }
}