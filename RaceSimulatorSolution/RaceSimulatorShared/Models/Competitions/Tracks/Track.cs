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
            Direction currentDirection = Direction.Right;
            foreach (SectionType sectionType in sectionTypes)
            {
                var section = new Section(sectionType, currentDirection, maxSectionProgression);
                Sections.AddLast(section);
                currentDirection = DetectDirectionChange(sectionType, currentDirection);
            }
        }

        private static Direction DetectDirectionChange(SectionType sectionType, Direction currentDirection)
        {
            if(sectionType == SectionType.LeftCorner)
            {
                switch (currentDirection)
                {
                    case Direction.Right:
                        return Direction.Up;
                    case Direction.Up:
                        return Direction.Left;
                    case Direction.Left:
                        return Direction.Down;
                    case Direction.Down:
                        return Direction.Right;
                }
            }
            else if(sectionType == SectionType.RightCorner)
            {
                switch (currentDirection)
                {
                    case Direction.Right:
                        return Direction.Down;
                    case Direction.Up:
                        return Direction.Right;
                    case Direction.Left:
                        return Direction.Up;
                    case Direction.Down:
                        return Direction.Left;
                }
            }

            return currentDirection;
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
        }

        private void AdvanceParticipantsInSection(LinkedListNode<Section> currentSectionNode, Dictionary<(IParticipant, int), Section> participantsToBeMovedFromSection)
        {
            if (currentSectionNode == null)
                throw new Exception("Track has no sections.");

            foreach ((IParticipant participant, int remainingDistance) in currentSectionNode.Value.AdvanceParticipants())
            {
                if (remainingDistance < 0)
                {
                    var nextSection = currentSectionNode.Next ?? Sections.First ?? throw new Exception("Track has no next section.");
                    participantsToBeMovedFromSection.Add((participant, remainingDistance), nextSection.Value);
                }
            }
        }
    }
}