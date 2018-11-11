using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.MovieServiceTests
{
    [TestClass]
    public class AddMovie_Should
    {
        private ServiceProvider serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
        private string movieName = "OldTestMovie";
        private string movieDescription = "OldDecription";
        private string movieDuration = "400";
        private string releaseYear = "2000";
        private Movie movie;

        [TestInitialize]
        public void TestInitialize()
        {
            movie = new Movie()
            {
                Name = movieName,
                Description = movieDescription,
                Duration = int.Parse(movieDuration),
                ReleaseYear = int.Parse(releaseYear),
            };
        }

        [TestMethod]
        public async Task AddNewMovie_WhenParametersAreCorrect()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "AddNewMovie_WhenParametersAreCorrect")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            //Act
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(actContext);
                await command.AddMovie(movieName, movieDescription, releaseYear, movieDuration);
                await actContext.SaveChangesAsync();
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.AreEqual(1, await assertContext.Movies.CountAsync());
                Assert.AreEqual(movieName, assertContext.Movies.First().Name);
                Assert.AreEqual(movieDescription, assertContext.Movies.First().Description);
                Assert.AreEqual(int.Parse(movieDuration), assertContext.Movies.First().Duration);
                Assert.AreEqual(int.Parse(releaseYear), assertContext.Movies.First().ReleaseYear);
            }
        }

        [TestMethod]
        public async Task ThrowException_WhenMovieAlreadyExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ThrowException_WhenMovieAlreadyExist")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var arrangeContext = new AlphaCinemaContext(contextOptions))
            {
                await arrangeContext.AddAsync(movie);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await Assert.ThrowsExceptionAsync<Exception>(async () => await command.AddMovie(movieName, movieDescription, releaseYear, movieDuration));
            }
        }

        [TestMethod]
        public async Task ChangeIsDeletedToFalse_WhenMovieAlreadyExist()
        {
            // Arrange
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
                .UseInMemoryDatabase(databaseName: "ChangeIsDeletedToFalse_WhenMovieAlreadyExist")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            using (var arrangeContext = new AlphaCinemaContext(contextOptions))
            {
                movie.IsDeleted = true;
                await arrangeContext.AddAsync(movie);
                await arrangeContext.SaveChangesAsync();
            }

            //Act and Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var command = new MovieService(assertContext);
                await command.AddMovie(movieName, movieDescription, releaseYear, movieDuration);
                Assert.IsTrue(!assertContext.Movies.First().IsDeleted);
                Assert.AreEqual(1, await assertContext.Movies.CountAsync());
                Assert.AreEqual(movieName, assertContext.Movies.First().Name);
                Assert.AreEqual(movieDescription, assertContext.Movies.First().Description);
                Assert.AreEqual(int.Parse(movieDuration), assertContext.Movies.First().Duration);
                Assert.AreEqual(int.Parse(releaseYear), assertContext.Movies.First().ReleaseYear);
            }
        }
    }
}
