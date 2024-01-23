namespace RaceSimulatorShared.Models.Competitions.Tracks.Events
{
    public class TrackEventsManager
    {
        public event EventHandler<TrackAdvancedEventArgs>? TrackAdvanced;
        public void InvokeTrackAdvanced(object? sender, TrackAdvancedEventArgs eventArgs) => TrackAdvanced?.Invoke(null, eventArgs);

        public event EventHandler<ParticipantChangedEventArgs>? ParticipantChanged;
        public void InvokeParticipantChanged(object? sender, ParticipantChangedEventArgs eventArgs) => ParticipantChanged?.Invoke(null, eventArgs);

        public event EventHandler<ParticipantLappedEventArgs>? ParticipantLapped;
        public void InvokeParticipantLapped(object? sender, ParticipantLappedEventArgs eventArgs) => ParticipantLapped?.Invoke(null, eventArgs);

        public void ClearEvents()
        {
            TrackAdvanced = null;
            ParticipantChanged = null;
            ParticipantLapped = null;
        }
    }
}
