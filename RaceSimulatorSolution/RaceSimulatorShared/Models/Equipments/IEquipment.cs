namespace RaceSimulatorShared.Models.Equipments;

public interface IEquipment
{
    public int Quality { get; set; }
    public int Performance { get; set; }
    public int Speed { get; set; }
    public bool IsBroken { get; set; }
    public int Damage { get; set; }
    public bool TryBreak();
    public bool TryRepair();
}
