using RaceSimulatorShared.Models.Competitions.Participants;

namespace RaceSimulatorShared.Models.Competitions.Tracks.Sections
{
    public class Section(SectionType sectionType, int maxSectionProgression)
    {
        public SectionType SectionType { get; } = sectionType;
        internal Dictionary<IParticipant, int> ParticipantSectionProgressions { get; set; } = [];
        private int MaxSectionProgression { get; } = maxSectionProgression;

        internal void PlaceParticipant(IParticipant participant, int sectionProgression = 0)
        {
            if (!ParticipantSectionProgressions.TryAdd(participant, sectionProgression))
                ParticipantSectionProgressions[participant] = sectionProgression;
        }

        /// <summary>
        /// Returns the remaining distance the participant has to travel. 
        /// Returns < 0 if the participant has reached further then the end of the section, then removes the participant from the section.
        /// </summary>
        internal int MoveParticipant(IParticipant participant, int movementAmount)
        {
            if(!ParticipantSectionProgressions.TryGetValue(participant, out int sectionProgression))
                throw new Exception("Participant is not on this section.");

            var newSectionProgression = sectionProgression + movementAmount;

            if (newSectionProgression > MaxSectionProgression)
            {
                ParticipantSectionProgressions.Remove(participant);
                return MaxSectionProgression - newSectionProgression;
            }
            else
            {
                ParticipantSectionProgressions[participant] = newSectionProgression;
                return MaxSectionProgression - newSectionProgression;
            }
        }

        internal IEnumerable<(IParticipant, int)> AdvanceParticipants()
        {
            foreach (IParticipant participant in ParticipantSectionProgressions.Keys)
                yield return (participant, MoveParticipant(participant, participant.Equipment.Speed));
        }
    }
}
