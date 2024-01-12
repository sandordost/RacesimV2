using RaceSimulatorShared.Models.Competitions.Equipments;

namespace RaceSimulatorShared.Models.Competitions.Participants
{
    internal interface IParticipant
    {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColor TeamColor { get; set; }
    }
}
