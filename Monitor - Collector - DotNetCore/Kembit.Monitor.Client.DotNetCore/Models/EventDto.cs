using Microsoft.Extensions.Logging;

namespace Kembit.Monitor.Client.DotNetCore.Models;

/// <summary>
/// Exceptions model based on the model in the Collector API.
/// </summary>
public class EventDto
{
    /// <summary>
    /// Represents date and time when exception occurred.
    /// </summary>
    public DateTime? ThrownAt { get; set; }

    /// <summary>
    /// Represents severity level of the occurred exception.
    /// </summary>
    public LogLevel LogLevel { get; set; }

    /// <summary>
    /// Represents the error message that describes the reason for the current exception in text form.
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// Represents a generic collection of used parameters.
    /// </summary>
    public IDictionary<string, string>? Parameters { get; set; }

    /// <summary>
    /// Represents type of exception in text form.
    /// </summary>
    public string? ExceptionType { get; set; }

    /// <summary>
    /// Represents the name of the application or the object that causes the exception.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// Represents relevant programming language in which the exception occurred.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Represents the Url of the occurred exception. ???
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Represents ???
    /// </summary>
    public string[]? Claims { get; set; }
}