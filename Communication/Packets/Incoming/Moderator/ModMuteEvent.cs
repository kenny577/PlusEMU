using Plus.Database;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Moderator;

internal class ModMuteEvent : IPacketEvent
{
    public readonly IDatabase _database;

    public ModMuteEvent(IDatabase database)
    {
        _database = database;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().GetPermissions().HasRight("mod_mute"))
            return Task.CompletedTask;
        var userId = packet.ReadInt();
        packet.ReadString(); //message
        double length = packet.ReadInt() * 60;
        packet.ReadString(); //unk1
        packet.ReadString(); //unk2
        var habbo = PlusEnvironment.GetHabboById(userId);
        if (habbo == null)
        {
            session.SendWhisper("An error occoured whilst finding that user in the database.");
            return Task.CompletedTask;
        }
        if (habbo.GetPermissions().HasRight("mod_mute") && !session.GetHabbo().GetPermissions().HasRight("mod_mute_any"))
        {
            session.SendWhisper("Oops, you cannot mute that user.");
            return Task.CompletedTask;
        }
        using (var dbClient = _database.GetQueryReactor())
        {
            dbClient.RunQuery("UPDATE `users` SET `time_muted` = '" + length + "' WHERE `id` = '" + habbo.Id + "' LIMIT 1");
        }
        if (habbo.GetClient() != null)
        {
            habbo.TimeMuted = length;
            habbo.GetClient().SendNotification("You have been muted by a moderator for " + length + " seconds!");
        }
        return Task.CompletedTask;
    }
}