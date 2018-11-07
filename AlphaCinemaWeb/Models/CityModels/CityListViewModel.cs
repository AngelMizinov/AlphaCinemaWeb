using AlphaCinemaWeb.Models.ProjectionModels;
using System.Collections.Generic;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityListViewModel
    {
        public CityListViewModel(IEnumerable<CityViewModel> cities, TopProjectionsViewModel projections)
        {
            this.Cities = cities;
            this.ListOfMovies = projections;
        }

        public TopProjectionsViewModel ListOfMovies { get; set; }

        public IEnumerable<CityViewModel> Cities { get; set; }
    }
}
