using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionListViewModel
    {
        public ProjectionListViewModel(IEnumerable<ProjectionViewModel> projections)
        {
            this.Projections = projections;
        }

        public IEnumerable<ProjectionViewModel> Projections { get; set; }
    }
}
