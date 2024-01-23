using RaceSimulatorController;
using RaceSimulatorController.Exceptions;

namespace RaceSimulatorTests.RaceSimulatorControllerTests;

public class DataTests
{
    [Fact]
    public void Initialize_SetsUpCompetitionCorrectly()
    {
        Data.Initialize();

        Assert.NotNull(Data.Competition);
        Assert.NotEmpty(Data.Competition.Participants);
        Assert.NotEmpty(Data.Competition.Tracks);
        Assert.NotNull(Data.CurrentRace);
    }

    [Fact]
    public void StartNextRace_StartsAndFinishesRace_CorrectlyUpdatesFinishedRaces()
    {
        Data.Initialize();
        var initialRace = Data.CurrentRace;

        for (int i = 0; i < 200; i++)
        {
            Data.CurrentRace?.Track.AdvanceParticipantsInAllSections();
        }

        var nextRace = Data.CurrentRace;

        Assert.Contains(initialRace, Data.FinishedRaces);
        Assert.NotEqual(initialRace, nextRace);
    }

    [Fact]
    public void StartNextRace_ThrowsException_WhenNoTracksAvailable()
    {
        Data.Initialize();
        while (Data.Competition.Tracks.Count > 0)
        {
            Data.Competition.Tracks.Dequeue();
        }

        var competition = Assert.Throws<NoTracksException>(Data.StartNextRace).Competition;
        Assert.NotNull(competition);
        Assert.Equal(Data.Competition, competition);
    }
}
