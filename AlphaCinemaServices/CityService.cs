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

        public ICollection<City> GetCities()
        {
            return this.context.Cities.ToList();
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
				throw new ArgumentException("City Name can't be more than 50 characters");
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
					throw new Exception("\nCity is already present in the database.");

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
	}
}
