namespace EventCalendar.DomainTests
{
    //[TestClass]
    //public class RepositoryTests
    //{
    //    private DbContextOptions<EventCalendarDbContext> _dbContextOptions;
    //    readonly Mock<EventCalendarDbContext> _mockContext = new();
    //    readonly Mock<DbSet<SocialEvent>> _mockDbSet = new();

    //    [TestMethod]
    //    public async Task GetSocialEvents_CanGetAllSocialEvent()
    //    {
    //        // Arrange
    //        var expected = 3;

    //        var socialEvents = new List<SocialEvent>
    //        {
    //            new SocialEvent {SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name"},
    //            new SocialEvent {SocialEventID = 2, SocialEventDescription = "Test Description 2", CategoryID = 1, SocialEventName = "Second Test Event Name"},
    //            new SocialEvent {SocialEventID = 3, SocialEventDescription = "Test Description 3", CategoryID = 2, SocialEventName = "Third Test Event Name"}
    //        };

    //        _mockDbSet.Setup(x => x. ToList()).Returns(socialEvents);
    //        _mockContext.Setup(x => x.Set<SocialEvent>()).Returns(_mockDbSet.Object);


    //        var target = new BaseRepository<SocialEvent>(_mockContext.Object);
    //        var actual = await target.GetAllEntities();

    //        // Assert
    //        Assert.IsNotNull(actual);
    //        Assert.AreEqual(expected, actual.Count());
    //    }

    //    [TestMethod]
    //    public async Task GetSocialEvent_CanGetSocialEvent()
    //    {
    //        var expected = "First Test Event Name";
    //        var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name" };

    //        _mockDbSet.Setup(x => x.FindAsync(It.IsAny<int>())).ReturnsAsync(socialEvent);
    //        _mockContext.Setup(x => x.Set<SocialEvent>()).Returns(_mockDbSet.Object);


    //        var target = new BaseRepository<SocialEvent>(_mockContext.Object);
    //        var actual = await target.GetEntityByID(1);

    //        Assert.IsNotNull(actual);
    //        Assert.AreEqual(expected, actual.SocialEventName);
    //    }

    //    [TestMethod]
    //    public async Task AddSocialEvent_CanAddSocialEvent()
    //    {
    //        var expected = 4;
    //        var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name" };

    //        var target = await CreateRepository();
    //        var actual = target.AddEntity(socialEvent);

    //        Assert.IsNotNull(actual);
    //    }

    //    [TestMethod]
    //    public async Task EditSocialEvent_CanEditSocialEvent()
    //    {
    //        var expected = "First Test Event Name Edited";
    //        var socialEvent = new SocialEvent { SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name Edited" };

    //        var target = await CreateRepository();
    //        await target.EditEntity(socialEvent);

    //    }

    //    #region Private Methods
    //    private async Task<BaseRepository<SocialEvent>> CreateRepository()
    //    {
    //        var context = new EventCalendarDbContext(_dbContextOptions);
    //        await PopulateData(context);
    //        return new BaseRepository<SocialEvent>(context);
    //    }

    //    private async Task PopulateData(EventCalendarDbContext context)
    //    {
    //        var socialEvents = new List<SocialEvent>
    //        {
    //            new SocialEvent {SocialEventID = 1, SocialEventDescription = "Test Description", CategoryID = 1, SocialEventName = "First Test Event Name"},
    //            new SocialEvent {SocialEventID = 2, SocialEventDescription = "Test Description 2", CategoryID = 1, SocialEventName = "Second Test Event Name"},
    //            new SocialEvent {SocialEventID = 3, SocialEventDescription = "Test Description 3", CategoryID = 2, SocialEventName = "Third Test Event Name"}
    //        };

    //        await context.SocialEvents.AddRangeAsync(socialEvents);
    //        await context.SaveChangesAsync();
    //    }

    //    public static class DbContextMock
    //    {
    //        public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
    //        {
    //            var queryable = sourceList.AsQueryable();
    //            var dbSet = new Mock<DbSet<T>>();
    //            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
    //            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
    //            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
    //            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
    //            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
    //            return dbSet.Object;
    //        }
    //    }
    //    #endregion
    //}
}