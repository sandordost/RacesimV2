using RaceSimulatorShared.Models.Competitions.Equipments;

namespace RaceSimulatorShared.Models.Competitions.Participants
{
    internal class Driver(string name, IEquipment equipment, TeamColor teamColor) : IParticipant
    {
        public string Name { get; } = name;
        public int Points { get; set; } = 0;
        public IEquipment Equipment { get; set; } = equipment;
        public TeamColor TeamColor { get; set; } = teamColor;
    }
}
