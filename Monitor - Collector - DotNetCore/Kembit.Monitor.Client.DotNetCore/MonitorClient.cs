using Kembit.Monitor.Client.DotNetCore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace Kembit.Monitor.Client.DotNetCore
{
    /// <summary>
    /// Communicates with the Collector API & logs exceptions and user tracking information.
    /// </summary>
    public class MonitorClient // !Temporary deleted static due Unit Testing!
    {
        private const string DEFAULT_COLLECTOR_URL = "http://collector.kembbit.nl";

        /// <summary>
        /// Sends exception info to the collector API based on the static configuration. <br></br>
        /// Sync version.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        /// <param name="exceptionParameters"></param>
        /// <param name="logger"></param>
        public static void SendException(Exception exception, LogLevel logLevel = LogLevel.Error, IDictionary<string, string>? exceptionParameters = null, ILogger? logger = null)
        {
            Task.Run(async () => { await SendExceptionAsync(exception, logLevel, exceptionParameters, logger); }).Wait();
        }

        /// <summary>
        /// Sends exception info to the collector API based on the static configuration. <br></br>
        /// Async version.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="logLevel"></param>
        /// <param name="exceptionParameters"></param>
        /// <param name="logger"></param>
        public static async Task SendExceptionAsync(Exception exception, LogLevel logLevel = LogLevel.Error, IDictionary<string, string>? exceptionParameters = null, ILogger? logger = null)
        {
            logger?.LogDebug("Sending exception: {Type}, {Message}", exception.GetType().FullName, exception.Message);

            var eventObj = new EventDto()
            {
                ThrownAt = DateTime.Now,
                LogLevel = logLevel,
                Message = exception.Message,
                Parameters = exceptionParameters,
                ExceptionType = exception.GetType().FullName,
                Source = exception.Source,
                Language = "C#/DotNetCore",
                Url = null,
                Claims = {}
            };

            string json;

            using (var memoryStream = new MemoryStream())
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(memoryStream, eventObj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                using var reader = new StreamReader(memoryStream);
                json = await reader.ReadToEndAsync();
            }

            await HttpClientAsync(json, logger);
        }

        /// <summary>
        /// Registers unhandled exceptions.
        /// </summary>
        public static void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
        }

        /// <summary>
        /// Unregisters unhandled exceptions.
        /// </summary>
        public static void UnregisterExceptionHandler()
        {
            AppDomain.CurrentDomain.UnhandledException -= UnhandledExceptionHandler;
        }

        /// <summary>
        /// Method for sending HTTP requests from a resource identified by a Url to invoke a Web API, using HttpClient.
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static async Task HttpClientAsync(string eventObject, ILogger? logger)
        {
            var client = new HttpClient();

            var content = new StringContent(eventObject, Encoding.UTF8, MediaTypeNames.Application.Json);

            string? collectorUrl = GetCollectorUrlFromConfiguration();
            
            if (string.IsNullOrEmpty(collectorUrl)) collectorUrl = DEFAULT_COLLECTOR_URL;

            logger?.LogDebug("Sending information to url: {Url}", collectorUrl);
            var response = await client.PostAsJsonAsync(collectorUrl, content);

            await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Get's the Collector Url from configuration model; 'MonitorClientConfiguration'.
        /// </summary>
        /// <returns name="monitorSettings"></returns>
        private static string? GetCollectorUrlFromConfiguration()
        {
            string? environmentName = Environment.GetEnvironmentVariable("DOTNET_CORE_ENVIRONMENT");

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var configuration = configurationBuilder
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            MonitorClientConfiguration monitorSettings = new MonitorClientConfiguration();
            configuration.GetSection("Kembit:Monitor").Bind(monitorSettings);

            return monitorSettings.ApiUrl;
        }

        /// <summary>
        /// Catches UnhandledExceptions and sends to SendException method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
                SendException(exception, LogLevel.Error);
        }
    }
}