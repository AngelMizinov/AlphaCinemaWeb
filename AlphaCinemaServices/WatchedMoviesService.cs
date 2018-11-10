using AlphaCinemaData.Context;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AlphaCinemaData.Models;

namespace AlphaCinemaServices
{
	public class WatchedMoviesService : IWatchedMoviesService
	{
		private readonly AlphaCinemaContext context;
		private WatchedMovie watchedMovie;

		public WatchedMoviesService(AlphaCinemaContext context)
		{
			this.context = context;
		}

		public async Task AddNewWatchedMovie(string userId, int resevationId)
		{
			watchedMovie = await GetWatchedMovie(userId, resevationId);
			if (watchedMovie != null)
			{
				throw new EntityAlreadyExistsException("You have already booked this projection");
			}
			else
			{
				watchedMovie = new WatchedMovie()
				{
					UserId = userId,
					ProjectionId = resevationId
				};
				await this.context.WatchedMovies.AddAsync(watchedMovie);
				await this.context.SaveChangesAsync();
			}
		}

		public async Task<ICollection<WatchedMovie>> GetWatchedMoviesByUserId(string userId)
		{
			var watchedMovies = await this.context.WatchedMovies
				.Where(u => u.UserId == userId)
				.Include(p => p.Projection)
				.ThenInclude(c => c.City)
				.Include(p => p.Projection)
				.ThenInclude(m => m.Movie)
				.Include(p => p.Projection)
				.ThenInclude(oh => oh.OpenHour)		
				.Select(wm => wm)
				.ToListAsync();

            return watchedMovies;
		}

		public async Task<WatchedMovie> GetWatchedMovie(string userId, int projectionId)
		{
			return await this.context.WatchedMovies
				.Where(wm => wm.UserId == userId)
				.Where(wm => wm.ProjectionId == projectionId)
				.FirstOrDefaultAsync();
		}
	}
}
