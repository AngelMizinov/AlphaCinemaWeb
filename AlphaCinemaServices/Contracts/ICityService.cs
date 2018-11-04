using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCinemaData.Models;

namespace AlphaCinemaServices.Contracts
{
    public interface ICityService
    {
        ICollection<City> GetCities();
		Task<City> GetCity(string cityName);
		Task AddCity(string cityName);

	}
}