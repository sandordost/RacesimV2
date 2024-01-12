using RaceSimulatorShared.Models.Competitions.Participants;

namespace RaceSimulatorShared.Models.Competitions.Tracks.Sections
{
    internal class Section(SectionType sectionType, int maxSectionProgression)
    {
        public SectionType SectionType { get; } = sectionType;
        public Dictionary<IParticipant, int> ParticipantSectionProgressions { get; set; } = [];
        public int MaxSectionProgression { get; } = maxSectionProgression;

        /// <summary>
        /// Returns the remaining distance the participant has to travel.
        /// </summary>
        public int MoveParticipant(IParticipant participant, int movementAmount)
        {
            if (!ParticipantSectionProgressions.TryAdd(participant, movementAmount))
                ParticipantSectionProgressions[participant] += movementAmount;

            var remainingMovementAmount = MaxSectionProgression - ParticipantSectionProgressions[participant];

            if (remainingMovementAmount <= 0) 
                ParticipantSectionProgressions.Remove(participant);

            return remainingMovementAmount;
        }
    }
}
