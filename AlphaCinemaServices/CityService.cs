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
	public class CityService : ICityService
	{
		private readonly AlphaCinemaContext context;
		private City city;

		public CityService(AlphaCinemaContext context)
		{
			this.context = context;
		}

		public async Task<ICollection<City>> GetCities()
		{
			var cities = await this.context.Cities
				.ToListAsync();
			return cities;
		}

		public async Task<City> GetCity(string cityName)
		{
			var city = await this.context.Cities
				.Where(c => c.Name == cityName)
				.FirstOrDefaultAsync();
			return city;
		}

		public async Task AddCity(string cityName)
		{
			if (cityName.Length > 50)
			{
				throw new ArgumentException("City name should be less than 50 characters");
			}

			city = await GetCity(cityName);
			if (city != null)
			{
				if (city.IsDeleted)
				{
					city.IsDeleted = false;
					await this.context.SaveChangesAsync();
					return;
				}
				else
				{
					throw new Exception($"\nCity {cityName} is already present in the database.");

					//throw new EntityAlreadyExistsException("\nCity is already present in the database.");
				}
			}
			else
			{
				city = new City()
				{
					Name = cityName
				};
				await this.context.Cities.AddAsync(city);
				await this.context.SaveChangesAsync();
			}
		}

		public async Task DeleteCity(string cityName)
		{
			city = await GetCity(cityName);
			if (city == null || city.IsDeleted)
			{
				throw new Exception($"\nCity {cityName} is not present in the database.");
			}

			this.context.Cities.Remove(city);
			await this.context.SaveChangesAsync();
		}

		public async Task UpdateName(string oldName, string newName)
		{
			if (oldName.Length > 50)
			{
				throw new ArgumentException("City name should be less than 50 characters");
			}

			city = await GetCity(oldName);

			if (city == null || city.IsDeleted)
			{
				throw new Exception($"\nCity {oldName} is not present in the database.");
			}
			city.Name = newName;

			this.context.Cities.Update(city);
			await this.context.SaveChangesAsync();
		}
	}
}
