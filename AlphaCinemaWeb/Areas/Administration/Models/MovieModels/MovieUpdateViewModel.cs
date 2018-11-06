using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.MovieModels
{
    public class MovieUpdateViewModel
    {
        public MovieUpdateViewModel()
        {

        }

        public MovieUpdateViewModel(Movie movie)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.ReleaseYear = movie.ReleaseYear.ToString();
            this.Duration = movie.Duration.ToString();
        }

        public int Id { get; set; }

        [MaxLength(50,ErrorMessage ="Movie length cannot be more tha 50 symbols.")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage = "Movie length cannot be more tha 50 symbols.")]
        public string OldName { get; set; }

        public string Description { get; set; }

        public string OldDescription { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Year must have only digits.")]
        [MinLength(4, ErrorMessage = "Year must contain 4 digits.")]
        [MaxLength(4, ErrorMessage = "Year must contain 4 digits.")]
        public string ReleaseYear { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Year must have only digits.")]
        [MinLength(4, ErrorMessage = "Year must contain 4 digits.")]
        [MaxLength(4, ErrorMessage = "Year must contain 4 digits.")]
        public string OldReleaseYear { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Duration must have only digits.")]
        public string Duration { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Duration must have only digits.")]
        public string OldDuration { get; set; }
    }
}
