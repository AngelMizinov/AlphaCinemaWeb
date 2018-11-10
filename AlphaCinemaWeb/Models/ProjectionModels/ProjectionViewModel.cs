using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionViewModel
    {
        private static readonly Regex imageUriPattern =
           new Regex(@"^data\:image\/(?<type>image\/(png|jpg|jpeg));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public ProjectionViewModel(Projection projection, string defaultImage)
        {//Regex-a проверява дали стринга на филма е валидна снимка, ако не е му присвояв default-на стойност
            this.Image = !imageUriPattern.Match(projection.Movie.Image).Success ? defaultImage : projection.Movie.Image;
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

        public string Image { get; set; }

        public string MovieDescription { get; private set; }
    }
}
