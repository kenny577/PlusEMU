using Plus.Database;
using Plus.HabboHotel.GameClients;
using Plus.Utilities;

namespace Plus.Communication.Packets.Incoming.Preferences;

internal class SetUserFocusPreferenceEvent : IPacketEvent
{
    private readonly IDatabase _database;

    public SetUserFocusPreferenceEvent(IDatabase database)
    {
        _database = database;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var focusPreference = packet.ReadBool();
        session.GetHabbo().FocusPreference = focusPreference;
        using var dbClient = _database.GetQueryReactor();
        dbClient.SetQuery("UPDATE `users` SET `focus_preference` = @focusPreference WHERE `id` = @habboId LIMIT 1");
        dbClient.AddParameter("habboId", session.GetHabbo().Id);
        dbClient.AddParameter("focusPreference", ConvertExtensions.ToStringEnumValue(focusPreference));
        dbClient.RunQuery();
        return Task.CompletedTask;
    }
}