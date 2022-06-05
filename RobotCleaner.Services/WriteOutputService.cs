

namespace RobotCleaner.Services
{
    public interface IWriteOutputService
    {
        void WriteOutput(string outputResult);
    }
    public class WriteOutputService : IWriteOutputService
    {
       public void WriteOutput(string outputResult) => Console.WriteLine(outputResult);
    }
}
