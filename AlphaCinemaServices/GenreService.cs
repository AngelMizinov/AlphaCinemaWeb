using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Exceptions;
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

		public async Task AddGenre(string genreName)
		{
			if (genreName.Length > 50 || genreName.Length < 3)
			{
				throw new InvalidClientInputException("City name should be between 3 and 50 characters");
			}
			genre = await GetGenre(genreName);
			if (genre != null)
			{
				if (genre.IsDeleted)
				{
					genre.IsDeleted = false;
					await this.context.SaveChangesAsync();
					return;
				}
				else
				{

					throw new EntityAlreadyExistsException($"\nCity {genreName} is already present in the database.");
				}
			}
			else
			{
				genre = new Genre()
				{
					Name = genreName
				};
				await this.context.Genres.AddAsync(genre);
				await this.context.SaveChangesAsync();
			}
		}

		public async Task DeleteGenre(string genreName)
		{
			genre = await GetGenre(genreName);
			if (genre == null || genre.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nCity [{genreName}] is not present in the database.");
			}

			this.context.Genres.Remove(genre);
			await this.context.SaveChangesAsync();
		}

		public async Task<Genre> GetGenre(string genreName)
		{
			var genre = await this.context.Genres
				.Where(g => g.Name == genreName)
				.FirstOrDefaultAsync();
			return genre;
		}

		public async Task<Genre> GetGenre(int genreId)
		{
			var genre = await this.context.Genres
				.Where(g => g.Id == genreId)
				.FirstOrDefaultAsync();
			return genre;
		}

		public async Task<ICollection<Genre>> GetGenres()
		{
			var genres = await this.context.Genres
				.ToListAsync();
			return genres;
		}

		public async Task UpdateName(int genreId, string newName)
		{
			genre = await this.GetGenre(genreId);

			if (genre == null || genre.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nCity is not present in the database.");
			}

			if (genre.Name == newName)
			{
				throw new EntityAlreadyExistsException($"\nCity [{genre.Name}] is already present in the database");
			}

			if (genre.Name.Length > 50 || genre.Name.Length < 3
				|| newName.Length > 50 || newName.Length < 3)
			{
				throw new InvalidClientInputException("City name should be between 3 and 50 characters");
			}

			genre.Name = newName;

			this.context.Genres.Update(genre);
			await this.context.SaveChangesAsync();
		}
	}
}
