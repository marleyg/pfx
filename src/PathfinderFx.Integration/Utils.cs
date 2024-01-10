using Microsoft.Extensions.Logging;

namespace PathfinderFx.Integration;

public static class Utils
{
    public static class AppLogger
    {
        public static ILoggerFactory MyLoggerFactory { get; set; } = LoggerFactory.Create(builder =>
        {
            builder.AddConsole(configure: config => { builder.SetMinimumLevel(LogLevel.Information); });
        });
    
        public static ILogger CreateLogger<T>() => MyLoggerFactory.CreateLogger<T>();
        public static ILogger CreateLogger(string categoryName) => MyLoggerFactory.CreateLogger(categoryName);
    }
}