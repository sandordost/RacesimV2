using RaceSimulatorController;
using RaceSimulatorShared.Models.Equipments;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks;
using RaceSimulatorShared.Models.Tracks.Sections;

namespace RaceSimulatorTests.RaceSimulatorControllerTests;

public class RaceTests
{
    [Fact]
    public void Race_StartsCorrectly()
    {
        var track = new Track("TestTrack", [SectionType.Start, SectionType.Straight, SectionType.Finish], 100);
        var race = new Race(track, 5);

        race.Start();

        Assert.NotNull(race.Scores);
        Assert.NotNull(race.Track);
        Assert.NotNull(race.Track.Sections.First());
        Assert.Equal("TestTrack", race.Track.Name);
        race.Dispose();
    }

    [Fact]
    public void Participant_AdvancesCorrectly_UpdatesScore()
    {
        var track = new Track("TestTrack", [SectionType.Start, SectionType.Straight, SectionType.Finish], 100);
        var race = new Race(track, 5);
        var participant = new Driver("TestDriver", new Car(), TeamColor.Red);
        track.PlaceParticipantsOnStart([participant]);

        race.Start();
        track.InvokeParticipantLapped(participant);

        Assert.True(race.Scores.ContainsKey(participant));
        Assert.Equal(1, race.Scores[participant].Laps);
    }

    [Fact]
    public void Race_CompletesWhenAllParticipantsFinish()
    {
        var track = new Track("TestTrack", [SectionType.Start, SectionType.Finish], 100);
        var race = new Race(track, 1);
        var participant = new Driver("TestDriver", new Car(100, 100, 100), TeamColor.Red);
        track.PlaceParticipantsOnStart([participant]);

        race.Start();
        bool raceFinished = false;
        race.RaceFinished += (sender, args) => raceFinished = true;

        for(int i = 0; i <130; i++)
        {
            track.AdvanceParticipantsInAllSections();
        }

        Assert.False(track.HasActiveParticipants());
        Assert.True(raceFinished);
    }

    [Fact]
    public void GetOrderedScores_ReturnsScoresInCorrectOrder()
    {
        var track = new Track("TestTrack", [SectionType.Start, SectionType.Straight, SectionType.Finish], 100);
        var race = new Race(track, 5);
        var participant1 = new Driver("TestDriver", new Car(30, 30, 30), TeamColor.Red); // Bad driver
        var participant2 = new Driver("TestDriver2", new Car(100, 100, 100), TeamColor.Blue); // Good driver
        track.PlaceParticipantsOnStart([participant1, participant2]);

        race.Start();
        for (int i = 0; i < 200; i++)
        {
            track.AdvanceParticipantsInAllSections();
        }

        var orderedScores = race.GetOrderedScores().ToList();
        Assert.Equal(participant2, orderedScores[0].Key); // Good driver (participant2) should be first
        Assert.Equal(participant1, orderedScores[1].Key);
    }
}
