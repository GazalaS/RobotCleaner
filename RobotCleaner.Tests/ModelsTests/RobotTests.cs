using RobotCleaner.Models;
using RobotCleaner.Services;

namespace RobotCleaner.Tests.ModelsTests
{
    public class RobotTests
    {
        [Test]
        public void WhenRobotOjectCreatedThenCreateRobotInStartPositionWithOnePosition()
        {
            //arrange
            const int startingX = 10;
            const int startingY = 22;

            //act
            var robot = new Robot(startingX, startingY);

            //assert
            Assert.IsNotNull(robot);
            Assert.That(robot.CurrentLocation.X, Is.EqualTo(startingX));
            Assert.That(robot.CurrentLocation.Y, Is.EqualTo(startingY));
        }

        [Test]
        public void WhenCreateRobotThenRobotCreated()
        {
            //arrange
            var readInputService = new ReadInputService();
            readInputService.ParseInput("0");
            readInputService.ParseInput("0 0");
            var position = readInputService.Instructions.StartPosition;

            //act
            Robot robot = new Robot(position.X, position.Y);

            //assert
            Assert.IsNotNull(robot);
        }

        [Test]
        public void WhenRobotMoveStartPositionIsZeroThenRobotPositionShouldRemainInSameChange()
        {
            //arrange
            var readInputService = new ReadInputService();
            readInputService.ParseInput("0");
            readInputService.ParseInput("0 0");
            var startPosition = readInputService.Instructions.StartPosition;
            Robot robot = new Robot(startPosition.X, startPosition.Y);

            //act
            robot.Move(Direction.Unknown, 0);

            //assert
            Assert.That(robot.CurrentLocation.X, Is.EqualTo(startPosition.X));
            Assert.That(robot.CurrentLocation.Y, Is.EqualTo(startPosition.Y));
        }

        [Test]
        public void WhenRobotCreateObjectThenOneLocationShouldBeCleaned()
        {
            //arrange
            const int startingX = 0;
            const int startingY = 0;

            //act
            var robot = new Robot(startingX, startingY);

            //check
            Assert.That(robot.GetCleanedLocations(), Is.EqualTo(1));
        }

        [Theory]
        [TestCase(Direction.East, 1)]
        [TestCase(Direction.West, 1)]
        [TestCase(Direction.North, 1)]
        [TestCase(Direction.South, 1)]
        public void WhenRobotMoveWithOneCleaningDirectionThenTwoPointsMustBeCleaned(Direction direction, int step)
        {
            //arrange
            const int startingX = 10;
            const int startingY = 22;

            var robot = new Robot(startingX, startingY);

            //act
            robot.Move(direction, step);

            //check
            Assert.That(robot.GetCleanedLocations(), Is.EqualTo(2));
        }

        [Test]
        public void WhenRobotMoveMultipleCleaningDirectionInUniqueLocationsThenResultShouldBeEqualToTotalStepsPlus1()
        {
            //arrange
            var robot = new Robot(5, 6);

            var commands = new List<KeyValuePair<Direction, int>>
            {
                KeyValuePair.Create(Direction.East, 2),
                KeyValuePair.Create(Direction.North, 2),
                KeyValuePair.Create(Direction.West, 3),
                KeyValuePair.Create(Direction.South, 1)
            };

            //act
            foreach (var command in commands)
            {
                robot.Move(command.Key, command.Value);
            }

            //check
            var totalSteps = commands.Sum(x => x.Value);
            var expectedResult = totalSteps + 1;
            Assert.That(robot.GetCleanedLocations(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void WhenRobotMoveMultipleCommandsInForwardAndBackwardMethodThenResultShouldBeUniqueLocations()
        {
            //arrange
            var robot = new Robot(3, 2);

            var commands = new List<KeyValuePair<Direction, int>>
            {
                KeyValuePair.Create(Direction.East, 3),
                KeyValuePair.Create(Direction.West, 3),
                KeyValuePair.Create(Direction.North, 3),
                KeyValuePair.Create(Direction.South, 3)
            };

            //act
            foreach (var (key, value) in commands)
            {
                robot.Move(key, value);
            }

            //check
            Assert.That(robot.GetCleanedLocations(), Is.EqualTo(7));
        }

        [Test]
        public void WhenRobotMoveMultipleCommandsThenResultShouldBeUniqueLocations()
        {
            //arrange
            var commands = new List<KeyValuePair<Direction, int>>
            {
                KeyValuePair.Create(Direction.East, 6),
                KeyValuePair.Create(Direction.North, 3),
                KeyValuePair.Create(Direction.West, 3),
                KeyValuePair.Create(Direction.South, 6),
                KeyValuePair.Create(Direction.West, 3),
                KeyValuePair.Create(Direction.North, 3)
            };

            var robot = new Robot(1, 2);

            //act
            foreach (var (key, value) in commands)
            {
                robot.Move(key, value);
            }

            //check
            Assert.That(robot.GetCleanedLocations(), Is.EqualTo(23));
        }
    }
}
