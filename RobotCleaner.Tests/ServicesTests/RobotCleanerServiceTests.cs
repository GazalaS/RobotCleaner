using Moq;
using RobotCleaner.Services;

namespace RobotCleaner.Tests.ServicesTests
{
    public class RobotCleanerServiceTests
    {
        [Test]
        public void WhenRobotGivenValidInputThenCleanIsCalled()
        {
            //arrange
            var inputInstructions = new[] {
                "2",
                "10 22",
                "E 2",
                "N 1"
            };

            var mockReadInputService = new Mock<IReadInputService>();

            //act
            mockReadInputService.Setup(r => r.ParseInput(inputInstructions[0]));
            mockReadInputService.Setup(r => r.ParseInput(inputInstructions[1]));
            mockReadInputService.Setup(r => r.ParseInput(inputInstructions[2]));
            mockReadInputService.Setup(r => r.ParseInput(inputInstructions[3]));

            var mockWriteOutputService = new Mock<IWriteOutputService>();

            var mockRobotCleanerService = new Mock<RobotCleanerService>(mockReadInputService.Object, mockWriteOutputService.Object);
            mockRobotCleanerService.Object.Clean();

            //assert
            mockRobotCleanerService.Verify(x => x.Clean(), Times.Once());
        }
    }
}
