using RaceSimulatorConsole.Tools;
using RaceSimulatorController;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks;

namespace RaceSimulatorConsole;

internal class TrackVisualizer
{
    public static void ShowTrack(Track track, int[] offset)
    {
        TrackPrinter.PrintTrack(track, offset);
    }

    public static void ShowScoreAndTrackInformation(Dictionary<IParticipant, Score> scores, Track track, Race? previousRace)
    {
        if (previousRace != null)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"----------- Previous Winner: {previousRace.WinningParticipant?.Name} -----------");
        }

        for (int i = 0; i < scores.Count; i++)
        {
            Console.SetCursorPosition(0, i + 1);
            Console.Write($"{i+1}. {scores.Keys.ElementAt(i).Name}: {scores.Values.ElementAt(i).Laps} ({scores.Values.ElementAt(i).TimeElapsed}s)");
        }

        Console.SetCursorPosition(0, scores.Count + 2);
        Console.Write($"Track: {track.Name} ({track.CalculateTrackLength()}m)");
    }
}
