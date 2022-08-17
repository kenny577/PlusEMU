using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Inventory.AvatarEffect;

internal class AvatarEffectSelectedEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var effectId = packet.ReadInt();
        if (effectId < 0)
            effectId = 0;
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var room = session.GetHabbo().CurrentRoom;
        if (room == null)
            return Task.CompletedTask;
        var user = room.GetRoomUserManager().GetRoomUserByHabbo(session.GetHabbo().Id);
        if (user == null)
            return Task.CompletedTask;
        if (effectId != 0 && session.GetHabbo().Effects().HasEffect(effectId, true))
            user.ApplyEffect(effectId);
        return Task.CompletedTask;
    }
}