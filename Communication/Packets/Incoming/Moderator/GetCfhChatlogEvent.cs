using Plus.Communication.Packets.Outgoing.Moderation;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Moderation;
using Plus.HabboHotel.Rooms;

namespace Plus.Communication.Packets.Incoming.Moderator;

internal class GetCfhChatlogEvent : IPacketEvent
{
    private readonly IModerationManager _moderationManager;

    public GetCfhChatlogEvent(IModerationManager moderationManager)
    {
        _moderationManager = moderationManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().GetPermissions().HasRight("mod_tickets"))
            return Task.CompletedTask;
        var ticketId = packet.ReadInt();
        if (!_moderationManager.TryGetTicket(ticketId, out var ticket) || ticket.Room == null)
            return Task.CompletedTask;
        if (!RoomFactory.TryGetData(ticket.Room.Id, out var data))
            return Task.CompletedTask;
        session.Send(new ModeratorTicketChatlogComposer(ticket, data, ticket.Timestamp));
        return Task.CompletedTask;
    }
}