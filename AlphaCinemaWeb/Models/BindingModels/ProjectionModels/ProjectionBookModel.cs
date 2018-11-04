using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Models.BindingModels.ProjectionModels
{
    public class ProjectionBookModel
    {
        public ProjectionBookModel()
        {

        }

        public string UserName { get; set; }

        public int ProjectionId { get; set; }

        public DayOfWeek Day { get; set; }

        public int CityId { get; set; }
    }
}
