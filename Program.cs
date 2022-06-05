// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RobotCleaner.Extensions;
using RobotCleaner.Services;

var serviceProvider = ServicesExtensions.BuildServiceProvider();

var robotCleanerService = serviceProvider.GetService<IRobotCleanerService>();
robotCleanerService!.Clean();