using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks.Events;
using RaceSimulatorShared.Models.Tracks.Sections;

namespace RaceSimulatorShared.Models.Tracks;

public class Track
{
    public TrackEventsManager TrackEventsManager { get; set; } = new();
    public string Name { get; }
    public LinkedList<Section> Sections { get; set; } = [];
    public Dictionary<IParticipant, int> Laps { get; set; } = [];

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

    public static Direction DetectDirectionChange(SectionType sectionType, Direction currentDirection)
    {
        if (sectionType == SectionType.LeftCorner)
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
        else if (sectionType == SectionType.RightCorner)
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

            // Participant has lapped
            if (participantToBeMoved.Value.SectionType == SectionType.Start)
            {
                InvokeParticipantLapped(participantToBeMoved.Key.Item1);
            }
        }

        TrackEventsManager.InvokeTrackAdvanced(this, new TrackAdvancedEventArgs(this));
    }

    public void InvokeParticipantLapped(IParticipant participant)
    {
        if (Laps.TryGetValue(participant, out int value))
            Laps[participant] = ++value;
        else
            Laps.Add(participant, 1);

        TrackEventsManager.InvokeParticipantLapped(this, new ParticipantLappedEventArgs(participant, Laps[participant]));
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

    public void RemoveParticipantFromSections(IParticipant participant)
    {
        foreach (var section in Sections)
            section.ParticipantSectionProgressions.Remove(participant);
    }

    public bool HasActiveParticipants()
    {
        foreach (var section in Sections)
        {
            if (section.ParticipantSectionProgressions.Count > 0)
                return true;
        }

        return false;
    }

    public Dictionary<IParticipant, int> GetDistances()
    {
        var distances = new Dictionary<IParticipant, int>();
        var currentDistance = 0;

        foreach (var section in Sections)
        {
            foreach (var participantSectionProgression in section.ParticipantSectionProgressions)
            {
                if (!distances.TryAdd(participantSectionProgression.Key, currentDistance + participantSectionProgression.Value))
                {
                    throw new Exception("Participant was found twice in track.");
                }
            }

            currentDistance += section.MaxSectionProgression;
        }

        return distances;
    }

    public int CalculateTrackLength()
    {
        var trackLength = 0;
        LinkedListNode<Section>? currentSection = Sections.First;
        while (currentSection != null)
        {
            trackLength += currentSection.Value.MaxSectionProgression;
            currentSection = currentSection.Next;
        }
        return trackLength;
    }
}