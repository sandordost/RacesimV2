using RaceSimulatorShared.Models.Participants;

namespace RaceSimulatorShared.Models.Tracks.Events;

public class ParticipantLappedEventArgs(IParticipant participant, int? laps = null) : EventArgs()
{
    public IParticipant Participant { get; } = participant;
    public int? Laps { get; set; } = laps;
}