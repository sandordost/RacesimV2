namespace RaceSimulatorShared.Models.Competitions.Tracks.Events
{
    public class TrackAdvancedEventArgs(Track track) : EventArgs
    {
        public Track Track { get; } = track;
    }
}