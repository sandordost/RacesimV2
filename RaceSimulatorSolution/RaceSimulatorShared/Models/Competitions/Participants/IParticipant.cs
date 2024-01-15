using RaceSimulatorShared.Models.Competitions.Equipments;

namespace RaceSimulatorShared.Models.Competitions.Participants
{
    public interface IParticipant
    {
        public string Name { get; }
        public int Points { get; set; }
        public IEquipment Equipment { get; set; }
        public TeamColor TeamColor { get; set; }
    }
}
