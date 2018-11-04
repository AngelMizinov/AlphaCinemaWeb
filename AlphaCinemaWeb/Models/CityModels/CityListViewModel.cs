using AlphaCinemaData.Models;
using AlphaCinemaWeb.Models.ProjectionModels;
using System;
using System.Collections.Generic;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityListViewModel
    {
        public CityListViewModel(IEnumerable<CityViewModel> cities, ProjectionListViewModel projections)
        {
            this.Cities = cities;
            this.ListOfMovies = projections;
        }

        public ProjectionListViewModel ListOfMovies { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
