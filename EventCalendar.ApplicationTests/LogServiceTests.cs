using EventCalendar.Application;
using EventCalendar.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace EventCalendar.ApplicationTests
{
    [TestClass]
    public class LogServiceTests
    {
        Mock<IRepository<Log>> _mockLogRepository = new Mock<IRepository<Log>>();

        #region CreateAddLogTests
        [TestMethod]
        public async Task CreateAddLog_CreatesLog()
        {
            // Arrange

            // Act
            var target = new LogService(_mockLogRepository.Object);
            await target.CreateAddLog("Test Message");

            // Assert
            _mockLogRepository.Verify(x => x.AddEntity(It.IsAny<Log>()), Times.Once);


        }
        #endregion

        #region CreateEditLogTests
        [TestMethod]
        public async Task CreateEditLog_CreatesLog()
        {
            // Arrange

            // Act
            var target = new LogService(_mockLogRepository.Object);
            await target.CreateEditLog("Test Message");

            // Assert
            _mockLogRepository.Verify(x => x.AddEntity(It.IsAny<Log>()), Times.Once);
        }
        #endregion

        #region CreateDeleteLogTests
        [TestMethod]
        public async Task CreateDeleteLog_CreatesLog()
        {
            // Arrange

            // Act
            var target = new LogService(_mockLogRepository.Object);
            await target.CreateDeleteLog("Test Message");

            // Assert
            _mockLogRepository.Verify(x => x.AddEntity(It.IsAny<Log>()), Times.Once);
        }
        #endregion

        #region CreateExceptionLogTests
        [TestMethod]
        public async Task CreateExceptionLog_CreatesLog()
        {
            // Arrange

            // Act
            var target = new LogService(_mockLogRepository.Object);
            await target.CreateExceptionLog("Test Message");

            // Assert
            _mockLogRepository.Verify(x => x.AddEntity(It.IsAny<Log>()), Times.Once);
        }
        #endregion
    }
}
