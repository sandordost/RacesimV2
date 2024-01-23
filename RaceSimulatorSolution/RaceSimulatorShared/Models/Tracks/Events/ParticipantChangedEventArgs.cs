using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks.Sections;

namespace RaceSimulatorShared.Models.Tracks.Events;

public class ParticipantChangedEventArgs : EventArgs
{
    public KeyValuePair<IParticipant, Section> ParticipantNewSection { get; }
}