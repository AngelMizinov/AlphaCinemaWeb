using System.Linq;
using System.Threading.Tasks;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Areas.Administration.Models.CityModels;
using AlphaCinemaWeb.Exceptions;
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
				this.TempData["Error-Message"] = $"City with name {cityViewModel.Name} already exists!";
				return this.RedirectToAction("Add", "City");
			}
			try
			{
				await this.cityService.AddCity(cityViewModel.Name);
			}
			catch (InvalidClientInputException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Add", "City");
			}
			catch (EntityAlreadyExistsException e)
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
				this.TempData["Error-Message"] = $"City with name {cityViewModel.Name} doesn't exist!";
				return this.RedirectToAction("Remove", "City");	
			}

			try
			{
				await this.cityService.DeleteCity(cityViewModel.Name);
			}
			catch (EntityDoesntExistException e)
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
        public IActionResult SetId(int cityId,string cityName)
        {
            this.ViewBag.CityId = cityId;
            this.ViewBag.CityName = cityName;

            City city = new City()
            {
                Id = cityId,
                Name = cityName
            };
            
            return PartialView("_CityInputPartial", new CityUpdateViewModel(city));
        }
        
        [Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpGet]
		public async Task<IActionResult> Update()
		{
            var cities = await this.cityService.GetCities();

            var models = new CityUpdateListViewModel(cities.Select(city => new CityUpdateViewModel(city)));
            
            return this.View(models);
		}


		[Area("Administration")]
		[Authorize(Roles = "Administrator")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(CityUpdateViewModel cityViewModel)
		{
			if (!this.ModelState.IsValid)
			{
                return RedirectToAction("Update");
            }
			var city = await this.cityService.GetCity(cityViewModel.Id);
			if (city == null)
			{
				this.TempData["Error-Message"] = $"City doesn't exist!";
				return this.RedirectToAction("Update", "City");
			}

			try
			{
				await this.cityService.UpdateName(cityViewModel.Id, cityViewModel.Name);
			}
			catch (EntityAlreadyExistsException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "City");
			}
			catch (InvalidClientInputException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "City");
			}
			catch (EntityDoesntExistException e)
			{
				this.TempData["Error-Message"] = e.Message;
				return this.RedirectToAction("Update", "City");
			}

			this.TempData["Success-Message"] = $"You successfully changed the city with new name {cityViewModel.Name}!";

			return this.RedirectToAction("Update", "City");
		}
	}
}