﻿using Plus.HabboHotel.GameClients;

namespace Plus.Communication.Packets.Incoming.Misc;

internal class EventTrackerEvent : IPacketEvent
{
    public Task Parse(GameClient session, IIncomingPacket packet) => Task.CompletedTask;
}