using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.MovieServiceTests
{
    [TestClass]
    public class GetMovie_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private Movie testMovieOne;
        private string testMovieOneName = "TestMovieOne";
        private Movie testMovieTwo;
        private string testMovieTwoName = "TestMovieTwo";
        private int movieId = 1;

        [TestInitialize]
        public void TestInitialize()
        {
            testMovieOne = new Movie() { Name = testMovieOneName };
            testMovieTwo = new Movie() { Name = testMovieTwoName };
        }

        [TestMethod]
        public async Task ReturnMovie_WhenIdIsValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnMovie_WhenIdIsValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var listOfMovies = new List<Movie>() { testMovieOne, testMovieTwo};

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
                var result = await command.GetMovie(movieId);
                Assert.AreEqual(testMovieOneName, result.Name);
                Assert.AreEqual(movieId, result.Id);
            }
        }

        [TestMethod]
        public async Task ReturnMovie_WhenNameIsValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnMovie_WhenNameIsValid")
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
                var result = await command.GetMovie(testMovieOneName);
                Assert.AreEqual(testMovieOneName, result.Name);
                Assert.AreEqual(movieId, result.Id);
            }
        }
    }
}
