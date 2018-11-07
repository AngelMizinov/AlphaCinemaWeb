﻿using AlphaCinemaData.Context;
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

        public async Task<City> GetCity(int cityId)
        {
            var city = await this.context.Cities
                .Where(c => c.Id== cityId)
                .FirstOrDefaultAsync();
            return city;
        }

        public async Task AddCity(string cityName)
		{
			if (cityName.Length > 50 || cityName.Length < 3)
			{
				throw new InvalidClientInputException("City name should be between 3 and 50 characters");
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

					throw new EntityAlreadyExistsException($"\nCity {cityName} is already present in the database.");
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
				throw new EntityDoesntExistException($"\nCity [{cityName}] is not present in the database.");
			}

			this.context.Cities.Remove(city);
			await this.context.SaveChangesAsync();
		}

		public async Task UpdateName(int cityId, string newName)
		{
            city = await this.GetCity(cityId);

			//if (oldName == newName)
			//{
			//	throw new EntityAlreadyExistsException($"\nCity [{oldName}] is already present in the database");
			//}

			//if (oldName.Length > 50 || oldName.Length < 3
			//	|| newName.Length > 50 || newName.Length < 3)
			//{
			//	throw new InvalidClientInputException("City name should be between 3 and 50 characters");
			//}
            
			if (city == null || city.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nCity is not present in the database.");
			}
			city.Name = newName;

			this.context.Cities.Update(city);
			await this.context.SaveChangesAsync();
		}
	}
}
