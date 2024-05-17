namespace EventsLogFileFeeder.Models
{
    public class EventDto
    {
        public int EventId { get; set; }
        public int? AlertDefinitionId { get; set; }
        public int? LogLevelId { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public string? Platform { get; set; }
        public string? ExceptionType { get; set; }
        public string? Url { get; set; }
        public DateTime ThrownAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
