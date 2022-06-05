using RobotCleaner.Models;

namespace RobotCleaner.Services
{
    /// <summary>
    /// An interface to get inputs from user
    /// </summary>
    public interface IReadInputService
    {
        /// <summary>
        /// Get inputs from user
        /// </summary>
        /// <returns>Entered inputs by user</returns>    
        ///         
        List<string> InputInstructions { get; set; }
        Instructions Instructions { get; set; }
        bool InputsAreComplete { get; }
        string ReadInput();
        void ParseInput(string inputInstructions);
    }

    public class ReadInputService : IReadInputService
    {
        // read-write instance property
        public List<string> InputInstructions { get; set; }
        // read-write instance property
        public Instructions Instructions { get; set; }

        public bool InputsAreComplete
        {
            get { return (InputInstructions.Count == (Instructions.NumberOfCommands + 2)); }
        }

        public ReadInputService()
        {
            InputInstructions = new List<string>();
            Instructions = new Instructions();
        }

        public string ReadInput()
        {
            return Console.ReadLine()!;
        }
        public void ParseInput(string inputInstruction)
        {
            if (!InputsAreComplete)
            {
                if (InputInstructions.Count == 0)
                {
                    SetNumberOfCommands(inputInstruction);
                }
                else if (InputInstructions.Count == 1)
                {
                    SetStartPosition(inputInstruction);
                }
                else
                {
                    SetCleaningDirections(inputInstruction);
                }
            }
            InputInstructions.Add(inputInstruction);
        }

        private void SetNumberOfCommands(string inputInstructions)
        {
            int numberOfCommands = int.Parse(inputInstructions);

            Instructions.NumberOfCommands = (numberOfCommands < Constants.MinNumberOfCommands) ? Constants.MinNumberOfCommands : (numberOfCommands > Constants.MaxNumberOfCommands) ? Constants.MaxNumberOfCommands : numberOfCommands;
        }
        private void SetStartPosition(string inputInstruction)
        {
            string[] coordinates = inputInstruction.Split(null); //white space character
            if (coordinates.Length > 1)
            {                
                int x = int.Parse(coordinates[0]);
                x = (x >= Constants.MaxForPositionX) ? Constants.MaxForPositionX : (x <= Constants.MinForPositionX) ? Constants.MinForPositionX : x;

                int y = int.Parse(coordinates[1]);
                y = (y >= Constants.MaxForPositionY) ? Constants.MaxForPositionY : (y <= Constants.MinForPositionY) ? Constants.MinForPositionY : y;
                
                Instructions.StartPosition = new Position(x, y);
            }
        }
        private void SetCleaningDirections(string inputInstruction)
        {
            string[] cleaningDirection = inputInstruction.Split(null);
            var direction = new Direction();
            int steps = 0;

            if (cleaningDirection.Length > 1)
            {
                switch (cleaningDirection[0].ToUpper())
                {
                    case "N":
                        direction = Direction.North;
                        break;
                    case "S":
                        direction = Direction.South;
                        break;
                    case "E":
                        direction = Direction.East;
                        break;
                    case "W":
                        direction = Direction.West;
                        break;
                    default:
                        direction = Direction.Unknown;
                        break;
                }

                steps = int.Parse(cleaningDirection[1]);
                steps = (steps >= Constants.MaxNumberOfSteps) ? (Constants.MaxNumberOfSteps-1) : (steps <= Constants.MinNumberOfSteps) ? (Constants.MinNumberOfSteps+1) : steps;
            }
            Instructions.CleaningDirections.Add(direction, steps); 
        }
    }
}