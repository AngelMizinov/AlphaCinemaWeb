using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityListViewModel
    {
        public CityListViewModel(IEnumerable<CityViewModel> cities)
        {
            this.Cities = cities;
        }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
