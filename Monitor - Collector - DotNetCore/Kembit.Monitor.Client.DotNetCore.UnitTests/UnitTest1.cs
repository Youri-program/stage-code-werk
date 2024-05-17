namespace Kembit.Monitor.Client.DotNetCore.UnitTests
{
    [TestClass]
    public class MonitorClientTests
    {
        static Exception? ex;

        [TestMethod]
        public void Test_SendException_Async()
        {
            // Arrange

            // Act
            var result = MonitorClient.SendExceptionAsync(new Exception("hi"));

            // Assert
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Test_SendException()
        {
            // Arrange

            // Act
            MonitorClient.SendException(new Exception("test"));

            // Assert
            Assert.IsNull(ex);
        }
        [TestMethod]
        public void Test_RegisterExceptionHandler()
        {
            // Arrange 

            // Act
            MonitorClient.RegisterExceptionHandler();

            // Assert 
        }
        [TestMethod]
        public void Test_UnregisterExceptionHandler()
        {
            // Arrange 

            // Act
            MonitorClient.UnregisterExceptionHandler();

            // Assert 
        }
    }
}