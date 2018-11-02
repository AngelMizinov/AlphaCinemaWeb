using System.Collections.Generic;
using AlphaCinemaData.Models;

namespace AlphaCinemaServices.Contracts
{
    public interface ICityService
    {
        ICollection<City> GetCities();
    }
}