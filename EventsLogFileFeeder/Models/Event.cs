using System.Text.Json.Serialization;

namespace EventsLogFileFeeder.Models
{
    public class EventModel
    {
        [JsonPropertyName("@t")]
        public string? Timestamp { get; set; }

        [JsonPropertyName("@mt")]
        public string? MessageTemplate { get; set; }

        [JsonPropertyName("@l")]
        public string? LogLevel { get; set; }

        public string? HealthCheckName { get; set; }
        public string? HealthStatus { get; set; }
        public double? ElapsedMilliseconds { get; set; }
        public string? HealthCheckDescription { get; set; }
        public EventIdModel? EventId { get; set; }
        public string? SourceContext { get; set; }
        public string? RequestId { get; set; }
        public string? RequestPath { get; set; }
        public string? Application { get; set; }
        public string? connectionString { get; set; }
        public string? Message { get; set; }
        public int? Attempts { get; set; }
        public string? envName { get; set; }
        public string? contentRoot { get; set; }

    }

    public class EventIdModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }

    public enum LogLevel
    {
        Trace,
        Debug,
        Information,
        Warning,
        Error,
        Critical,
        None
    }
}
