using AutoMapper;
using EventCalendar.Application;
using EventCalendar.Domain;
using EventCalendar.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventCalendar.ApplicationTests
{
    [TestClass]
    public class SocialEventServiceTests
    {
        readonly Mock<IRepository<SocialEvent>> _mockSocialEventRepository = new();
        readonly Mock<IRepository<Log>> _mockLogRepository = new();
        readonly Mock<IRepository<EventCategory>> _mockSocialCategoryRepository = new();
        readonly Mock<IMapper> _mockMapper = new();
        readonly Mock<ILogService> _mockLogService = new(MockBehavior.Loose);

        #region GetAndSearchSocialEventsTests
        [TestMethod]
        public async Task GetAllSocialEvents_ReturnsEntities()
        {
            // Arrange
            var socialEvents = new List<SocialEvent>
            {
                new SocialEvent {SocialEventID = 1, SocialEventName = "First Event"},
                new SocialEvent {SocialEventID = 2, SocialEventName = "Second Event"},
                new SocialEvent {SocialEventID = 3, SocialEventName = "Third Event"}
            };

            var dtos = new List<SocialEventDTO>
            {
                new SocialEventDTO {SocialEventID = 1, SocialEventName = "First Event"},
                new SocialEventDTO {SocialEventID = 2, SocialEventName = "Second Event"},
                new SocialEventDTO {SocialEventID = 3, SocialEventName = "Third Event"}
            };

            _mockSocialEventRepository.Setup(x => x.GetAllEntities()).ReturnsAsync(socialEvents);
            _mockMapper.Setup(x => x.Map<List<SocialEventDTO>>(It.IsAny<List<SocialEvent>>())).Returns(dtos);

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = await target.GetAllSocialEvents();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Count);
        }

        [TestMethod]
        public void SearchSocialEvents_ReturnsSpecificEntity()
        {
            // Arrange
            var socialEvents = new List<SocialEvent>
            {
                new SocialEvent {SocialEventID = 1, SocialEventName = "First Event"},
            };

            var dtos = new List<SocialEventDTO>
            {
                new SocialEventDTO {SocialEventID = 1, SocialEventName = "First Event"},
            };

            _mockSocialEventRepository.Setup(x => x.QueryEntitiesByParameter(It.IsAny<Expression<Func<SocialEvent, bool>>>())).ReturnsAsync(socialEvents);
            _mockMapper.Setup(x => x.Map<List<SocialEventDTO>>(It.IsAny<List<SocialEvent>>())).Returns(dtos);

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.SearchSocialEventsByName("First Event");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Result.Count);
        }
        #endregion

        #region GetSocialEventByIDTests
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetSocialEventByID_ThrowsExceptionWhenIDIsZeroOrNegative()
        {
            // Arrange
            var logService = new LogService(_mockLogRepository.Object);
            Guard.ThrowIfZeroOrNegative(0, "Test Message", "Test Parameter");
            _mockLogRepository.Setup(x => x.GetAllEntities()).ReturnsAsync(new List<Log>());

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.GetSocialEventByID(0);

            // Assert
            Assert.AreEqual(null, actual.Result.SocialEventName);
            _mockLogService.Verify(x => x.CreateExceptionLog(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void GetSocialEventByID_ReturnsSocialEvent()
        {
            // Arrange
            var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventName = "First Event" };
            var dto = new SocialEventDTO { SocialEventID = 1, SocialEventName = "First Event" };

            _mockSocialEventRepository.Setup(x => x.GetEntityByID(It.IsAny<int>())).ReturnsAsync(socialEvent);
            _mockMapper.Setup(x => x.Map<SocialEventDTO>(It.IsAny<SocialEvent>())).Returns(dto);
            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.GetSocialEventByID(1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("First Event", actual.Result.SocialEventName);
        }
        #endregion

        #region AddSocialEventTests
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void AddSocialEvent_ThrowsExceptionWhenDTOIsNull()
        {
            // Arrange
            SocialEventDTO dto = null;
            var logService = new LogService(_mockLogRepository.Object);
            Guard.ThrowIfNull(dto, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.AddSocialEvent(dto);

            // Assert
            _mockSocialEventRepository.Verify(x => x.AddEntity(It.IsAny<SocialEvent>()), Times.Once());
        }

        [TestMethod]
        public void AddSocialEvent_AddsSocialEvent()
        {
            // Arrange
            var dto = new SocialEventDTO { SocialEventID = 1, SocialEventDescription = "Test Time", SocialEventName = "Test" };
            Guard.ThrowIfNull(dto, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.AddSocialEvent(dto);

            // Arrange
            _mockLogService.Verify(x => x.CreateAddLog(It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region EditSocialEventTests
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void EditSocialEvent_ThrowsExceptionWhenDTOIsNull()
        {
            // Arrange
            SocialEventDTO dto = null;
            var logService = new LogService(_mockLogRepository.Object);
            Guard.ThrowIfNull(dto, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.EditSocialEvent(dto);
        }

        [TestMethod]
        public void EditSocialEvent_EditsSocialEvent()
        {
            // Arrange
            var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventName = "Event Name" };
            var dto = new SocialEventDTO { SocialEventID = 1, SocialEventDescription = "Test Time", SocialEventName = "Test" };

            _mockSocialEventRepository.Setup(x => x.EditEntity(It.IsAny<SocialEvent>()));
            _mockSocialEventRepository.Setup(x => x.GetEntityByID(It.IsAny<int>())).ReturnsAsync(socialEvent);

            var logService = new LogService(_mockLogRepository.Object);
            Guard.ThrowIfNull(dto, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.EditSocialEvent(dto);

            // Arrange
            _mockLogService.Verify(x => x.CreateEditLog(It.IsAny<string>()), Times.Once);
        }
        #endregion

        #region DeleteSocialEventTests
        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DeleteSocialEvent_ThrowsExceptionWhenDTOIsNull()
        {
            // Arrange
            Guard.ThrowIfZeroOrNegative(0, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.DeleteSocialEvent(0);
        }

        [TestMethod]
        public void DeleteSocialEvent_DeletesSocialEvent()
        {
            // Arrange
            var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventName = "Event Name" };
            var dto = new SocialEventDTO { SocialEventID = 1, SocialEventDescription = "Test Time", SocialEventName = "Test" };

            _mockSocialEventRepository.Setup(x => x.EditEntity(It.IsAny<SocialEvent>()));
            _mockSocialEventRepository.Setup(x => x.GetEntityByID(It.IsAny<int>())).ReturnsAsync(socialEvent);

            Guard.ThrowIfNull(dto, "Test Message", "Test Parameter");

            // Act
            var target = new SocialEventService(_mockSocialEventRepository.Object, _mockSocialCategoryRepository.Object,
                _mockLogService.Object, _mockMapper.Object);
            var actual = target.DeleteSocialEvent(dto.SocialEventID);

            // Arrange
            _mockLogService.Verify(x => x.CreateDeleteLog(It.IsAny<string>()), Times.Once);
        }
        #endregion
    }
}
