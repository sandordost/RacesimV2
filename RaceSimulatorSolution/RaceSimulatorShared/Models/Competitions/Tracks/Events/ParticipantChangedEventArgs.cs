using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorShared.Models.Competitions.Tracks.Events
{
    public class ParticipantChangedEventArgs : EventArgs
    {
        public KeyValuePair<IParticipant, Section> ParticipantNewSection { get; }
    }
}