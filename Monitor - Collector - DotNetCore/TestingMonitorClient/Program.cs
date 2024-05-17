using Kembit.Monitor.Client.DotNetCore;

class TestMonitorClient {

    /// <summary>
    /// This is a simple Console App where we will test the MonitorClient Class library on mainly 'Unhandled Exceptions'. <br></br>
    /// API calls and/or Http status codes are not included in .NET Core framework. 
    /// </summary>
    static void Main(string[] args) {
        // Call Monitor.RegisterExceptionHandler to catch, register and send the exception to the Collector API.
        MonitorClient.RegisterExceptionHandler();

        // Generates an index out of range exception. 
        string fileContents = File.ReadAllText(args[0]);

        // Pauses the console until user presses enter.
        Console.ReadLine();
    }
}


