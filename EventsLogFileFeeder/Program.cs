using EventsLogFileFeeder.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Data.SqlClient;
using Dapper;

namespace EventsLogFileFeeder
{
    class Program
    {
        // Database Connection String
        static string connectionString = "Server=localhost;database=master;uid=sa;pwd=P@ssW0rd123;TrustServerCertificate=true;";

        // Logfile-Reader Method
        static void LogFileReader(string filePath1, string filePath2)
        {
            string[] filePaths = { filePath1, filePath2 };

            foreach (string filePath in filePaths)
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Deserialize the JSON message
                        EventModel? ev = JsonSerializer.Deserialize<EventModel>(line);

                        // Use and print out the mapped values
                        EventDto eventDto = MapSourceToDestination(ev);
                        Console.WriteLine("Mapped Message: " + eventDto.Message);
                        Console.WriteLine("Mapped Source: " + eventDto.Source);
                        Console.WriteLine("Mapped ThrownAt: " + eventDto.ThrownAt);
                        Console.WriteLine("Mapped LogLevelId: " + eventDto.LogLevelId);
                        Console.WriteLine("-----------------------------------");

                        // Insert eventDto into the Event table
                        InsertData(connectionString, eventDto);
                    }
                }
            }
            Console.ReadLine();
        }

        // Mapping Method
        public static EventDto MapSourceToDestination(EventModel ev)
        {
            var eventDto = new EventDto
            {
                Message = ev.MessageTemplate,
                Source = ev.SourceContext,
                ThrownAt = DateTime.Parse(ev.Timestamp)
            };

            // Map LogLevel to LogLevelId as an Enum
            if (Enum.TryParse(ev.LogLevel, true, out LogLevel logLevel))
            {
                eventDto.LogLevelId = (int)logLevel;
            }

            return eventDto;
        }

        // Inserting mapped data from logfiles into the Event table using Dapper.
        public static void InsertData(string connectionString, EventDto eventDto)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                string insertQuery = "INSERT INTO [eventsdb].[Event] (Message, Source, ThrownAt, LogLevelId) VALUES (@Message, @Source, @ThrownAt, @LogLevelId)";
                connection.Execute(insertQuery, eventDto);
            }
        }

        // Main Method
        static void Main(string[] args)
        {
            LogFileReader("C:\\Users\\yvdmeulen\\AlertDefinitionTasks\\EventsLogFileFeeder\\EventsLogFileFeeder\\CleansingApp20230504.json", "C:\\Users\\yvdmeulen\\AlertDefinitionTasks\\EventsLogFileFeeder\\EventsLogFileFeeder\\CleansingApp20230511_001.json");
        }
    }
}