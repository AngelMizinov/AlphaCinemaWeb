using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Areas.Administration.Models.GenreViewModels;
using AlphaCinemaWeb.Exceptions;
using AlphaCinemaWeb.Models.GenreViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlphaCinemaWeb.Areas.Administration.Controllers
{
	public class GenreController : Controller
	{
		// GET: /<controller>/
		private readonly IGenreService genreService;

		public GenreController(IGenreService genreService)
		{
			this.genreService = genreService;
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
			return this.View();
		}

		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(GenreViewModel genreViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}

			var city = await this.genreService.GetGenre(genreViewModel.Name);
			if (city != null)
			{
				this.TempData["Error-Message"] = $"Genre with name {genreViewModel.Name} already exists!";
				return this.RedirectToAction("Add", "Genre");
			}
			try
			{
				await this.genreService.AddGenre(genreViewModel.Name);
			}
			catch (InvalidClientInputException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Add", "Genre");
			}
			catch (EntityAlreadyExistsException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Add", "Genre");
			}

			this.TempData["Success-Message"] = $"You successfully added genre with name {genreViewModel.Name}!";

			return this.RedirectToAction("Add", "Genre");
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
		public async Task<IActionResult> Remove(GenreViewModel genreViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}

			var city = await this.genreService.GetGenre(genreViewModel.Name);
			if (city == null)
			{
				this.TempData["Error-Message"] = $"Genre with name {genreViewModel.Name} doesn't exist!";
				return this.RedirectToAction("Remove", "Genre");
			}

			try
			{
				await this.genreService.DeleteGenre(genreViewModel.Name);
			}
			catch (EntityDoesntExistException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Remove", "Genre");
			}

			this.TempData["Success-Message"] = $"You successfully deleted genre with name {genreViewModel.Name}!";

			return this.RedirectToAction("Remove", "Genre");
		}

		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		public IActionResult SetId(GenreUpdateListViewModel viewModel)
		{
            var model = new GenreUpdateListViewModel()
            {
                Id = viewModel.Id,
                Name = viewModel.Name
            };

			return PartialView("_GenreInputPartial", model);
		}

		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public async Task<IActionResult> Update()
		{
			var genres = await this.genreService.GetGenres();

			var models = new GenreUpdateListViewModel(genres.Select(genre => new GenreUpdateViewModel(genre)));

			return this.View(models);
		}


		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(GenreUpdateViewModel genreViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return RedirectToAction("Update");
			}
			var genre = await this.genreService.GetGenre(genreViewModel.Id);
			if (genre == null)
			{
				this.TempData["Error-Message"] = $"Genre doesn't exist!";
				return this.RedirectToAction("Update", "Genre");
			}

			try
			{
				await this.genreService.UpdateName(genre.Id, genreViewModel.Name);
			}
			catch (EntityAlreadyExistsException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "Genre");
			}
			catch (InvalidClientInputException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "Genre");
			}
			catch (EntityDoesntExistException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "Genre");
			}

			this.TempData["Success-Message"] = $"You successfully changed the genre with new name {genreViewModel.Name}!";

			return this.RedirectToAction("Update", "Genre");
		}
	}
}
