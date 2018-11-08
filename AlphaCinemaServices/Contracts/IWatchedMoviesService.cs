using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices.Contracts
{
	public interface IWatchedMoviesService
	{
		Task<WatchedMovie> GetWatchedMovie(string userId, int projectionId);
		//Task<ICollection<Movie>> GetWatchedMoviesByUserId(string userId);
		Task<ICollection<WatchedMovie>> GetWatchedMoviesByUserId(string userId);
		Task AddNewWatchedMovie(string userId, int resevationId);
	}
}
