using RaceSimulatorController.Events;
using RaceSimulatorController.Exceptions;
using RaceSimulatorShared.Models.Competitions;
using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorController
{
    public static class Data
    {
        public static Competition Competition { get; set; } = new();
        public static Race? CurrentRace { get; set; }
        public static List<Race> FinishedRaces { get; set; } = [];
        public static event EventHandler<RaceChangedEventArgs>? RaceChanged;
        public static void InvokeRaceChanged(object? sender, RaceChangedEventArgs eventArgs) => RaceChanged?.Invoke(null, eventArgs);

        public static void Initialize()
        {
            var driver1 = new Driver("Sandor", new Car(90,90,90), TeamColor.Green);
            var driver2 = new Driver("Klaas", new Car(), TeamColor.Red);
            var driver3 = new Driver("Johan", new Car(), TeamColor.Blue);
            var driver4 = new Driver("Peter", new Car(), TeamColor.Yellow);
            var driver5 = new Driver("Harry", new Car(), TeamColor.Grey);

            Competition.Participants = [driver1, driver2, driver3, driver4, driver5];

            var track1 = new Track("Zandvoort Circuit", 
                    [
                        SectionType.Start,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.LeftCorner,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.LeftCorner, 
                        SectionType.Straight, 
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.LeftCorner,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.LeftCorner, 
                        SectionType.Finish
                    ], 
                100);
            var track2 = new Track("Dubai Airport Circuit", 
                    [
                        SectionType.Start, 
                        SectionType.Straight, 
                        SectionType.RightCorner, 
                        SectionType.Straight, 
                        SectionType.LeftCorner, 
                        SectionType.Straight, 
                        SectionType.LeftCorner, 
                        SectionType.Straight, 
                        SectionType.Straight,
                        SectionType.LeftCorner,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.Straight,
                        SectionType.LeftCorner,
                        SectionType.LeftCorner,
                        SectionType.Finish
                    ], 
                100);

            Competition.Tracks.Enqueue(track1);
            Competition.Tracks.Enqueue(track2);

            StartNextRace();
        }

        private static void CurrentRace_RaceFinished(object? sender, RaceFinishedEventArgs e)
        {
            FinishedRaces.Add(e.Race);
            StartNextRace();
        }

        public static void StartNextRace()
        {
            CurrentRace?.Dispose();

            Track? track = Competition.TakeNextTrack() ?? throw new NoTracksException(Competition);
            Competition.PlaceParticipantsOnTrack(track);
            CurrentRace = new(track, 3);
            CurrentRace.Start();

            CurrentRace.RaceFinished += CurrentRace_RaceFinished;
            InvokeRaceChanged(null, new RaceChangedEventArgs(CurrentRace));
        }
    }
}
