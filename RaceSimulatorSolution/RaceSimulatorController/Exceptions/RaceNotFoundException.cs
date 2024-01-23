using RaceSimulatorShared.Models.Competitions;

namespace RaceSimulatorController.Exceptions
{
    public class RaceNotFoundException(Competition competition) : Exception
    {
        public override string Message => "Race not found";
        public Competition Competition { get; set; } = competition;
    }
}
