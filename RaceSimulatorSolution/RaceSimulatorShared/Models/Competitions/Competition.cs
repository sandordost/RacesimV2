using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;

namespace RaceSimulatorShared.Models.Competitions
{
    internal class Competition
    {
        public List<IParticipant> Participants { get; set; } = [];
        public Queue<Track> Tracks { get; set; } = [];

        public Track? TakeNextTrack() => Tracks.Count > 0 ? Tracks.Dequeue() : null;
    }
}
