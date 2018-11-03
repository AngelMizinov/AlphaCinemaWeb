using System.Linq;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using AlphaCinemaWeb.Models.ProjectionModels;
using AlphaCinemaWeb.Models.CityModels;
using System;

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

            var model = new CityListViewModel(cities.Select(city => new CityViewModel(city)));

            return View(model);
        }

        public IActionResult Movie(int cityId, DayOfWeek day)
        {
            var projections = projectionsService.GetByTownId(cityId);
            projections = projections.Where(projection => projection.Day == (int)day);

            var model = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)), cityId);

            return View(model);
        }

        public IActionResult UpdateMovie(int cityId, DayOfWeek day)
        {
            var projections = projectionsService.GetByTownId(cityId);
            projections = projections.Where(projection => projection.Day == (int)day);

            var model = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)), cityId);

            return PartialView("../BuyTicket/Projections",model);
        }

        public IActionResult Detail(ProjectionViewModel projection)
        {
            return View();
        }
    }
}