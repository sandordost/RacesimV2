namespace RaceSimulatorController.Events
{
    public class RaceFinishedEventArgs(Race race) : EventArgs
    {
        public Race Race { get; set; } = race;
    }
}