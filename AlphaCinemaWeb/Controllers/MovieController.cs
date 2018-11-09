using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Models.ProjectionModels;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinemaWeb.Controllers
{
    public class MovieController : Controller
    {
        private IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Detail(int movieId)
        {
            //var movie = movieService.GetMovie(movieId);


            return View();
        }
    }
}
