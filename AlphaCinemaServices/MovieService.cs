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
    public class MovieService : IMovieService
    {
        private readonly AlphaCinemaContext context;
        private Movie movie;

        public MovieService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public async Task AddMovie(string name, string description, string releaseYear, string duration)
        {
            if (name.Length > 50)
            {
                throw new ArgumentException("Movie name should be less than 50 characters");
            }

            movie = await this.GetMovie(name);

            //if movie object is null means that movie doesn't exist
            if(movie != null)
            {
                if (movie.IsDeleted)
                {
                    movie.IsDeleted = false;
                    await this.context.SaveChangesAsync();
                    return;
                }
                else
                {
                    throw new Exception($"\nCity {name} is already present in the database.");
                    //throw new EntityAlreadyExistException($"\nCity {cityName} is already present in the database.");

                }
            }
            else
            {
                movie = new Movie()
                {
                    Name = name,
                    Description = description,
                    ReleaseYear = int.Parse(releaseYear),
                    Duration = int.Parse(duration)
                };

                await this.context.AddAsync(movie);
                await this.context.SaveChangesAsync();
            }

        }

        public async Task DeleteMovie(string movieName)
        {
            movie = await this.GetMovie(movieName);

            if(movie == null || movie.IsDeleted)
            {
                throw new Exception($"\nMovie {movieName} is not present in the database.");
                //throw new EntityDoesntExistException($"\nMovie {movieName} is not present in the database.");
            }

            this.context.Remove(movie);
            await this.context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovie(string movieName)
        {
            return await this.context.Movies
                .Where(mov => mov.Name == movieName)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<Movie>> GetMovies()
        {
            return await this.context.Movies
                .ToListAsync();
        }

        public async Task UpdateName(string oldName, string newName)
        {
            if (oldName.Length > 50)
            {
                throw new ArgumentException("Movie name should be less than 50 characters");
            }

            movie = await this.GetMovie(oldName);

            if(movie == null || movie.IsDeleted)
            {
                throw new Exception($"\nMovie {oldName} is not present in the database.");
                //throw new EntityDoesntExistException($"\nMovie {oldName} is not present in the database.");
            }

            movie.Name = newName;

            this.context.Movies.Update(movie);
            await this.context.SaveChangesAsync();

        }
    }
}
