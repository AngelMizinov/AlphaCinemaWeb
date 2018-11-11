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
		private DbContextOptions<AlphaCinemaContext> contextOptions;
		private WatchedMovie watchedMovie;

		[TestMethod]
		public async Task ThrowEntityAlreadyExistsExceptionWhenParamsAreValid()
		{
			// Arrange
			contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "ThrowEntityAlreadyExistsExceptionWhenParamsAreValid")
				.Options;

			var userId = "userId";
			var projectionId = 5;

			watchedMovie = new WatchedMovie()
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
		public async Task SuccessfullyCreateNewWatchedMovieWhenParamsAreValid()
		{
			// Arrange
			contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "SuccessfullyCreateNewWatchedMovieWhenParamsAreValid")
				.Options;

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
