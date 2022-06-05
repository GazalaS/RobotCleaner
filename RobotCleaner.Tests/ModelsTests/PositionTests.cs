using NUnit.Framework;
using RobotCleaner.Models;

namespace RobotCleaner.Tests.ModelsTests
{
    public class PositionTests
    {
        [TestCase(60, 70)]
        [TestCase(800, -9000)]
        [TestCase(9000, 10000)]
        public void WhenPositionCreatedCoordinatesShouldBeSetProperly(int x, int y)
        {
            //arrange

            //act
            var position = new Position(x, y);

            //assert
            Assert.That(position.X, Is.EqualTo(x));
            Assert.That(position.Y, Is.EqualTo(y));
        }

        [Theory]
        [TestCase(Direction.North, 0, 1)]
        public void WhenGetNeighborLocationWithValidInputThenGetValidResult(Direction direction, int expectedX, int expectedY)
        {
            //arrange
            var position = new Position(0, 0);

            //act
            var neighborPoint = position.GetNeighborLocation(direction);

            //check
            Assert.That(neighborPoint.X, Is.EqualTo(expectedX));
            Assert.That(neighborPoint.Y, Is.EqualTo(expectedY));
        }

        [Theory]
        [TestCase(Direction.Unknown, 0, 1)]
        public void WhenGetNeighborLocationWithInValidInputThenGetValidResult(Direction direction, int expectedX, int expectedY)
        {
            //arrange
            var position = new Position(0, 1);

            //act
            var neighborPoint = position.GetNeighborLocation(direction);

            //check
            Assert.That(neighborPoint.X, Is.EqualTo(expectedX));
            Assert.That(neighborPoint.Y, Is.EqualTo(expectedY));
        }
    }
}
