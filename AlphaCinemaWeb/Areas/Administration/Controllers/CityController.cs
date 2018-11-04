using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaServices.Contracts;
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
		public async Task<IActionResult> Add(CityViewModel model)
		{
			if (this.ModelState.IsValid)
			{
				var city = await this.cityService.GetCity(model.Name);
				model = new CityViewModel(city);
				await this.cityService.AddCity(model.Name);

				this.TempData["Success-Message"] = $"You successfully added city with name {model.Name}!";

				return this.RedirectToAction("Index", "Home");
			}
			return View(model);
		}
	}
}