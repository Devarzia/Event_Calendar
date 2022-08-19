using EventCalendar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace EventCalendar.DomainTests
{
    [TestClass]
    public class RepositoryTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            var options = CreateDbContextOptions();
            CreateDatabaseEntities(options);
        }

        [TestMethod]
        public void GetAllEntities_ReturnsAllEntities()
        {
            // Arrange
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act
            var target = new BaseRepository<SocialEvent>(context);
            var actual = target.GetAllEntities();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Result.Count());
        }

        [TestMethod]
        public void SearchEntities_ReturnsEntities()
        {
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act 
            var target = new BaseRepository<SocialEvent>(context);
            var actual = target.QueryEntitiesByParameter(x => x.CategoryID == 2);

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Result.Count());
        }

        [TestMethod]
        public void GetEntityByID_ReturnsEntity()
        {
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act 
            var target = new BaseRepository<SocialEvent>(context);
            var actual = target.GetEntityByID(2);

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreEqual("Second Test Event Name", actual.Result.SocialEventName);
        }

        [TestMethod]
        public void AddEntity_IsSuccessful()
        {
            var socialEvent = new SocialEvent { CategoryID = 1, SocialEventName = "Fourth Test Event Name", SocialEventID = 4 };
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act 
            var target = new BaseRepository<SocialEvent>(context);
            var actual = target.AddEntity(socialEvent);

            // Arrange
            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void EditEntity_IsSuccessful()
        {
            var dto = new SocialEvent { SocialEventID = 1, SocialEventName = "Edited Name" };
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act 
            var target = new BaseRepository<SocialEvent>(context);
            var actual = target.EditEntity(dto);
            var newSocialEvent = target.GetEntityByID(1);

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreEqual(dto.SocialEventName, newSocialEvent.Result.SocialEventName);
        }

        [TestMethod]
        public void DeleteEntity_IsSuccessful()
        {
            var options = CreateDbContextOptions();
            using var context = new EventCalendarDbContext(options);
            // Act 
            var target = new BaseRepository<SocialEvent>(context);
            var deletedEvent = target.GetEntityByID(4).Result;
            var actual = target.DeleteEntity(deletedEvent);
            var numberOfEvents = target.GetAllEntities().Result.Count();

            // Arrange
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, numberOfEvents);
        }

        #region Private Methods
        private DbContextOptions<EventCalendarDbContext> CreateDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<EventCalendarDbContext>().UseInMemoryDatabase("TestDatabase").Options;

            CreateDatabaseEntities(options);

            return options;
        }

        private static void CreateDatabaseEntities(DbContextOptions<EventCalendarDbContext> options)
        {
            var socialEvents = new List<SocialEvent>
            {
                new SocialEvent {SocialEventID = 1, CategoryID = 1, SocialEventName = "First Test Event Name"},
                new SocialEvent {SocialEventID = 2, CategoryID = 1, SocialEventName = "Second Test Event Name"},
                new SocialEvent {SocialEventID = 3, CategoryID = 2, SocialEventName = "Third Test Event Name"}
            };

            using var context = new EventCalendarDbContext(options);
            context.SocialEvents.AddRange(socialEvents);
            context.SaveChangesAsync();
        }
        #endregion
    }
}
