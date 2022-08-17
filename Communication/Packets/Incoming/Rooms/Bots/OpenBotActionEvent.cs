using Plus.Communication.Packets.Outgoing.Rooms.AI.Bots;
using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Rooms.Bots;

internal class OpenBotActionEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet)
    {
        if (!session.GetHabbo().InRoom)
            return Task.CompletedTask;
        var botId = packet.ReadInt();
        var actionId = packet.ReadInt();
        var room = session.GetHabbo().CurrentRoom;
        if (room == null)
            return Task.CompletedTask;
        if (!room.GetRoomUserManager().TryGetBot(botId, out var botUser))
            return Task.CompletedTask;
        var botSpeech = "";
        foreach (var speech in botUser.BotData.RandomSpeech.ToList()) botSpeech += speech.Message + "\n";
        botSpeech += ";#;";
        botSpeech += botUser.BotData.AutomaticChat;
        botSpeech += ";#;";
        botSpeech += botUser.BotData.SpeakingInterval;
        botSpeech += ";#;";
        botSpeech += botUser.BotData.MixSentences;
        if (actionId == 2 || actionId == 5)
            session.Send(new OpenBotActionComposer(botUser, actionId, botSpeech));
        return Task.CompletedTask;
    }
}