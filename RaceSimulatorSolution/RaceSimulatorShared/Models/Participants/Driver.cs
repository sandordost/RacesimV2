﻿using RaceSimulatorShared.Models.Equipments;

namespace RaceSimulatorShared.Models.Participants;

public class Driver(string name, IEquipment equipment, TeamColor teamColor) : IParticipant
{
    public string Name { get; } = name;
    public int Points { get; set; } = 0;
    public IEquipment Equipment { get; set; } = equipment;
    public TeamColor TeamColor { get; set; } = teamColor;
}
