using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Areas.Administration.Models.MovieModels;
using AlphaCinemaWeb.Models.MovieViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinemaWeb.Areas.Administration.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }
        
        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MovieViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            var movie = await this.movieService.GetMovie(viewModel.Name);

            if(movie != null)
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


        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult Remove()
        {
            return this.View();
        }

        [Area("Administration")]
        [Authorize(Roles = "Administrator")]
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


    }
}
