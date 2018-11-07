using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;
using System.Linq;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionViewModel
    {
        public ProjectionViewModel(Projection projection)
        {
            this.Seats = projection.Seats;
            this.ProjectionId = projection.Id;
            this.IsBooked = projection.IsBooked;
            this.MovieName = projection.Movie.Name;
            this.MovieDuration = projection.Movie.Duration;
            this.MovieDescription = projection.Movie.Description;
            this.Genres = projection.Movie.MovieGenres.Select(mg => mg.Genre.Name);
            this.MovieStart = $"{projection.OpenHour.Hours:D2}:{projection.OpenHour.Minutes:D2}h";
        }

        public bool IsBooked { get; set; }

        public int ProjectionId { get; set; }

        public int Seats { get; set; }

        public string MovieStart { get; set; }

        public string MovieName { get; set; }

        public int MovieDuration { get; set; }

        public IEnumerable<string> Genres { get; set; }

        public string ImageUrl { get; set; }

        public string MovieDescription { get; private set; }
    }
}
