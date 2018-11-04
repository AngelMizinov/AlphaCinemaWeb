using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Areas.Administration.Models.CityModels;
using AlphaCinemaWeb.Models.CityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinemaWeb.Areas.Administration.Controllers
{
	public class CityController : Controller
	{
		private readonly ICityService cityService;

		public CityController(ICityService cityService)
		{
			this.cityService = cityService;
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
		public async Task<IActionResult> Add(CityViewModel cityViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}

			var city = await this.cityService.GetCity(cityViewModel.Name);
			if (city != null)
			{
				//ModelState.AddModelError("City", $"City with name {cityViewModel.Name} already exists!");
				//return View(cityViewModel);
				this.TempData["Error-Message"] = $"City with name {cityViewModel.Name} already exists!";
				return this.RedirectToAction("Add", "City");
			}
			try
			{
				await this.cityService.AddCity(cityViewModel.Name);
			}
			catch (Exception e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Add", "City");
			}

			this.TempData["Success-Message"] = $"You successfully added city with name {cityViewModel.Name}!";

			return this.RedirectToAction("Add", "City");
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
		public async Task<IActionResult> Remove(CityViewModel cityViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}

			var city = await this.cityService.GetCity(cityViewModel.Name);
			if (city == null)
			{
				//ModelState.AddModelError("City", $"City with name {cityViewModel.Name} doesn't exist!");
				//return View(cityViewModel);
				this.TempData["Error-Message"] = $"City with name {cityViewModel.Name} doesn't exist!";
				return this.RedirectToAction("Remove", "City");	
			}

			try
			{
				await this.cityService.DeleteCity(cityViewModel.Name);
			}
			catch (Exception e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Remove", "City");
			}

			this.TempData["Success-Message"] = $"You successfully deleted city with name {cityViewModel.Name}!";

			return this.RedirectToAction("Remove", "City");
		}

		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public ActionResult Update()
		{
			return this.View();
		}

		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(CityUpdateViewModel cityViewModel)
		{
			if (!this.ModelState.IsValid)
			{
				return View();
			}
			var city = await this.cityService.GetCity(cityViewModel.OldName);
			if (city == null)
			{
				//ModelState.AddModelError("City", $"City with name {cityViewModel.Name} doesn't exist!");
				//return View(cityViewModel);
				this.TempData["Error-Message"] = $"City with name {cityViewModel.OldName} doesn't exist!";
				return this.RedirectToAction("Update", "City");
			}

			try
			{
				await this.cityService.UpdateName(cityViewModel.OldName, cityViewModel.Name);
			}
			catch (Exception e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "City");
			}

			this.TempData["Success-Message"] = $"You successfully changed {cityViewModel.OldName} to {cityViewModel.Name}!";

			return this.RedirectToAction("Update", "City");
		}
	}
}