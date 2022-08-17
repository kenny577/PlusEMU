using Plus.Communication.Packets.Outgoing.Navigator;
using Plus.Database;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Navigator;

public class DeleteFavouriteRoomEvent : IPacketEvent
{
    private readonly IDatabase _database;

    public DeleteFavouriteRoomEvent(IDatabase database)
    {
        _database = database;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var id = packet.ReadInt();
        session.GetHabbo().FavoriteRooms.Remove(id);
        session.Send(new UpdateFavouriteRoomComposer(id, false));
        using var dbClient = _database.GetQueryReactor();
        dbClient.RunQuery("DELETE FROM user_favorites WHERE user_id = " + session.GetHabbo().Id + " AND room_id = " + id + " LIMIT 1");
        return Task.CompletedTask;
    }
}