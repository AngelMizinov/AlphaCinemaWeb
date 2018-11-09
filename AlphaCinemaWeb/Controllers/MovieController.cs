using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Models.GenreViewModels;
using AlphaCinemaWeb.Models.MovieViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService movieService;
        private readonly IMovieGenreService movieGenreService;

        public MovieController(IMovieService movieService, IMovieGenreService movieGenreService)
        {
            this.movieService = movieService;
            this.movieGenreService = movieGenreService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string movieName)
        {
            var movie = await this.movieService.GetMovie(movieName);

            //get genres
            var genres = await this.movieGenreService.GetGenresByMovieId(movie.Id.ToString());

            var genresModels = genres.Select(genre => new GenreViewModel()
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();

            var model = new MovieViewModel()
            {
                Name = movie.Name,
                Description = movie.Description,
                ReleaseYear = movie.ReleaseYear.ToString(),
                Duration = movie.Duration.ToString(),
                Genres = genresModels
            };
            
            return View(model);
        }
     
    }
}
