using Plus.Database;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Users;

internal class UnignoreUserEvent : IPacketEvent
{
    private readonly IDatabase _database;

    public UnignoreUserEvent(IDatabase database)
    {
        _database = database;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var room = session.GetHabbo().CurrentRoom;
        if (room == null)
            return Task.CompletedTask;
        var username = packet.ReadString();
        var player = PlusEnvironment.GetGame().GetClientManager().GetClientByUsername(username)?.GetHabbo();
        if (player == null)
            return Task.CompletedTask;
        session.GetHabbo().IgnoresComponent.Unignore(player.Id);
        return Task.CompletedTask;
    }
}