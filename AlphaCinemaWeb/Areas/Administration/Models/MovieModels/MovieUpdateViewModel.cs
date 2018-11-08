using AlphaCinemaData.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

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
            //Да се добавя от тук и снимката
        }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be non-negative integer")]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Movie length cannot be more tha 50 symbols.")]
        public string Name { get; set; }

        [MaxLength(400, ErrorMessage = "Description length cannot be more tha 400 symbols.")]
        public string Description { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Year must have only digits.")]
        [MinLength(4, ErrorMessage = "Year must contain 4 digits.")]
        [MaxLength(4, ErrorMessage = "Year must contain 4 digits.")]
        public string ReleaseYear { get; set; }

        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Duration must have only digits.")]
        public string Duration { get; set; }

        public IFormFile Image { get; set; }

        public string ImageString { get; set; }
    }
}
