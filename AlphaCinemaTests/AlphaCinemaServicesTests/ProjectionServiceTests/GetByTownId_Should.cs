using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
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
    public class GetByTownId_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private WatchedMovie deletedReservation;
        private WatchedMovie validReservation;
        private Projection projection;
        private OpenHour openHour;
        private Movie movie;
        private string firstUserId = "1";
        private int firstProjectionId = 1;
        private int currentDay = DateTime.Now.Day;
        private int currentMonth = DateTime.Now.Month;
        private int currentYear = DateTime.Now.Year;
        private int currentHour = DateTime.Now.Hour;
        private int currentMinute = DateTime.Now.Minute;
        private int cityId = 1;
        private int openHourId = 1;
        private int movieId = 1;
        private int projectionSeats = 2;

        [TestInitialize]
        public void TestInitialize()
        {
            deletedReservation = new WatchedMovie()
            {
                UserId = firstUserId,
                ProjectionId = firstProjectionId,
                IsDeleted = true,
                Date = new DateTime(currentYear, currentDay, currentMonth)
            };
            validReservation = new WatchedMovie()
            {
                UserId = firstUserId,
                ProjectionId = firstProjectionId,
                IsDeleted = false,
                Date = new DateTime(currentYear, currentDay, currentMonth)
            };
            projection = new Projection()
            {
                Id = firstProjectionId,
                CityId = cityId,
                MovieId = movieId,
                OpenHourId = openHourId,
                Seats = projectionSeats,
            };
            openHour = new OpenHour() { Id = openHourId, Hours = currentHour, Minutes = currentMinute };
            movie = new Movie() { Id = movieId };
        }

        [TestMethod]
        public async Task ReturnListOfNotBookedProjections_WhenUserIdIsNull()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnListOfNotBookedProjections_WhenUserIdIsNull")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddAsync(projection);
                await actContext.AddAsync(validReservation);
                await actContext.AddAsync(movie);
                await actContext.AddAsync(openHour);

                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new ProjectionService(assertContext);
                var projections = await command.GetByTownId(cityId, "");

                Assert.AreEqual(false, projections.First().IsBooked);
                Assert.AreEqual(projectionSeats - 1, projections.First().Seats);
                //Someone already booked for this Projection
            }
        }

        [TestMethod]
        public async Task ReturnListOfNotBookedProjections_WhenUserDidntBookAny()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnListOfNotBookedProjections_WhenUserDidntBookAny")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddAsync(projection);
                await actContext.AddAsync(validReservation);
                await actContext.AddAsync(movie);
                await actContext.AddAsync(openHour);

                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new ProjectionService(assertContext);
                var projections = await command.GetByTownId(cityId, firstUserId);

                Assert.AreEqual(true, projections.First().IsBooked);
                Assert.AreEqual(projectionSeats - 1, projections.First().Seats);
            }
        }

        [TestMethod]
        public async Task ReturnListOfBookedProjections_WhenUserBookedAny()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnListOfBookedProjections_WhenUserBookedAny")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddAsync(projection);
                await actContext.AddAsync(deletedReservation);
                await actContext.AddAsync(movie);
                await actContext.AddAsync(openHour);

                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new ProjectionService(assertContext);
                var projections = await command.GetByTownId(cityId, firstUserId);

                Assert.AreEqual(false, projections.First().IsBooked);
                Assert.AreEqual(projectionSeats, projections.First().Seats);
            }
        }
    }
}
