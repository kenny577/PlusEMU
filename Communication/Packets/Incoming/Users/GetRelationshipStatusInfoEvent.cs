using Plus.Communication.Packets.Outgoing.Users;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Users;

internal class GetRelationshipStatusInfoEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var habbo = PlusEnvironment.GetHabboById(packet.ReadInt());
        if (habbo == null)
            return Task.CompletedTask;
        session.Send(new GetRelationshipsComposer(habbo));
        return Task.CompletedTask;
    }
}