using RaceSimulatorShared.Models.Competitions.Tracks.Sections;

namespace RaceSimulatorConsole
{
    internal class SectionPrinter
    {
        private readonly Dictionary<SectionType, string[]> sectionPrints = new()
        {
            { 
                SectionType.Start, ["====", 
                                    "    ", 
                                    "    ", 
                                    "===="]
            },
            {
                SectionType.Straight,  ["----",
                                        "    ",
                                        "    ",
                                        "----"]
            },
            {
                SectionType.LeftCorner, ["   |",
                                         "   |",
                                         "   |",
                                         "----"]
            },
            {
                SectionType.RightCorner, ["----",
                                                         "\\   ",
                                                         " \\  ",
                                                         "----"]
            },
            {
                SectionType.Finish, ["====",
                                                   "    ",
                                                   "    ",
                                                   "===="]
            },
        };

        public void PrintSection(Section section)
        {
            string[] sectionPrint = sectionPrints[section.SectionType];
            foreach (string line in sectionPrint)
            {
                Console.WriteLine(line);
            }
        }
    }
}
