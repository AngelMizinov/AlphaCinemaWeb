using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.MovieServiceTests
{
    [TestClass]
    public class GetMovies_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private Movie testMovieOne;
        private string testMovieOneName = "TestMovieOne";
        private Movie testMovieTwo;
        private string testMovieTwoName = "TestMovieTwo";
        private Movie testMovieThree;
        private string testMovieThreeName = "TestMovieThree";

        [TestInitialize]
        public void TestInitialize()
        {
            testMovieOne = new Movie() { Name = testMovieOneName };
            testMovieTwo = new Movie() { Name = testMovieTwoName };
            testMovieThree = new Movie() { Name = testMovieThreeName };
        }

        [TestMethod]
        public async Task ReturnCollectionOfMovies_WhenCalled()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ReturnCollectionOfMovies_WhenCalled")
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var listOfMovies = new List<Movie>() { testMovieOne, testMovieTwo, testMovieThree };

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
                var result = await command.GetMovies();
                Assert.IsTrue(result.Count() == 3);
                Assert.AreEqual(testMovieOneName, result.First().Name);
                Assert.AreEqual(testMovieTwoName, result.Skip(1).First().Name);
                Assert.AreEqual(testMovieThreeName, result.Skip(2).First().Name);
            }
        }
    }
}
