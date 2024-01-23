using RaceSimulatorController;
using RaceSimulatorController.Events;
using RaceSimulatorShared.Models.Tracks.Events;
using System.Windows;

namespace RaceSimulatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RaceDataContext CurrentRaceContext { get; set; } = new(null);
        public string TestString { get; set; } = "Testingg";

        public MainWindow()
        {
            InitializeComponent();
            Data.RaceChanged += Data_RaceChanged;
            Data.Initialize();
            DataContext = CurrentRaceContext;
        }

        private void Data_RaceChanged(object? sender, RaceChangedEventArgs e)
        {
            CurrentRaceContext.CurrentRace = e.Race;
            CurrentRaceContext.CurrentRace.Track.TrackEventsManager.TrackAdvanced += TrackEventsManager_TrackAdvanced;
            CurrentRaceContext.CurrentRace.Track.TrackEventsManager.ParticipantLapped += TrackEventsManager_ParticipantLapped;
            CurrentRaceContext.PreviousRace = Data.FinishedRaces.LastOrDefault();
        }

        private void TrackEventsManager_ParticipantLapped(object? sender, ParticipantLappedEventArgs e)
        {
            CurrentRaceContext.Scores = CurrentRaceContext.CurrentRace?.GetOrderedScores().ToDictionary() ?? [];
        }

        private void TrackEventsManager_TrackAdvanced(object? sender, TrackAdvancedEventArgs e)
        {
            CurrentRaceContext.Distances = e.Track.GetDistances();
        }
    }
}