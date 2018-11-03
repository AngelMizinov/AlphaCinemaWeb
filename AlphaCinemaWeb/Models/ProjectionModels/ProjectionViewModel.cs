using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaData.Models;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionViewModel
    {
        public ProjectionViewModel(Projection projection)
        {
            this.MovieStart = projection.OpenHour.Hours + ":" + projection.OpenHour.Minutes + "h";
            this.MovieName = projection.Movie.Name;
            this.MovieDuration = projection.Movie.Duration;
            this.MovieDescription = projection.Movie.Description;
            this.Genres = projection.Movie.MovieGenres.Select(mg => mg.Genre.Name);
        }
        public string MovieStart { get; set; }

        public string MovieName { get; set; }

        public int MovieDuration { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string ImageUrl { get; set; }

        public string MovieDescription { get; private set; }
    }
}
