using System.Linq;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using AlphaCinemaWeb.Models.ProjectionModels;
using AlphaCinemaWeb.Models.CityModels;
using System;
using AlphaCinemaWeb.Models.BindingModels.ProjectionModels;

namespace AlphaCinemaWeb.Controllers
{
    public class BuyTicketController : Controller
    {
        private IProjectionService projectionsService;
        private ICityService cityService;

        public BuyTicketController(IProjectionService projectionsService, ICityService cityService)
        {
            this.projectionsService = projectionsService;
            this.cityService = cityService;
        }

        public IActionResult Index()
        {
            var cities = cityService.GetCities();
            var projections = projectionsService.GetTopProjections(3);

            var topProjections = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)), 0, DateTime.Now.DayOfWeek);

            var model = new CityListViewModel(cities.Select(city => new CityViewModel(city)),topProjections);

            return View(model);
        }

        public IActionResult Movie(int cityId, DayOfWeek day)
        {
            var projections = projectionsService.GetByTownId(cityId);
            projections = projections.Where(projection => projection.Day == (int)day);

            var model = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)), cityId, day);

            return View(model);
        }

        public IActionResult UpdateMovie(int cityId, DayOfWeek day)
        {
            var projections = projectionsService.GetByTownId(cityId);
            projections = projections.Where(projection => projection.Day == (int)day);

            var model = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)), cityId, day);

            return PartialView("../BuyTicket/Projections", model);
        }

        [HttpPost]
        public IActionResult Book(ProjectionBookModel projection)
        {


            return RedirectToAction("UpdateMovie", new { cityId = projection.CityId, day = projection.Day});
        }

        public IActionResult Detail(ProjectionViewModel projection)
        {
            return View();
        }
    }
}