using RaceSimulatorController;
using RaceSimulatorShared.Models.Participants;
using RaceSimulatorShared.Models.Tracks;
using System.ComponentModel;

namespace RaceSimulatorWPF;

public class RaceDataContext(Race? race) : INotifyPropertyChanged
{
    private Race? _currentRace = race;
    private Track? _nextTrack;
    private Dictionary<IParticipant, int> _distances = [];
    private Dictionary<IParticipant, Score> _scores = [];
    private Race? _previousRace;

    private int trackLength = 0;

    public Race? CurrentRace
    {
        get { return _currentRace; }
        set
        {
            if (_currentRace != value)
            {
                _currentRace = value;
                if (_currentRace != null)
                {
                    TrackLength = _currentRace.Track.CalculateTrackLength();
                    Data.Competition.Tracks.TryPeek(out Track? nextTrack);
                    NextTrack = nextTrack;
                }
                OnPropertyChanged(nameof(CurrentRace));
            }
        }
    }

    public Dictionary<IParticipant, int> Distances
    {
        get { return _distances; }
        set
        {
            if (_distances != value)
            {
                _distances = value.OrderBy(x => x.Key.Name).ToDictionary(pair => pair.Key, pair => pair.Value);
                OnPropertyChanged(nameof(Distances));
            }
        }
    }

    public Race? PreviousRace
    {
        get { return _previousRace; }
        set
        {
            if (_previousRace != value)
            {
                _previousRace = value;
                OnPropertyChanged(nameof(PreviousRace));
            }
        }
    }

    public int TrackLength
    {
        get { return trackLength; }
        set
        {
            if (trackLength != value)
            {
                trackLength = value;
                OnPropertyChanged(nameof(TrackLength));
            }
        }
    }

    public Track? NextTrack
    {
        get { return _nextTrack; }
        set
        {
            if (_nextTrack != value)
            {
                _nextTrack = value;
                OnPropertyChanged(nameof(NextTrack));
            }
        }
    }

    public Dictionary<IParticipant, Score> Scores
    {
        get { return _scores; }
        set
        {
            if (_scores != value)
            {
                _scores = value;
                OnPropertyChanged(nameof(Scores));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
