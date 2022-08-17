using Plus.Communication.Packets.Outgoing.Inventory.AvatarEffects;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Inventory.AvatarEffect;

internal class AvatarEffectActivatedEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var effectId = packet.ReadInt();
        var effect = session.GetHabbo().Effects().GetEffectNullable(effectId, false, true);
        if (session.GetHabbo().Effects().HasEffect(effectId, true)) return Task.CompletedTask;
        if (effect.Activate()) session.Send(new AvatarEffectActivatedComposer(effect));
        return Task.CompletedTask;
    }
}