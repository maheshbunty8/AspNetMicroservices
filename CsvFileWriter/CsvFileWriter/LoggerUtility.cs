using Microsoft.Extensions.Logging;
using System;

public class LoggerUtility<T> where T : class
{
    private readonly ILogger<T> _logger;

    public LoggerUtility(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message)
    {
        _logger.LogInformation(message);
    }

    public void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public void LogError(Exception ex, string message)
    {
        _logger.LogError(ex, message);
    }
}