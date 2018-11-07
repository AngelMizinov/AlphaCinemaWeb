using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices.Contracts
{
    public interface IGenreService
    {
        Task<ICollection<Genre>> GetGenres();

        Task<Genre> GetGenre(string genreName);

        Task<Genre> GetGenre(int genreId);

        Task AddGenre(string genreName);

        Task DeleteGenre(string genreName);

        Task UpdateName(int genreId, string newName);
    }
}
