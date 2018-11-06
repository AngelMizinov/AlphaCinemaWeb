using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Areas.Administration.Models.MovieModels;
using AlphaCinemaWeb.Exceptions;
using AlphaCinemaWeb.Models.MovieViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinemaWeb.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "Administrator")]
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
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
        public ActionResult Update()
        {
            return this.View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MovieUpdateViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var movie = await this.movieService.GetMovie(viewModel.OldName);

            if(movie == null)
            {
                this.TempData["Error-Message"] = $"Movie with name [{viewModel.OldName}] doesn't exist!";
                return this.View();
            }

            try
            {
                await this.movieService.UpdateName(viewModel.OldName, viewModel.Name);
            }
            catch (EntityAlreadyExistsException e)
            {
                this.TempData["Error-Message"] = e.Message;
                return this.View();
            }
            catch (InvalidClientInputException e)
            {
                this.TempData["Error-Message"] = e.Message;
                return this.View();
            }
            catch (EntityDoesntExistException e)
            {
                this.TempData["Error-Message"] = e.Message;
                return this.View();
            }

            this.TempData["Success-Message"] = $"You successfully changed movie name {viewModel.OldName} to {viewModel.Name}!";
            return this.View();
        }
    }
}
