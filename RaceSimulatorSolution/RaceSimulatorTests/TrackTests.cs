using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorTests
{
    public class TrackTests
    {
        [Fact]
        public void Track_ShouldInitializeSections()
        {
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            Track track = new("testTrack", sections, 100);

            Assert.True(track.Sections.Count == 2);
        }

        [Fact]
        public void PlaceParticipants_ShouldPlaceParticipantsOnFirstSection()
        {
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            Track track = new("testTrack", sections, 100);
            var participants = new List<IParticipant>()
            {
                new Driver("testDriver1", new Car(), TeamColor.Green),
                new Driver("testDriver2", new Car(), TeamColor.Grey),
                new Driver("testDriver3", new Car(), TeamColor.Red)
            };

            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
            track.PlaceParticipants(participants);
            Assert.True(track.Sections.First.Value.ParticipantSectionProgressions.Count == 3);
        }
    }
}
