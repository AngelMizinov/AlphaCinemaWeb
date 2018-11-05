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

        [HttpGet]
        public IActionResult Index()
        {
            var cities = cityService.GetCities();
            var projections = projectionsService.GetTopProjections(3);

            var topProjections = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)));

            var model = new CityListViewModel(cities.Select(city => new CityViewModel(city)),topProjections);

            return View(model);
        }

        [HttpGet]
        public IActionResult Movie(int cityId)
        {
            var projections = projectionsService.GetByTownId(cityId).OrderBy(p => p.Movie.Name);
            const int pageSize = 3;
            var projectionsModel = new ProjectionListViewModel(projections.Take(pageSize).Select(p => new ProjectionViewModel(p)));
            ViewBag.TitleSort = "title_desc";
            ViewBag.HourSort = "hour";
            ViewBag.CityId = cityId;
            ViewBag.CurrentPage = 1;
            ViewBag.DayOfWeek = DateTime.Now.DayOfWeek;
            ViewBag.MaxPages = projections.Count() / pageSize;
            return View(projectionsModel);
        }

        [HttpGet]
		public IActionResult UpdateMovie(int cityId, DayOfWeek day, string sortOrder = "title", int page = 1)
		{
            const int pageSize = 3;
            var projections = projectionsService.GetByTownId(cityId, day);
            ViewBag.TitleSort = sortOrder == "title" ? "title_desc" : "title";//Тук нагласяме какво да се подаде от view-то следващия път като кликнем на сорт-линка
            //Винаги когато подадем нещо друго, различно от title следващото сортиране по име ще е в нарастващ ред
            ViewBag.HourSort = sortOrder == "hour" ? "hour_desc" : "hour";
            //Винаги когато подадем нещо друго, различно от hour следващото сортиране по час ще е в нарастващ ред
            ViewBag.CityId = cityId;
            ViewBag.DayOfWeek = day;
            ViewBag.CurrentPage = page;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaxPages = projections.Count() / pageSize;
            //Филтрираме за деня, който сме избрали
            switch (sortOrder)
            {
                case "title_desc": projections = projections.OrderByDescending(p => p.Movie.Name); break;
                case "hour": projections = projections.OrderBy(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                case "hour_desc": projections = projections.OrderByDescending(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                default: projections = projections.OrderBy(p => p.Movie.Name); break;
            }

            projections = projections.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new ProjectionListViewModel(projections.Select(p => new ProjectionViewModel(p)));

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