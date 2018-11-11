using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.ProjectionServiceTests
{
    [TestClass]
    public class AddReservation_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private WatchedMovie deletedReservation;
        private string testUserId = "1234";
        private int testProjectionId = 1;
        private int currentDay = DateTime.Now.Day;
        private int currentMonth = DateTime.Now.Month;
        private int currentYear = DateTime.Now.Year;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            deletedReservation = new WatchedMovie()
            {
                UserId = testUserId,
                ProjectionId = testProjectionId,
                IsDeleted = true,
                Date = new DateTime(currentYear, currentMonth, currentDay)
            };
        }

        [TestMethod]
        public async Task AddNewWatchedMovie_WhenDontExistAndParametersAreValid()
        {
            // Arrange
            //Important: InMemory is designed to be a general purpose database for testing, 
            //and is not designed to mimic a relational database.
            //Simply said InMemory database wont respect the foreign key constraint
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "AddNewWatchedMovie_WhenDontExistAndParametersAreValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new ProjectionService(actContext);
                await command.AddReservation(testUserId, testProjectionId);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(assertContext.WatchedMovies.Count() == 1);
                Assert.IsTrue(assertContext.WatchedMovies.First().UserId == testUserId);
                Assert.IsTrue(assertContext.WatchedMovies.First().ProjectionId == testProjectionId);
            }
        }

        [TestMethod]
        public async Task ChangeIsDeletedToFalse_WhenExistAndParametersAreValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ChangeIsDeletedToFalse_WhenExistAndParametersAreValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddAsync(deletedReservation);
                await actContext.SaveChangesAsync();
                var command = new ProjectionService(actContext);
                await command.AddReservation(testUserId, testProjectionId);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(assertContext.WatchedMovies.Count() == 1);
                Assert.IsTrue(assertContext.WatchedMovies.First().IsDeleted == false);
            }
        }

        //[TestMethod]
        //public async Task ThrowEntityDoesntExistException_WhenUserIdIsNotValid()
        //{
        //    // Arrange
        //    var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
        //        .UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistException_WhenUserIdIsNotValid")
        //        .Options;

        //    //Act and Assert
        //    using (var assertContext = new AlphaCinemaContext(contextOptions))
        //    {
        //        var command = new ProjectionService(assertContext);
        //        await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() => command.AddReservation("", testProjectionId));
        //    }
        //}

        //[TestMethod]
        //public async Task ThrowEntityDoesntExistException_WhenProjectionIdIsNotValid()
        //{
        //    // Arrange
        //    var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
        //        .UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistException_WhenProjectionIdIsNotValid")
        //        .Options;

        //    //Act and Assert
        //    using (var assertContext = new AlphaCinemaContext(contextOptions))
        //    {//We havent added any projections so it should throw an exception
        //        var command = new ProjectionService(assertContext);
        //        await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() => command.AddReservation(testUserId, 5826));
        //    }
        //}
    }
}
