using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
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
        private const string defaultImage = "~/images/defaultMovie.jpg";

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

            this.ViewBag.DefaultImage = defaultImage;

            var model = new MovieUpdateListViewModel(movies.Select(movie => new MovieUpdateViewModel(movie, defaultImage)));

            return this.View(model);
        }

        [HttpPost]
        public IActionResult GetMovie(MovieUpdateListViewModel movie)
        {
            this.TempData["Success-Message"] = null;

            this.TempData["Error-Message"] = null;

            this.ViewBag.MovieName = movie.Name;

            this.ViewBag.DefaultImage = defaultImage;

            return PartialView("_MovieInputPartial", movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieUpdateListViewModel viewModel)
        {
            this.ViewBag.DefaultImage = defaultImage;

            if (!this.ModelState.IsValid)
            {
                this.TempData["Error-Message"] = $"You have entered wrong Movie parameters";
                return this.RedirectToAction("Update");
            }

            try
            {
                await this.movieService.UpdateMovie(viewModel.Id, viewModel.Name, viewModel.Description, viewModel.ReleaseYear, viewModel.Duration, viewModel.ImageString);
            }
            catch (Exception ex)
            {
                this.TempData["Error-Message"] = "Movie does not exist in database";
                return this.RedirectToAction("Update");
            }

            this.TempData["Success-Message"] = $"You successfully updated the movie information!";
            return this.RedirectToAction("Update");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Avatar([FromForm] MovieUpdateListViewModel movie)
        {
            this.ViewBag.DefaultImage = defaultImage;

            if (movie.Image == null && movie.ImageString == null)
            {
                this.TempData["Error-Message"] = "Error! Please provide an image";
                return this.PartialView("_MovieInputPartial", movie);
            }

            if (movie.Image != null)
            {//Ако сме подали нов файл
                if (!this.IsValidImage(movie.Image))
                {
                    this.TempData["Error-Message"] = "Error! Please provide a.jpg, .png ro jpeg file smaller than 1MB";
                    return this.PartialView("_MovieInputPartial", movie);
                }

                using (var ms = new MemoryStream())
                {
                    movie.Image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string base64 = Convert.ToBase64String(fileBytes);
                    movie.ImageString = string.Format("data:image/{0};base64,{1}", movie.Image.ContentType, base64);
                }

                this.TempData["Success-Message"] = "Success! Movie image updated";
            }
            else
            {
                this.TempData["Error-Message"] = "Warning! Movie image restored";
            }

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
