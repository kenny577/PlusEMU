using Plus.Communication.Packets.Outgoing.Talents;
using Plus.HabboHotel.GameClients;
using Plus.HabboHotel.Talents;

namespace Plus.Communication.Packets.Incoming.Talents;

internal class GetTalentTrackEvent : IPacketEvent
{
    private readonly ITalentTrackManager _talentTrackManager;

    public GetTalentTrackEvent(ITalentTrackManager talentTrackManager)
    {
        _talentTrackManager = talentTrackManager;
    }

    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        var type = packet.ReadString();
        var levels = _talentTrackManager.GetLevels();
        session.Send(new TalentTrackComposer(levels, type));
        return Task.CompletedTask;
    }
}