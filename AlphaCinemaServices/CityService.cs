using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlphaCinemaServices
{
    public class CityService : ICityService
    {
        private readonly AlphaCinemaContext context;

        public CityService(AlphaCinemaContext context)
        {
            this.context = context;
        }

        public ICollection<City> GetCities()
        {
            return this.context.Cities.ToList();
        }
    }
}
