using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks.Events;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorShared.Models.Competitions.Tracks
{
    public class Track
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
            foreach (SectionType sectionType in sectionTypes)
            {
                var section = new Section(sectionType, maxSectionProgression);
                Sections.AddLast(section);
            }
        }

        public void PlaceParticipantsOnStart(List<IParticipant> participants)
        {
            var firstSection = Sections.First ?? throw new Exception("Track has no sections.");

            foreach (IParticipant participant in participants)
                firstSection.Value.PlaceParticipant(participant);
        }

        public void AdvanceParticipantsInAllSections()
        {
            LinkedListNode<Section>? currentSection = Sections.First ?? throw new Exception("Track has no sections.");
            Dictionary<(IParticipant, int), Section> participantsToBeMoved = [];

            while (currentSection != null)
            {
                AdvanceParticipantsInSection(currentSection, participantsToBeMoved);
                currentSection = currentSection.Next;
            }

            foreach (var participantToBeMoved in participantsToBeMoved)
            {
                participantToBeMoved.Value.PlaceParticipant(participantToBeMoved.Key.Item1, Math.Abs(participantToBeMoved.Key.Item2));
            }

            TrackEvents.InvokeTrackAdvanced(this, new TrackAdvancedEventArgs(this));
        }

        private void AdvanceParticipantsInSection(LinkedListNode<Section> currentSectionNode, Dictionary<(IParticipant, int), Section> participantsToBeMovedFromSection)
        {
            if (currentSectionNode == null)
                throw new Exception("Track has no sections.");

            foreach ((IParticipant, int) participantRemainingDistance in currentSectionNode.Value.AdvanceParticipants())
            {
                if (participantRemainingDistance.Item2 < 0)
                {
                    var nextSection = currentSectionNode.Next ?? Sections.First ?? throw new Exception("Track has no next section.");
                    participantsToBeMovedFromSection.Add(participantRemainingDistance, nextSection.Value);
                }
            }
        }
    }
}