using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionListViewModel
    {
        public IEnumerable<ProjectionViewModel> Projections { get; set; }

        public int CityId { get; set; }

        public ProjectionListViewModel(IEnumerable<ProjectionViewModel> projections,int cityID)
        {
            this.Projections = projections;
        }
    }
}
