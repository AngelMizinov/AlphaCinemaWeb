using System.Collections.Generic;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class TopProjectionsViewModel
    {
        public TopProjectionsViewModel(IEnumerable<ProjectionViewModel> projections)
        {
            this.Projections = projections;
        }

        public IEnumerable<ProjectionViewModel> Projections { get; set; }
    }

}
