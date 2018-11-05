using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.MovieModels
{
    public class MovieRemoveViewModel
    {
        public MovieRemoveViewModel()
        {

        }

        public MovieRemoveViewModel(string movieName)
        {
            Name = movieName;
        }

        public string Name { get; set; }
    }
}
