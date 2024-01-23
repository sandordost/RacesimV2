using RaceSimulatorShared.Models.Competitions;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorTests.RaceSimulatorShared
{
    public class CompetitionTests
    {
        [Fact]
        public void TakeNextTrack_ShouldReturnNextTrack()
        {
            Competition competition = new();
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            competition.Tracks.Enqueue(new Track("testTrack", sections, 100));

            Assert.True(competition.Tracks.Count == 1);
            Assert.True(competition.TakeNextTrack() is not null);
        }

        [Fact]
        public void TakeNextTrack_ShouldReturnNull()
        {
            Competition competition = new();

            Assert.True(competition.Tracks.Count == 0);
            Assert.True(competition.TakeNextTrack() is null);
        }
    }
}