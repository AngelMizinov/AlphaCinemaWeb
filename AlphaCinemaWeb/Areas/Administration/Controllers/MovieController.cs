using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Areas.Administration.Models.MovieModels;
using AlphaCinemaWeb.Models.MovieViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinemaWeb.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;
        private readonly IGenreService genreService;

        public MovieController(IMovieService movieService, IGenreService genreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //take all genres from Database
            var allGenres = await this.genreService.GetGenres();

            this.ViewBag.Genres = allGenres;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var movie = await this.movieService.GetMovie(viewModel.Name);

            if (movie != null)
            {
                this.TempData["Error-Message"] = $"Movie with name {viewModel.Name} already exist!";
                return this.View();
            }

            try
            {
                await this.movieService.AddMovie(viewModel.Name, viewModel.Description,
                    viewModel.ReleaseYear, viewModel.Duration); ;
            }
            catch (Exception ex)
            {
                this.TempData["Error-Message"] = ex.Message;
                return this.View();
            }

            this.TempData["Success-Message"] = $"You successfully added movie with name {viewModel.Name}!";

            return this.View();
        }


        [HttpGet]
        public IActionResult Remove()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(MovieRemoveViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var movie = await this.movieService.GetMovie(viewModel.Name);

            if (movie == null)
            {
                this.TempData["Error-Message"] = $"Movie with name {viewModel.Name} doesn't exist!";
                return this.View();
            }

            try
            {
                await this.movieService.DeleteMovie(viewModel.Name);
            }
            catch (Exception ex)
            {
                this.TempData["Error-Message"] = ex.Message;
                return this.View();
            }

            this.TempData["Success-Message"] = $"You successfully deleted movie with name {viewModel.Name}!";
            return this.View();
        }

        [HttpGet]
        public async Task<ActionResult> Update()
        {
            var movies = await this.movieService.GetMovies();//Тук реално ни трябват само имената на филмите

            var model = movies.Select(movie => new MovieUpdateViewModel(movie));

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovie(string movieName)
        {
            var movie = await this.movieService.GetMovie(movieName);
            this.ViewBag.MovieName = movieName;
            var model = new MovieUpdateViewModel(movie);

            return PartialView("_MovieInputPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieUpdateViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var movie = await this.movieService.GetMovie(viewModel.Name);

            if (movie == null)
            {
                this.TempData["Error-Message"] = $"Movie with name [{viewModel.Name}] doesn't exist!";
                return this.View();
            }

            //try
            //{
            //    await this.movieService.UpdateName(viewModel.OldName, viewModel.Name);
            //}
            //catch (EntityAlreadyExistsException e)
            //{
            //    this.TempData["Error-Message"] = e.Message;
            //    return this.View();
            //}
            //catch (InvalidClientInputException e)
            //{
            //    this.TempData["Error-Message"] = e.Message;
            //    return this.View();
            //}
            //catch (EntityDoesntExistException e)
            //{
            //    this.TempData["Error-Message"] = e.Message;
            //    return this.View();
            //}

            this.TempData["Success-Message"] = $"You successfully changed movie name {viewModel.Name}!";
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Avatar([FromForm] MovieUpdateViewModel movieModel)
        {//Файлът(снимката), която получим ще дойде тук от този формат
            if (movieModel.Image == null)
            {
                this.ViewBag.Message = "Error: Please provide an image";
                return this.PartialView("_MovieInputPartial", movieModel);
            }

            if (!this.IsValidImage(movieModel.Image))
            {
                this.ViewBag.Message = "Error: Please provide a .jpg, .png ro jpeg file smaller than 1MB";
                return this.PartialView("_MovieInputPartial", movieModel);
            }

            using (var ms = new MemoryStream())
            {
                movieModel.Image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(fileBytes);
                movieModel.ImageString = string.Format("data:image/{0};base64,{1}", movieModel.Image.ContentType, base64);
            }

            this.ViewBag.Success = "Movie image updated";

            return this.PartialView("_MovieInputPartial", movieModel);
        }

        private bool IsValidImage(IFormFile image)
        {
            string type = image.ContentType;
            if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
            {
                return false;
            }

            return image.Length <= 1024 * 1024;
        }
    }
}
