using RaceSimulatorShared.Models.Competitions.Participants;

namespace RaceSimulatorShared.Models.Competitions.Tracks.Events
{
    public class ParticipantLappedEventArgs(IParticipant participant, int? laps = null) : EventArgs()
    {
        public IParticipant Participant { get; } = participant;
        public int? Laps { get; set; } = laps;
    }
}