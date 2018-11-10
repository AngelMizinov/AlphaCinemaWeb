using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.WatchedMovieServiceTests
{
	[TestClass]
	public class AddNewWatchedMovie_Should
	{
		private readonly DbContextOptions<AlphaCinemaContext> contextOptions =
			new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
				.Options;

		[TestMethod]
		public async Task Throw_EntityAlreadyExistsException_When_UserId_And_ProjectionId_Are_ValidAsync()
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
				await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(() => 
				watchedMoviesService.AddNewWatchedMovie(userId, projectionId));
			}
		}

		[TestMethod]
		public async Task Successfully_Create_New_WatchedMovie_When_UserId_And_ProjectionId_Are_Valid()
		{
			// Arrange
			var userId = "djoni";
			var projectionId = 6;

			// Act && Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var watchedMoviesService = new WatchedMoviesService(assertContext);
				await watchedMoviesService.AddNewWatchedMovie(userId, projectionId);
				var watchedMovie = await watchedMoviesService.GetWatchedMovie(userId, projectionId);

				Assert.AreEqual(userId, watchedMovie.UserId);
				Assert.AreEqual(projectionId, watchedMovie.ProjectionId);
			}
		}
	}
}
