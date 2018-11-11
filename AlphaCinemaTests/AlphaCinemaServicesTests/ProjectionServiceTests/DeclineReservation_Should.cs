using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.ProjectionServiceTests
{
    [TestClass]
    public class DeclineReservation_Should
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
                IsDeleted = false,
                Date = new DateTime(currentYear, currentMonth, currentDay)
            };
        }

        [TestMethod]
        public async Task ChangeIsDeletedToTrue_WhenExistAndParametersAreValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ChangeIsDeletedToTrue_WhenExistAndParametersAreValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                //Добавяме старата резервация
                await actContext.AddAsync(deletedReservation);
                await actContext.SaveChangesAsync();
                var command = new ProjectionService(actContext);
                await command.DeclineReservation(testUserId, testProjectionId);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsTrue(assertContext.WatchedMovies.Count() == 1);
                Assert.IsTrue(assertContext.WatchedMovies.First().IsDeleted == true);
            }
        }

        [TestMethod]
        public async Task ThrowEntityDoesntExistException_WhenReservationDoesntExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistException_WhenReservationDoesntExist")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new ProjectionService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(async () => await command.DeclineReservation(testUserId, testProjectionId));
            }
        }
    }
}
