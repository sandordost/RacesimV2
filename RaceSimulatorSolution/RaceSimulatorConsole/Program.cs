// See https://aka.ms/new-console-template for more information
using RaceSimulatorShared.Models.Competitions;
using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

Console.WriteLine("Hello, World!");

Competition competition = new();

var driver1 = new Driver("Driver1", new Car(), TeamColor.Green);
var driver2 = new Driver("Driver2", new Car(), TeamColor.Red);
var driver3 = new Driver("Driver3", new Car(), TeamColor.Blue);

competition.Participants = new List<IParticipant>() { driver1, driver2, driver3 };

var track1 = new Track("Track1", [SectionType.Start, SectionType.Straight, SectionType.LeftCorner, SectionType.Straight, SectionType.Finish], 200);
var track2 = new Track("Track2", [SectionType.Start, SectionType.Straight, SectionType.RightCorner, SectionType.Straight, SectionType.Finish], 200);

competition.Tracks.Enqueue(track1);
competition.Tracks.Enqueue(track2);