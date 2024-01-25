using System;

namespace WebApi.Utilities.Services;

public class ConsoleLogger : ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine($"[ConsoleLogger] - {message}");
    }
}