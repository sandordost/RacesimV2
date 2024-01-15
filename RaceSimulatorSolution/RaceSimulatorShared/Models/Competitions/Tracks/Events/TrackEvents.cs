namespace RaceSimulatorShared.Models.Competitions.Tracks.Events
{
    public static class TrackEvents
    {
        public static event EventHandler<TrackAdvancedEventArgs>? TrackAdvanced;
        public static void InvokeTrackAdvanced(object? sender, TrackAdvancedEventArgs eventArgs) => TrackAdvanced?.Invoke(null, eventArgs);

        public static event EventHandler<ParticipantChangedEventArgs>? ParticipantChanged;
        public static void InvokeParticipantChanged(object? sender, ParticipantChangedEventArgs eventArgs) => ParticipantChanged?.Invoke(null, eventArgs);
    }
}
