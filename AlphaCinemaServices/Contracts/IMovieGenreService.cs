using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieGenreService
    {
        Task<MovieGenre> GetMovieGenre(string movieId, string genreId);

        Task<ICollection<Genre>> GetGenresByMovieId(string movieId);

        Task<ICollection<Movie>> GetMoviesByGenreId(string genreId);
        
        Task AddNewMovieGenre(string movieId, string genreId);

        Task AddNewMovieGenre(Movie movie,Genre genre);

    }
}
