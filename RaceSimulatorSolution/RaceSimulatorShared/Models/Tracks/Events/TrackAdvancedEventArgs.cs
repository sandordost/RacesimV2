namespace RaceSimulatorShared.Models.Tracks.Events;

public class TrackAdvancedEventArgs(Track track) : EventArgs
{
    public Track Track { get; } = track;
}