using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices
{
    public class MovieGenreService : IMovieGenreService
    {
        private readonly AlphaCinemaContext context;
        private MovieGenre movieGenre;

        public MovieGenreService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public async Task AddNewMovieGenre(string movieId, string genreId)
        {
            movieGenre = await this.GetMovieGenre(movieId, genreId);

            if (movieGenre != null)
            {
                throw new EntityAlreadyExistsException("This movie genre already exists!");
            }
            else
            {
                movieGenre = new MovieGenre()
                {
                    MovieId = int.Parse(movieId),
                    GenreId = int.Parse(genreId)
                };

                await this.context.MoviesGenres.AddAsync(movieGenre);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task AddNewMovieGenre(Movie movie, Genre genre)
        {
            movieGenre = await this.GetMovieGenre(movie.Id.ToString(), genre.Id.ToString());

            if (movieGenre != null)
            {
                throw new EntityAlreadyExistsException("This movie genre already exists!");
            }
            else
            {
                movieGenre = new MovieGenre()
                {
                    MovieId = movie.Id,
                    GenreId = genre.Id
                };

                await this.context.MoviesGenres.AddAsync(movieGenre);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Genre>> GetGenresByMovieId(string movieId)
        {
            var genres = await this.context.Movies
                .Where(mov => mov.Id == int.Parse(movieId))
                .Select(mov => mov.MovieGenres
                    .Select(mg => mg.Genre)
                    .FirstOrDefault())
                 .ToListAsync();

            return genres;
        }

        public async Task<MovieGenre> GetMovieGenre(string movieId, string genreId)
        {
            return await this.context.MoviesGenres
                .Where(movieGenre => movieGenre.MovieId == int.Parse(movieId)
                && movieGenre.GenreId == int.Parse(genreId))
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Movie>> GetMoviesByGenreId(string genreId)
        {
            var movies = await this.context.Genres
                .Where(genre => genre.Id == int.Parse(genreId))
                .Select(genre => genre.MoviesGenres
                    .Select(mg => mg.Movie)
                    .FirstOrDefault())
                 .ToListAsync();

            return movies;
        }
    }
}
