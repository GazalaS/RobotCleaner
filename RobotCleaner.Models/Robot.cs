

namespace RobotCleaner.Models
{
    /// <summary>
    /// class for Robot
    /// </summary>
    public class Robot
    {
        /// <summary>
        /// A list of unique locations that are cleaned by Robot
        /// </summary>
        private HashSet<Position> CleanedLocations { get; set; }

        private Position _currentLocation;
        /// <summary>
        /// Get and set current coordinates(x,y) of Robot
        /// </summary>
        public Position CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;

                if (CleanedLocations == null)
                    CleanedLocations = new HashSet<Position>();

                CleanedLocations.Add(CurrentLocation);

            }
        }
        /// <summary>
        /// Create a Robot on specific location
        /// </summary>
        /// <param name="x">X coordinate of robot's start position</param>
        /// <param name="y">Y coordinate of robot's start position</param>
        public Robot(int x, int y)
        {
            CurrentLocation = new Position(x, y);
        }

        /// <summary>
        /// Move the robot in specific direction and defined steps
        /// </summary>
        /// <param name="direction">Moving direction</param>
        /// <param name="step">Moving steps</param>
        public void Move(Direction direction, int step)
        {
            while (step >= 1)
            {
                CurrentLocation = CurrentLocation.GetNeighborLocation(direction);

                step--;
            }
        }

        /// <summary>
        /// Get count of unique locations that are cleaned by Robot
        /// </summary>
        /// <returns>count of unique locations that are cleaned by Robot</returns>
        public int GetCleanedLocations()
        {
            return CleanedLocations.Count;
        }
    }
}
