using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Moderator;

internal class ModMessageEvent : IPacketEvent
{
    private readonly IGameClientManager _clientManager;

    public ModMessageEvent(IGameClientManager clientManager)
    {
        _clientManager = clientManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().GetPermissions().HasRight("mod_alert"))
            return Task.CompletedTask;
        var userId = packet.ReadInt();
        var message = packet.ReadString();
        var client = _clientManager.GetClientByUserId(userId);
        if (client == null)
            return Task.CompletedTask;
        client.SendNotification(message);
        return Task.CompletedTask;
    }
}