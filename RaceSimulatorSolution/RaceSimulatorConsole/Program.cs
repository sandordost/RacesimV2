using RaceSimulatorConsole;
using RaceSimulatorController;
using RaceSimulatorController.Events;
using RaceSimulatorShared.Models.Tracks.Events;

Race currentRace;
Data.RaceChanged += Data_RaceChanged;
Data.Initialize();

void Data_RaceChanged(object? sender, RaceChangedEventArgs e)
{
    currentRace = e.Race;
    e.Race.Track.TrackEventsManager.TrackAdvanced += TrackEventsManager_TrackAdvanced;
}

void TrackEventsManager_TrackAdvanced(object? sender, TrackAdvancedEventArgs e)
{
    if(Console.WindowHeight <= 40 || Console.WindowWidth <= 50)
    {
        Console.Clear();
        Console.SetCursorPosition(0, Console.WindowHeight / 2);
        Console.Write("Please increase your window height and width ...");
    }
    else
    {
        int[] offset = [4, 11];
        TrackVisualizer.ShowTrack(e.Track, offset);
        TrackVisualizer.ShowScoreAndTrackInformation(currentRace.GetOrderedScores().ToDictionary(), e.Track, Data.FinishedRaces.LastOrDefault());
    }

    Console.SetCursorPosition(0, Console.WindowHeight - 1);
    Console.Write("Press any key to close the simulation");
}

Console.ReadKey();