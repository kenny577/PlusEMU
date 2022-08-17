using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Moderator;

internal class CloseIssueDefaultActionEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        return Task.CompletedTask;
    }
}