using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Events;

namespace RaceSimulatorShared.Models.Competitions
{
    public class Race(Track track)
    {
        public readonly TrackEventsManager trackEventsManager = new();
        Track Track { get; set; } = track;

        public void Start()
        {
            System.Timers.Timer timer = new(1000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Track.AdvanceParticipantsInAllSections();
            trackEventsManager.InvokeTrackAdvanced(this, new TrackAdvancedEventArgs(Track));
        }
    }
}
