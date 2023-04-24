using EventCalendar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar.DomainTests
{
    [TestClass]
    public class RepositoryTests
    {
        private DbContextOptions<EventCalendarDbContext> _dbContextOptions;

        [TestInitialize]
        public void TestInitialize()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EventCalendarDbContext>().UseInMemoryDatabase("Name").Options;
        }

        [TestMethod]
        public async Task GetSocialEvents_CanGetAllSocialEvent()
        {
            // Arrange
            var expected = 3;

            // Act
            var target = await CreateRepository();
            var actual = await target.GetAllEntities();

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Count());
        }

        [TestMethod]
        public async Task GetSocialEvent_CanGetSocialEvent()
        {
            var expected = "First Test Event Name";

            var target = await CreateRepository();
            var actual = await target.GetEntityByID(1);

            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.SocialEventName);
        }

        [TestMethod]
        public async Task AddSocialEvent_CanAddSocialEvent()
        {
            var expected = 4;
            var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name" };

            var target = await CreateRepository();
            var actual = target.AddEntity(socialEvent);

            Assert.IsNotNull(actual);
        }

        #region Private Methods
        private async Task<BaseRepository<SocialEvent>> CreateRepository()
        {
            var context = new EventCalendarDbContext(_dbContextOptions);
            await PopulateData(context);
            return new BaseRepository<SocialEvent>(context);
        }

        private async Task PopulateData(EventCalendarDbContext context)
        {
            var socialEvents = new List<SocialEvent>
            {
                new SocialEvent {SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name"},
                new SocialEvent {SocialEventID = 2, SocialEventDescription = "Test Description 2", CategoryID = 1, SocialEventName = "Second Test Event Name"},
                new SocialEvent {SocialEventID = 3, SocialEventDescription = "Test Description 3", CategoryID = 2, SocialEventName = "Third Test Event Name"}
            };

            await context.SocialEvents.AddRangeAsync(socialEvents);
            await context.SaveChangesAsync();
        }
        #endregion
    }
}