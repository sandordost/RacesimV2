using RaceSimulatorController.Events;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks;
using RaceSimulatorShared.Models.Tracks.Events;
using System.Diagnostics;

namespace RaceSimulatorController
{
    public class Race(Track track, int maxLaps)
    {
        public Track Track { get; set; } = track;
        public int MaxLaps { get; set; } = maxLaps;
        public event EventHandler<RaceFinishedEventArgs>? RaceFinished;
        public Dictionary<IParticipant, Score> Scores { get; set; } = [];
        public IParticipant? WinningParticipant { get => GetOrderedScores().FirstOrDefault().Key; }
        System.Timers.Timer? timer;
        readonly Stopwatch stopwatch = new();

        public void Start()
        {
            timer = new(500);
            timer.Elapsed += Timer_Elapsed;
            stopwatch.Start();
            timer.Start();
            Track.TrackEventsManager.ParticipantLapped += Track_ParticipantLapped;
        }

        private void Track_ParticipantLapped(object? sender, ParticipantLappedEventArgs e)
        {
            Scores.TryGetValue(e.Participant, out Score? score);
            if (score == null)
            {
                score = new(e.Laps ?? 0, stopwatch.Elapsed.Seconds);
                Scores.Add(e.Participant, score);
            }
            else
            {
                score.TimeElapsed = stopwatch.Elapsed.Seconds;
                score.Laps = e.Laps ?? 0;
            }

            if (e.Laps >= MaxLaps)
                Track.RemoveParticipantFromSections(e.Participant);

            if (!Track.HasActiveParticipants())
            {
                RaceFinished?.Invoke(this, new RaceFinishedEventArgs(this));
            }
        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            Track.AdvanceParticipantsInAllSections();
        }

        public IEnumerable<KeyValuePair<IParticipant, Score>> GetOrderedScores()
        {
            return Scores
                .OrderByDescending(kvp => kvp.Value.Laps)
                .ThenBy(kvp => kvp.Value.TimeElapsed);
        }

        public void Dispose()
        {
            Track.TrackEventsManager.ClearEvents();
            timer?.Stop();
            timer?.Dispose();
            stopwatch.Stop();
        }
    }
}
