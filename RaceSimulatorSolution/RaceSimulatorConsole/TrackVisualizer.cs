using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorConsole
{
    internal class TrackVisualizer
    {
        public void ShowTrack(Track track)
        {
            foreach(Section section in track.Sections)
            {
                PrintSection(section);
            }
        }

        private void PrintSection(Section section)
        {
            switch (section.SectionType)
            {
                case SectionType.Start:
                    PrintStart();
                    break;
                case SectionType.Straight:
                    PrintStraight();
                    break;
                case SectionType.LeftCorner:
                    PrintLeftCorner();
                    break;
                case SectionType.RightCorner:
                    PrintRightCorner();
                    break;
                case SectionType.Finish:
                    PrintFinish();
                    break;
            }
        }

        private void PrintFinish()
        {
            
        }

        private void PrintRightCorner()
        {
            throw new NotImplementedException();
        }

        private void PrintLeftCorner()
        {
            throw new NotImplementedException();
        }

        private void PrintStraight()
        {
            throw new NotImplementedException();
        }

        private void PrintStart()
        {
            throw new NotImplementedException();
        }
    }
}
