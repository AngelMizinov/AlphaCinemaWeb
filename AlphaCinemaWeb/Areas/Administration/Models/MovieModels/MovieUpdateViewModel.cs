using AlphaCinemaData.Models;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AlphaCinemaWeb.Areas.Administration.Models.MovieModels
{
    public class MovieUpdateViewModel
    {
        private readonly Regex imageUriPattern =
            new Regex(@"^data\:image\/(?<type>image\/(png|jpg|jpeg));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);

        public MovieUpdateViewModel()
        {

        }

        public MovieUpdateViewModel(Movie movie, string defaultImage)
        {
            this.Id = movie.Id;
            this.Name = movie.Name;
            this.Description = movie.Description;
            this.ReleaseYear = movie.ReleaseYear.ToString();
            this.Duration = movie.Duration.ToString();
            this.ImageString = !imageUriPattern.Match(movie.Image).Success ? defaultImage : movie.Image;
            this.OldImageString = !imageUriPattern.Match(movie.Image).Success ? defaultImage : movie.Image;
            //Този regex просто проверява дали стринга е валиден image
        }

        [Range(0, int.MaxValue, ErrorMessage = "Value must be non-negative integer")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Movie length cannot be more tha 50 symbols.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(400, ErrorMessage = "Description length cannot be more tha 400 symbols.")]
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
