using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.MovieServiceTests
{
    [TestClass]
    public class DeleteMovie_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private Movie testMovieOne;
        private string testMovieOneName = "TestMovieOne";
        private Movie testMovieTwo;
        private string testMovieTwoName = "TestMovieTwo";
        private Movie deletedMovie;
        private string deletedMovieName = "DeletedMovie";

        [TestInitialize]
        public void TestInitialize()
        {
            testMovieOne = new Movie() { Name = testMovieOneName };
            testMovieTwo = new Movie() { Name = testMovieTwoName };
            deletedMovie = new Movie() { Name = deletedMovieName, IsDeleted = true };
        }

        [TestMethod]
        public async Task DeleteMovie_WhenNameIsValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "DeleteMovie_WhenNameIsValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var listOfMovies = new List<Movie>() { testMovieOne, testMovieTwo };

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddRangeAsync(listOfMovies);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await command.DeleteMovie(testMovieOneName);
                Assert.IsTrue(assertContext.Movies.First().IsDeleted);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenMovieDoesntExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException_WhenMovieDoesntExist")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            // Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await Assert.ThrowsExceptionAsync<Exception>(async () => await command.DeleteMovie(testMovieOneName));
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenMovieIsDeleted()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException_WhenMovieIsDeleted")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var listOfMovies = new List<Movie>() { testMovieOne, testMovieTwo, deletedMovie };

            // Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await Assert.ThrowsExceptionAsync<Exception>(async () => await command.DeleteMovie(deletedMovieName));
            }
        }
    }
}
