using RaceSimulatorShared.Models;

namespace RaceSimulatorController.Exceptions
{
    public class NoTracksException(Competition competition) : Exception
    {
        override public string Message => "Track not found.";
        public Competition Competition { get; set; } = competition;
    }
}
