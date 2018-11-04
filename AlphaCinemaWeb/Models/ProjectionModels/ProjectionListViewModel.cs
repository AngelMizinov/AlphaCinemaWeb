using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionListViewModel
    {
        public ProjectionListViewModel(IEnumerable<ProjectionViewModel> projections,int cityID, DayOfWeek day)
        {
            this.Projections = projections;
            this.CityId = cityID;
            this.Day = day;
        }

        public DayOfWeek Day { get; set; }

        public IEnumerable<ProjectionViewModel> Projections { get; set; }

        public int CityId { get; set; }
    }
}
