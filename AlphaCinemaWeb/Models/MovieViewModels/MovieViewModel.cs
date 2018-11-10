using AlphaCinemaData.Models;
using AlphaCinemaWeb.Models.GenreViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaWeb.Models.MovieViewModels
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {
            this.Genres = new List<GenreViewModel>();
        }

        public MovieViewModel(Movie movie)
        {
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.ReleaseYear = movie.ReleaseYear.ToString();
            this.Duration = movie.Duration.ToString();
            
        }
        
        [Required]
        [MaxLength(50, ErrorMessage = "Movie length cannot be more tha 50 symbols.")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$",ErrorMessage ="Year must have only digits.")]
        [MinLength(4,ErrorMessage ="Year must contain 4 digits.")]
        [MaxLength(4, ErrorMessage = "Year must contain 4 digits.")]
        public string ReleaseYear { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Duration must have only digits.")]
        public string Duration { get; set; }

        public List<GenreViewModel> Genres{ get; set; }

    }
}
