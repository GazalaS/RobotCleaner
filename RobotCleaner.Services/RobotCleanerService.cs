
using RobotCleaner.Models;

namespace RobotCleaner.Services
{
    /// <summary>
    /// An interface of RobotCleanerService for injecting its dependencies
    /// </summary>
    public interface IRobotCleanerService
    {
        /// <summary>
        /// Clean is the main process of cleaning by robot
        /// </summary>
        void Clean();
    }
    /// <summary>
    /// A controller for starting and controlling the robot
    /// </summary>
    public class RobotCleanerService : IRobotCleanerService
    {
        /// <summary>
        /// Create a RobotCleanerService to control the robot
        /// </summary>
        /// <param name="reader">An injected service to get inputs from user</param>
        /// <param name="writer">An injected service to show outputs to user</param>
        public RobotCleanerService(IReadInputService reader, IWriteOutputService writer)
        {
            Reader = reader;
            Writer = writer;
        }

        private const string Separator = " ";
        /// <summary>
        /// An injected service to get inputs from user
        /// </summary>
        public IReadInputService Reader { get; }
        /// <summary>
        /// An injected service to show outputs to user
        /// </summary>
        public IWriteOutputService Writer { get; }
        /// <summary>
        /// Run the main process of cleaning by robot
        /// </summary>

        public virtual void Clean()
        {

            while (!Reader.InputsAreComplete)
            {
                Reader.ParseInput(Reader.ReadInput());
            }

            //Get the inputs needed from console
            var numberOfCommands = Reader.Instructions.NumberOfCommands;            

            var startX = Reader.Instructions.StartPosition.X;
            var startY = Reader.Instructions.StartPosition.Y;

            //Execute the cleaning command for Robot
            var robot = new Robot(startX, startY);

            foreach (KeyValuePair<Direction, int> kvp in Reader.Instructions.CleaningDirections)
            {
                var direction = kvp.Key;
                var step = kvp.Value;

                robot.Move(direction, step);
            }

            //Print number of places Cleaned
            Writer.WriteOutput($"=> Cleaned: {robot.GetCleanedLocations()}");
        }
    }
}
