using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorShared.Models.Competitions.Tracks
{
    internal class Track
    {
        public string Name { get; }
        public LinkedList<Section> Sections { get; set; } = [];

        public Track(string name, SectionType[] sections, int maxSectionProgression)
        {
            Name = name;
            InitializeSections(sections, maxSectionProgression);
        }

        private void InitializeSections(SectionType[] sectionTypes, int maxSectionProgression)
        {
            foreach(SectionType sectionType in sectionTypes)
            {
                var section = new Section(sectionType, maxSectionProgression);
                Sections.AddLast(section);
            }
        }

        public void PlaceParticipants(List<IParticipant> participants)
        {
            var firstSection = Sections.First ?? throw new Exception("Track has no sections.");

            foreach (IParticipant participant in participants)
                firstSection.Value.PlaceParticipant(participant);
        }

        public void AdvanceParticipants()
        {
            foreach (Section section in Sections)
                section.AdvanceParticipants();
        }
    }
}
