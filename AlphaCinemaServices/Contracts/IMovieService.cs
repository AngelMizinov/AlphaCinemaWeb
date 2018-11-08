using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices.Contracts
{
    public interface IMovieService
    {
        Task<ICollection<Movie>> GetMovies();
        Task<Movie> GetMovie(string movieName);
        Task<Movie> AddMovie(string name,string description,string releaseYear,string duration);
        Task DeleteMovie(string movieName);
        Task UpdateName(string oldName, string newName);
    }
}
