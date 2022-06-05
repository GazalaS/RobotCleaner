using Microsoft.Extensions.DependencyInjection;
using RobotCleaner.Services;

namespace RobotCleaner.Extensions
{
    public static class ServicesExtensions
    {
        public static ServiceProvider BuildServiceProvider()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddScoped<IReadInputService, ReadInputService>()
                .AddScoped<IWriteOutputService, WriteOutputService>()
                .AddScoped<IRobotCleanerService, RobotCleanerService>()
                .BuildServiceProvider();

            return serviceProvider;
        }
    }
}
