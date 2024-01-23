namespace RaceSimulatorController
{
    public class Score(int laps, int timeElapsed)
    {
        public int Laps { get; set; } = laps;
        public int TimeElapsed { get; set; } = timeElapsed;
    }
}