using RaceSimulatorShared.Models.Competitions.Equipments;
using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Events;
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
        public void PlaceParticipantsOnStart_ShouldPlaceParticipantsOnFirstSection()
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
            track.PlaceParticipantsOnStart(participants);
            Assert.True(track.Sections.First.Value.ParticipantSectionProgressions.Count == 3);
        }

        [Fact]
        public void AdvanceParticipantsInAllSections_ShouldAdvanceParticipants()
        {
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            Track track = new("testTrack", sections, 100);

            var participants = new List<IParticipant>()
            {
                new Driver("testDriver1", new Car(100, 100, 51), TeamColor.Green),
                new Driver("testDriver2", new Car(100, 100, 51), TeamColor.Grey),
                new Driver("testDriver3", new Car(100, 100, 51), TeamColor.Red)
            };

            track.PlaceParticipantsOnStart(participants);

            track.AdvanceParticipantsInAllSections();
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3);

            track.AdvanceParticipantsInAllSections();
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
            Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 3);
        }

        [Fact]
        public void AdvanceParticipantsInAllSections_ShouldInvokeTrackAdvancedEvent()
        {
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            Track track = new("testTrack", sections, 100);

            var participants = new List<IParticipant>()
            {
                new Driver("testDriver1", new Car(100, 100, 51), TeamColor.Green),
                new Driver("testDriver2", new Car(100, 100, 51), TeamColor.Grey),
                new Driver("testDriver3", new Car(100, 100, 51), TeamColor.Red)
            };

            track.PlaceParticipantsOnStart(participants);

            bool trackAdvancedEventInvoked = false;
            TrackEvents.TrackAdvanced += (sender, args) => trackAdvancedEventInvoked = true;

            track.AdvanceParticipantsInAllSections();
            Assert.True(trackAdvancedEventInvoked);
        }

        [Fact]
        public void AdvanceParticipantsInAllSections_ShouldPlaceParticipantsOnStartAfterFinish()
        {
            var sections = new SectionType[] { SectionType.Start, SectionType.Finish };
            Track track = new("testTrack", sections, 100);
            var speed = 51;

            var participants = new List<IParticipant>()
            {
                new Driver("testDriver1", new Car(100, 100, speed), TeamColor.Green),
                new Driver("testDriver2", new Car(100, 100, speed), TeamColor.Grey),
                new Driver("testDriver3", new Car(100, 100, speed), TeamColor.Red)
            };

            track.PlaceParticipantsOnStart(participants);

            track.AdvanceParticipantsInAllSections();
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.All(progression => progression.Value == speed));
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3);

            track.AdvanceParticipantsInAllSections();
            var newProgression = Math.Abs(100 - speed * 2);
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 0);
            Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.All(progression => progression.Value == newProgression));
            Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 3);

            track.AdvanceParticipantsInAllSections();
            track.AdvanceParticipantsInAllSections();
            Assert.True(track.Sections.First?.Value.ParticipantSectionProgressions.Count == 3); 
            Assert.True(track.Sections.Last?.Value.ParticipantSectionProgressions.Count == 0);
        }
    }
}
