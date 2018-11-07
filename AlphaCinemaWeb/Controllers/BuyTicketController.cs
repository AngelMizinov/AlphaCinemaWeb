using System.Linq;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Mvc;
using AlphaCinemaWeb.Models.ProjectionModels;
using AlphaCinemaWeb.Models.CityModels;
using System;
using AlphaCinemaWeb.Models.BindingModels.ProjectionModels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AlphaCinemaData.Models;
using Microsoft.AspNetCore.Authorization;

namespace AlphaCinemaWeb.Controllers
{
    public class BuyTicketController : Controller
    {
        private IProjectionService projectionsService;
        private ICityService cityService;
        private UserManager<User> userManager;

        public BuyTicketController(IProjectionService projectionsService, ICityService cityService, UserManager<User> userManager)
        {
            this.projectionsService = projectionsService;
            this.userManager = userManager;
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cities = await cityService.GetCities();
            var projections = projectionsService.GetTopProjections(3);

            var topProjections = new TopProjectionsViewModel(projections.Select(p => new ProjectionViewModel(p)));

            var model = new CityListViewModel(cities.Select(city => new CityViewModel(city)), topProjections);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Movie(int cityId)
        {
            string userId = "";
            const int pageSize = 3;
            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.userManager.GetUserAsync(this.User).Result.Id;
            }
            var projections = projectionsService.GetByTownId(cityId, userId).OrderBy(p => p.Movie.Name);
            this.ViewBag.CityName = await this.cityService.GetCityName(cityId);
            int maxPages = projections.Count() / pageSize;
            DayOfWeek day = DateTime.Now.DayOfWeek;
            var projectionsModel = new ProjectionListViewModel(1, maxPages, "title_desc", "hour", "title", cityId, 
                userId,  day, projections.Take(pageSize).Select(p => new ProjectionViewModel(p)));
            return View(projectionsModel);
        }

        [HttpPost]
        public IActionResult UpdateMovie(ProjectionListViewModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                model.UserId = "";
            }
            const int pageSize = 3;
            var projections = projectionsService.GetByTownId(model.CityId, model.UserId, model.Day);
            model.TitleSort = model.SortOrder == "title" ? "title_desc" : "title";//Тук нагласяме какво да се подаде от view-то следващия път като кликнем на сорт-линка
            //Винаги когато подадем нещо друго, различно от title следващото сортиране по име ще е в нарастващ ред
            model.HourSort = model.SortOrder == "hour" ? "hour_desc" : "hour";
            //Винаги когато подадем нещо друго, различно от hour следващото сортиране по час ще е в нарастващ ред
            int cityId = model.CityId;
            int currentPage = model.CurrentPage ?? 1;
            int maxPages = projections.Count() / pageSize;
            switch (model.SortOrder)
            {
                case "title_desc": projections = projections.OrderByDescending(p => p.Movie.Name); break;
                case "hour": projections = projections.OrderBy(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                case "hour_desc": projections = projections.OrderByDescending(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                default: projections = projections.OrderBy(p => p.Movie.Name); break;
            }

            projections = projections.Skip((currentPage - 1) * pageSize).Take(pageSize);

            var projectionsModel = new ProjectionListViewModel(currentPage, maxPages, model.TitleSort, 
                model.HourSort, model.SortOrder,cityId, model.UserId,
                model.Day, projections.Select(p => new ProjectionViewModel(p)));

            return PartialView("../BuyTicket/_ProjectionsPartial", projectionsModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Book(ProjectionBookModel projection)
        {
            //this.projectionsService.AddReservation(projection.UserName)

            //return RedirectToAction("UpdateMovie", new { cityId = projection.CityId, day = projection.Day });

            return NoContent();
        }

        public IActionResult Detail(ProjectionViewModel projection)
        {
            return View();
        }
    }
}