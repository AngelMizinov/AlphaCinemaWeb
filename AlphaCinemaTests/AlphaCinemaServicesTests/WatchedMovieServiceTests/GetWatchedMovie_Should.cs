using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.WatchedMovieServiceTests
{
	[TestClass]
	public class GetWatchedMovie_Should
	{
		private readonly DbContextOptions<AlphaCinemaContext> contextOptions =
			new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
				.Options;

		[TestMethod]
		public async Task ReturnWatchedMovieWhenParamsAreInDatabase()
		{
			// Arrange
			var userId = "userId";
			var projectionId = 5;
			
			var watchedMovie = new WatchedMovie()
			{
				UserId = userId,
				ProjectionId = projectionId
			};

			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				await actContext.WatchedMovies.AddAsync(watchedMovie);
				await actContext.SaveChangesAsync();
			}

			// Act && Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var watchedMoviesService = new WatchedMoviesService(assertContext);
				var result = await watchedMoviesService.GetWatchedMovie(userId, projectionId);

				Assert.IsNotNull(result);
				Assert.AreEqual(result.ProjectionId, projectionId);
				Assert.AreEqual(result.UserId, userId);
			}
		}

		[TestMethod]
		public async Task ReturnNullWhenUserIsNotFound()
		{
			// Arrange
			var userId = "userId";
			var projectionId = 5;
			// Assert && Act
			var watchedMovie = new WatchedMovie()
			{
				UserId = userId,
				ProjectionId = projectionId
			};

			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				await actContext.WatchedMovies.AddAsync(watchedMovie);
				await actContext.SaveChangesAsync();
			}
			var serviceProviderMock = new Mock<IServiceProvider>();

			// Act && Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var watchedMoviesService = new WatchedMoviesService(assertContext);
				var result = await watchedMoviesService.GetWatchedMovie("no user id", projectionId);

				Assert.IsNull(result);
			}
		}
	}
}
