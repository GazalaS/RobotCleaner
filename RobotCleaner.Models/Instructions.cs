namespace RobotCleaner.Models
{
    public class Instructions
    {
        public int NumberOfCommands { get; set; }
        public Position StartPosition { get; set; }
        public SortedList<Direction, int> CleaningDirections { get; set; }

        public Instructions()
        {
            CleaningDirections = new SortedList<Direction, int>();
        }
    }

}