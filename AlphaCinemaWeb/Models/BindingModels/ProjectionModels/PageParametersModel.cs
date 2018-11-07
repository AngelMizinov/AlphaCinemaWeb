using System;

namespace AlphaCinemaWeb.Models.BindingModels.ProjectionModels
{
    public class PageParametersModel
    {
        public PageParametersModel()
        {

        }

        public int CityId { get; set; }

        public DayOfWeek Day { get; set; }

        public string UserId { get; set; }

        public string SortOrder { get; set; }

        public int? CurrentPage { get; set; }

    }
}
