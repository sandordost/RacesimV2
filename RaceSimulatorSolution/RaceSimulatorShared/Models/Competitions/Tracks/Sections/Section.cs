using RaceSimulatorShared.Models.Competitions.Participants;

namespace RaceSimulatorShared.Models.Competitions.Tracks.Sections
{
    internal class Section(SectionType sectionType, int maxSectionProgression)
    {
        internal SectionType SectionType { get; } = sectionType;
        internal Dictionary<IParticipant, int> ParticipantSectionProgressions { get; set; } = [];
        private int MaxSectionProgression { get; } = maxSectionProgression;

        internal void PlaceParticipant(IParticipant participant)
        {
            if (!ParticipantSectionProgressions.TryAdd(participant, 0))
                ParticipantSectionProgressions[participant] = 0;
        }

        /// <summary>
        /// Returns the remaining distance the participant has to travel.
        /// </summary>
        internal int MoveParticipant(IParticipant participant, int movementAmount)
        {
            if (!ParticipantSectionProgressions.TryAdd(participant, movementAmount))
                ParticipantSectionProgressions[participant] += movementAmount;

            var remainingMovementAmount = MaxSectionProgression - ParticipantSectionProgressions[participant];

            if (remainingMovementAmount <= 0) 
                ParticipantSectionProgressions.Remove(participant);

            return remainingMovementAmount;
        }

        internal void AdvanceParticipants()
        {
            foreach (IParticipant participant in ParticipantSectionProgressions.Keys)
                MoveParticipant(participant, SectionType.GetMovementAmount(participant));
        }
    }
}
