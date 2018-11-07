using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices
{
    public class GenreService : IGenreService
    {

        private readonly AlphaCinemaContext context;
        private Genre genre;

        public GenreService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public Task AddGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public Task<Genre> GetGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        public async Task<Genre> GetGenre(int genreId)
        {
            return await this.context.Genres
                .Where(genre => genre.Id == genreId)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return await this.context.Genres
                .ToListAsync();
        }

        public Task UpdateName(int genreId, string newName)
        {
            throw new NotImplementedException();
        }
    }
}
