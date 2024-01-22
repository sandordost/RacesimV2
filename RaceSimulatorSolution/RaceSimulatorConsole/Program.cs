using RaceSimulatorConsole;
using RaceSimulatorConsole.Tools;
using RaceSimulatorShared.Models.Competitions;
using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

Competition competition = new();

var driver1 = new Driver("Sandor", new Car(), TeamColor.Green);
var driver2 = new Driver("Klaas", new Car(), TeamColor.Red);
var driver3 = new Driver("Johan", new Car(), TeamColor.Blue);

competition.Participants = [driver1, driver2, driver3];

var track1 = new Track("Track1", [SectionType.Start, SectionType.Straight, SectionType.LeftCorner, SectionType.LeftCorner, SectionType.Straight, SectionType.Straight, SectionType.Straight, SectionType.LeftCorner, SectionType.LeftCorner, SectionType.Finish], 200);
var track2 = new Track("Track2", [SectionType.Start, SectionType.Straight, SectionType.RightCorner, SectionType.Straight, SectionType.Finish], 200);

competition.Tracks.Enqueue(track1);
competition.Tracks.Enqueue(track2);

Track? track = competition.TakeNextTrack() ?? throw new Exception("No tracks available.");
competition.PlaceParticipantsOnTrack(track);

Race race = new(track);
race.trackEventsManager.TrackAdvanced += TrackEventsManager_TrackAdvanced;

void TrackEventsManager_TrackAdvanced(object? sender, RaceSimulatorShared.Models.Competitions.Tracks.Events.TrackAdvancedEventArgs e)
{
    TrackVisualizer.ShowTrack(e.Track);
}

race.Start();

Console.ReadLine();