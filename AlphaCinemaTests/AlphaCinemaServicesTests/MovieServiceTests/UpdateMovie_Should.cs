using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.MovieServiceTests
{
    [TestClass]
    public class UpdateMovie_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private int movieId = 1;
        private string oldMovieName = "OldTestMovie";
        private int oldMovieDuration = 200;
        private string oldMovieDescription = "OldDecription";
        private int oldRealeaseYear = 1000;
        private string newMovieName = "NewTestMovie";
        private string newMovieDuration = "400";
        private string newMovieDescription = "NewDecription";
        private string newRealeaseYear = "2000";
        private Movie movie;

        [TestInitialize]
        public void TestInitialize()
        {
            movie = new Movie()
            {
                Id = 1,
                Name = oldMovieName,
                Description = oldMovieDescription,
                ReleaseYear = oldRealeaseYear,
                Duration = oldMovieDuration
            };
        }

        [TestMethod]
        public async Task UpdateMovieParameters_WhenPassedParametersAreValid()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "UpdateMovieParameters_WhenPassedParametersAreValid")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.AddAsync(movie);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                var movie = await command.UpdateMovie(movieId, newMovieName, newMovieDescription, newRealeaseYear, newMovieDuration, "");
                Assert.AreEqual(newMovieName, movie.Name);
                Assert.AreEqual(newMovieDescription, movie.Description);
                Assert.AreEqual(int.Parse(newRealeaseYear), movie.ReleaseYear);
                Assert.AreEqual(int.Parse(newMovieDuration), movie.Duration);
            }
        }

        [TestMethod]
        public async Task ThrowEntityDoesntExistException_WhenMovieDesntExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistException_WhenMovieDesntExist")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(async () => await command.UpdateMovie(movieId, newMovieName, newMovieDescription, newRealeaseYear, newMovieDuration, ""));
            }
        }
    }
}
