using RaceSimulatorConsole.Tools;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorConsole
{
    internal class TrackVisualizer
    {
        public static void ShowTrack(Track track)
        {
            TrackPrinter.PrintTrack(track);
        }
    }
}
