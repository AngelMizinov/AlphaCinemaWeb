using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaWeb.Areas.Administration.Models.MovieModels
{
    public class MovieUpdateListViewModel
    {
        public MovieUpdateListViewModel()
        {

        }

        public MovieUpdateListViewModel(IEnumerable<MovieUpdateViewModel> movies)
        {
            this.Movies = movies;
        }

        public IEnumerable<MovieUpdateViewModel> Movies { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be non-negative integer")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Movie length cannot be more than 50 symbols.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(400, ErrorMessage = "Description length cannot be more than 400 symbols.")]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Year must have only digits.")]
        [MinLength(4, ErrorMessage = "Year must contain 4 digits.")]
        [MaxLength(4, ErrorMessage = "Year must contain 4 digits.")]
        public string ReleaseYear { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Duration must have only digits.")]
        public string Duration { get; set; }

        public IFormFile Image { get; set; }

        [Required]
        public string ImageString { get; set; }

        public string OldImageString { get; set; }
    }
}
