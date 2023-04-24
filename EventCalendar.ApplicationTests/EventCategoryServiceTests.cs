using AutoMapper;
using EventCalendar.Application;
using EventCalendar.Domain;
using EventCalendar.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCalendar.ApplicationTests
{
    [TestClass]
    public class EventCategoryServiceTests
    {
        Mock<IRepository<EventCategory>> _mockEventCategoryRepository = new Mock<IRepository<EventCategory>>();
        Mock<IMapper> _mockMapper = new Mock<IMapper>();


        [TestMethod]
        public async Task GetAllEventCategories_ReturnsEntities()
        {
            // Arrange
            var eventCategories = new List<EventCategory>
            {
                new EventCategory{EventCategoryID = 1, EventCategoryName = "First Category"},
                new EventCategory{EventCategoryID = 2, EventCategoryName = "Second Category"},
                new EventCategory{EventCategoryID = 3, EventCategoryName = "Third Category"}
            };

            _mockEventCategoryRepository.Setup(x => x.GetAllEntities()).ReturnsAsync(eventCategories);

            // Act
            var target = new EventCategoryService(_mockEventCategoryRepository.Object, _mockMapper.Object);
            var actual = await target.GetAllCategories();

            // Assert
            Assert.IsNotNull(actual);

        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetEventCategoryByID_ReturnsNullWhenEventCategoryIDisZeroOrNegative()
        {
            // Arrange
            var eventCategory = new EventCategory { EventCategoryName = "First Category", EventCategoryID = 1 };
            _mockEventCategoryRepository.Setup(x => x.GetEntityByID(It.IsAny<int>())).ReturnsAsync(eventCategory);
            Guard.ThrowIfZeroOrNegative(0, "Test Message", "Test Parameter");

            // Act
            var target = new EventCategoryService(_mockEventCategoryRepository.Object, _mockMapper.Object);
            var actual = target.GetCategoryByID(0);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void GetEventCategoryByID_ReturnsNullWhenEventCategoryIsNull()
        {
            // Arrange
            Guard.ThrowIfNull(null, "Test Message", "Test Parameter");

            // Act
            var target = new EventCategoryService(_mockEventCategoryRepository.Object, _mockMapper.Object);
            var actual = target.GetCategoryByID(1);
        }

        [TestMethod]
        public async Task GetEventCategoryByID_ReturnsEventCategory()
        {
            // Arrange
            var eventCategory = new EventCategory { EventCategoryName = "First Category", EventCategoryID = 1 };
            var dto = new EventCategoryDTO { EventCategoryID = eventCategory.EventCategoryID, EventCategoryName = eventCategory.EventCategoryName };
            _mockEventCategoryRepository.Setup(x => x.GetEntityByID(It.IsAny<int>())).ReturnsAsync(eventCategory);
            _mockMapper.Setup(x => x.Map<EventCategoryDTO>(It.IsAny<EventCategory>())).Returns(dto);

            // Act
            var target = new EventCategoryService(_mockEventCategoryRepository.Object, _mockMapper.Object);
            var actual = await target.GetCategoryByID(1);

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual("First Category", actual.EventCategoryName);
        }
    }
}