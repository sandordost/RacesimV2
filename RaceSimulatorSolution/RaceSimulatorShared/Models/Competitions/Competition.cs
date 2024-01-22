using RaceSimulatorShared.Models.Competitions.Participants;
using RaceSimulatorShared.Models.Competitions.Tracks;

namespace RaceSimulatorShared.Models.Competitions
{
    public class Competition
    {
        public List<IParticipant> Participants { get; set; } = [];
        public Queue<Track> Tracks { get; set; } = [];

        public Track? TakeNextTrack() => Tracks.Count > 0 ? Tracks.Dequeue() : null;

        public void PlaceParticipantsOnTrack(Track track)
        {
            if (track.Sections.First == null)
                throw new Exception("Track has no sections.");

            foreach (IParticipant participant in Participants)
                track.Sections.First.Value.PlaceParticipant(participant);
        }
    }
}
