using RaceSimulatorShared.Models.Competitions.Tracks;
using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorConsole.Tools
{
    internal class TrackPrinter
    {
        readonly static int _sectionWidth = 7;
        readonly static int _sectionHeight = 5;

        private static readonly Dictionary<(SectionType, Direction), string[]> _sectionPrints = new()
            {
                {(SectionType.Start, Direction.Right), new string[]
                    {
                        "-------",
                        "  1|   ",
                        "   |   ",
                        " 2 |   ",
                        "-------"
                    }
                },
                {(SectionType.Start, Direction.Left), new string[]
                    {
                        "-------",
                        "   | 2 ",
                        "   |   ",
                        "   |1  ",
                        "-------"
                    }
                },
                {(SectionType.Start, Direction.Up), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|1---2|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.Start, Direction.Down), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|2---1|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.Straight, Direction.Right), new string[]
                    {
                        "-------",
                        "   1   ",
                        "       ",
                        "   2   ",
                        "-------"
                    }
                },
                {(SectionType.Straight, Direction.Left), new string[]
                    {
                        "-------",
                        "   2   ",
                        "       ",
                        "   1   ",
                        "-------"
                    }
                },
                {(SectionType.Straight, Direction.Up), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|1   2|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.Straight, Direction.Down), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|2   1|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.Finish, Direction.Right), new string[]
                    {
                        "-------",
                        "  1#   ",
                        "   #   ",
                        "   #2  ",
                        "-------"
                    }
                },
                {(SectionType.Finish, Direction.Left), new string[]
                    {
                        "-------",
                        "  2#   ",
                        "   #   ",
                        "   #1  ",
                        "-------"
                    }
                },
                {(SectionType.Finish, Direction.Up), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|1# #2|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.Finish, Direction.Down), new string[]
                    {
                        "|     |",
                        "|     |",
                        "|2# #1|",
                        "|     |",
                        "|     |"
                    }
                },
                {(SectionType.LeftCorner, Direction.Right), new string[]
                    {
                        "|     |",
                        " 1    |",
                        "      |",
                        "     2|",
                        "-------"
                    }
                },
                {(SectionType.LeftCorner, Direction.Left), new string[]
                    {
                        "-------",
                        "|2     ",
                        "|      ",
                        "|    1 ",
                        "|     |"
                    }
                },
                {(SectionType.LeftCorner, Direction.Up), new string[]
                    {
                        "-------",
                        "     2|",
                        "      |",
                        " 1    |",
                        "|     |"
                    }
                },
                {(SectionType.LeftCorner, Direction.Down), new string[]
                    {
                        "|     |",
                        "|    2 ",
                        "|      ",
                        "|1     ",
                        "-------"
                    }
                },
                {(SectionType.RightCorner, Direction.Right), new string[]
                    {
                        "-------",
                        "     2|",
                        "      |",
                        " 1    |",
                        "|     |"
                    }
                },
                {(SectionType.RightCorner, Direction.Left), new string[]
                    {

                        "|     |",
                        "|    2 ",
                        "|      ",
                        "|1     ",
                        "-------"
                    }
                },
                {(SectionType.RightCorner, Direction.Up), new string[]
                    {
                        "-------",
                        "|2     ",
                        "|      ",
                        "|    1 ",
                        "|     |"
                    }
                },
                {(SectionType.RightCorner, Direction.Down), new string[]
                    {
                        "|     |",
                        " 1    |",
                        "      |",
                        "     2|",
                        "-------"
                    }
                }
            };

        public static void PrintSection(Section section, int mousePosX, int mousePosY)
        {
            string[] sectionPrint = _sectionPrints[(section.SectionType, section.Direction)];

            var furthestParticipantsOnSection = section.ParticipantSectionProgressions
                        .OrderByDescending(x => x.Value)
                        .Take(2)
                        .ToList();

            foreach (string line in sectionPrint)
            {
                char firstParticipantSymbol = ' ';
                if (furthestParticipantsOnSection.Count > 0)
                {
                    firstParticipantSymbol = furthestParticipantsOnSection[0].Key.Equipment.IsBroken
                        ? '!'
                        : furthestParticipantsOnSection[0].Key.Name[0];
                }

                char secondParticipantSymbol = ' ';
                if (furthestParticipantsOnSection.Count > 1)
                {
                    secondParticipantSymbol = furthestParticipantsOnSection[1].Key.Equipment.IsBroken
                        ? '!'
                        : furthestParticipantsOnSection[1].Key.Name[0];
                }

                var finalPrint = line.Replace('1', firstParticipantSymbol)
                                     .Replace('2', secondParticipantSymbol);

                Console.SetCursorPosition(mousePosX, mousePosY++);
                Console.WriteLine(finalPrint);
            }
        }


        public static void PrintTrack(Track track)
        {
            Console.Clear();
            int currentX = 0;
            int currentY = 0;

            (int startX, int startY) = GetTrackStartPosition(track);

            int adjustedStartX = startX < 0 ? Math.Abs(startX) : 0;
            int adjustedStartY = startY < 0 ? Math.Abs(startY) : 0;

            foreach (Section section in track.Sections)
            {
                switch (section.Direction)
                {
                    case Direction.Right:
                        currentX += _sectionWidth;
                        break;
                    case Direction.Left:
                        currentX -= _sectionWidth;
                        break;
                    case Direction.Up:
                        currentY -= _sectionHeight;
                        break;
                    case Direction.Down:
                        currentY += _sectionHeight;
                        break;
                }

                PrintSection(section, adjustedStartX + currentX, adjustedStartY + currentY);
            }
        }

        private static (int startX, int startY) GetTrackStartPosition(Track track)
        {
            int minX = 0;
            int minY = 0;
            int currentX = 0;
            int currentY = 0;

            foreach (Section section in track.Sections)
            {
                // Assuming each section is 7 characters wide and 5 lines tall
                switch (section.Direction)
                {
                    case Direction.Right:
                        currentX += _sectionWidth;
                        break;
                    case Direction.Left:
                        currentX -= _sectionWidth;
                        minX = Math.Min(minX, currentX);
                        break;
                    case Direction.Up:
                        currentY -= _sectionHeight;
                        minY = Math.Min(minY, currentY);
                        break;
                    case Direction.Down:
                        currentY += _sectionHeight;
                        break;
                }
            }

            return (minX, minY);
        }
    }
}
