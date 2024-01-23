using RaceSimulatorShared.Models.Equipments;

namespace RaceSimulatorShared.Models.Participants;

public interface IParticipant
{
    public string Name { get; }
    public int Points { get; set; }
    public IEquipment Equipment { get; set; }
    public TeamColor TeamColor { get; set; }
}
