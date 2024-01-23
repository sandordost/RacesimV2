namespace RaceSimulatorController.Events
{
    public class RaceChangedEventArgs(Race race) : EventArgs
    {
        public Race Race { get; set; } = race;
    }
}