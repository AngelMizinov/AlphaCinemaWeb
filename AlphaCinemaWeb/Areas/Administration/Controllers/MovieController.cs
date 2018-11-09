using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Areas.Administration.Models.GenreViewModels;
using AlphaCinemaWeb.Areas.Administration.Models.MovieModels;
using AlphaCinemaWeb.Exceptions;
using AlphaCinemaWeb.Models.GenreViewModels;
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
        private readonly IMovieGenreService movieGenreService;

        public MovieController(IMovieService movieService, IGenreService genreService, IMovieGenreService movieGenreService)
        {
            this.movieService = movieService;
            this.genreService = genreService;
            this.movieGenreService = movieGenreService;
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

            var models = allGenres
                .Select(genre => new GenreViewModel(genre))
                .ToList();

            var movieModel = new MovieViewModel()
            {
                Genres = models
            };

            return View(movieModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return RedirectToAction("Add");
            }

            var movie = await this.movieService.GetMovie(viewModel.Name);

            if (movie != null)
            {
                this.TempData["Error-Message"] = $"Movie with name {viewModel.Name} already exist!";
                return RedirectToAction("Add");
            }

            try
            {
                movie = await this.movieService.AddMovie(viewModel.Name, viewModel.Description,
                    viewModel.ReleaseYear, viewModel.Duration); ;
            }
            catch (Exception ex)
            {
                this.TempData["Error-Message"] = ex.Message;
                return RedirectToAction("Add");
            }

            //
            //get all genres that the Admin has choosed and add them to movie
            foreach (var genre in viewModel.Genres)
            {
                if (genre.IsChecked)
                {
                    Genre g = new Genre()
                    {
                        Id = genre.Id,
                        Name = genre.Name
                    };

                    try
                    {
                        //just add new MovieGenre
                        await this.movieGenreService.AddNewMovieGenre(movie, g);
                    }
                    catch (EntityAlreadyExistsException ex)
                    {
                        this.TempData["Error-Message"] = ex.Message;
                        return RedirectToAction("Add");
                    }
                    catch (Exception ex)
                    {
                        this.TempData["Error-Message"] = ex.Message;
                        return RedirectToAction("Add");
                    }
                }
            }
            //

            this.TempData["Success-Message"] = $"You successfully added movie with name {viewModel.Name}!";
            return RedirectToAction("Add");
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
            var movies = await this.movieService.GetMovies();

            var model = movies.Select(movie => new MovieUpdateViewModel(movie));

            return this.View(model);
        }

        [HttpPost]
        public IActionResult GetMovie(MovieUpdateViewModel movie)
        {
            this.ViewBag.MovieName = movie.Name;
            this.ViewBag.MovieOldImage = movie.ImageString;

            return PartialView("_MovieInputPartial", movie);
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
        public IActionResult Avatar([FromForm] MovieUpdateViewModel movie)
        {//Файлът(снимката), която получим ще дойде тук от този формат
            this.ViewBag.MovieName = movie.Name;
            this.ViewBag.MovieOldImage = movie.ImageString;

            if (movie.Image == null && movie.ImageString == null)
            {
                this.ViewBag.Message = "Please provide an image";
                return this.PartialView("_MovieInputPartial", movie);
            }

            if (!this.IsValidImage(movie.Image))
            {
                this.ViewBag.Message = "Please provide a .jpg, .png ro jpeg file smaller than 1MB";
                return this.PartialView("_MovieInputPartial", movie);
            }

            using (var ms = new MemoryStream())
            {
                movie.Image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string base64 = Convert.ToBase64String(fileBytes);
                movie.ImageString = string.Format("data:image/{0};base64,{1}", movie.Image.ContentType, base64);
            }

            this.ViewBag.Success = "Movie image updated";

            return this.PartialView("_MovieInputPartial", movie);
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
