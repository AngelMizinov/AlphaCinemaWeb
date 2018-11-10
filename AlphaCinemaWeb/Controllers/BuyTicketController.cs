using System;
using System.Linq;
using AlphaCinemaData.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Identity;
using AlphaCinemaWeb.Models.CityModels;
using Microsoft.AspNetCore.Authorization;
using AlphaCinemaWeb.Models.ProjectionModels;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Exceptions;

namespace AlphaCinemaWeb.Controllers
{
    public class BuyTicketController : Controller
    {
        private readonly IProjectionService projectionsService;
        private readonly ICityService cityService;
        private readonly UserManager<User> userManager;
        private readonly IMemoryCache cache;
        private const string defaultImage = "~/images/defaultMovie.jpg";

        public BuyTicketController(IProjectionService projectionsService, ICityService cityService, UserManager<User> userManager,
        IMemoryCache cache)
        {
            this.projectionsService = projectionsService;
            this.userManager = userManager;
            this.cache = cache;
            this.cityService = cityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            this.ViewData["ReturnUrl"] = "/BuyTicket/Index";
            var cities = await GetCitiesCached();

            var projections = await GetTopProjectionsCached(3);

            this.ViewBag.DefaultImage = defaultImage;

            var topProjections = new TopProjectionsViewModel(projections.Select(p => new ProjectionViewModel(p, defaultImage)));

            var model = new CityListViewModel(cities.Select(city => new CityViewModel(city)), topProjections);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Movie(int cityId)
        {
            this.ViewData["ReturnUrl"] = "/BuyTicket/Movie/?cityId=" + cityId;
            //Този Url е за User-a Използва се при вписване и отписване
            string userId = "";
            this.ViewBag.DefaultImage = defaultImage;
            const int pageSize = 3;
            int maxPages = 0;
            DayOfWeek day = DateTime.Now.DayOfWeek;

            if (this.User.Identity.IsAuthenticated)
            {
                userId = this.userManager.GetUserAsync(this.User).Result.Id;
            }

            //try
            //{
            this.ViewBag.CityName = await this.cityService.GetCityName(cityId);

            var projections = await projectionsService.GetByTownId(cityId, userId);

            projections = projections.OrderBy(p => p.Movie.Name);

            maxPages = (int)Math.Ceiling(projections.Count() / (decimal)pageSize);

            var projectionsModel = new ProjectionListViewModel(1, maxPages, "title_desc", "hour", "title", cityId,
                userId, day, projections.Take(pageSize).Select(p => new ProjectionViewModel(p, defaultImage)));
            return View(projectionsModel);
            //}
            //catch (Exception ex)
            //{
            //    this.TempData["Error-Message"] = ex.Message;
            //    return this.RedirectToAction("Index");
            //}
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMovie(ProjectionListViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidClientInputException("Something went wrong when received UpdateMovie parameters");
                //this.TempData["Error-Message"] = "Sorry! We cannot execute your request";
                //return this.RedirectToAction("Index");
            }

            this.ViewBag.DefaultImage = defaultImage;
            if (!this.User.Identity.IsAuthenticated)
            {
                model.UserId = "";
            }
            const int pageSize = 3;
            //try
            //{
            var projections = await projectionsService.GetByTownId(model.CityId, model.UserId, model.Day);
            model.TitleSort = model.SortOrder == "title" ? "title_desc" : "title";//Тук нагласяме какво да се подаде от view-то следващия път като кликнем на сорт-линка
                                                                                  //Винаги когато подадем нещо друго, различно от title следващото сортиране по име ще е в нарастващ ред
            model.HourSort = model.SortOrder == "hour" ? "hour_desc" : "hour";
            //Винаги когато подадем нещо друго, различно от hour следващото сортиране по час ще е в нарастващ ред
            int cityId = model.CityId;
            int currentPage = model.CurrentPage ?? 1;
            int maxPages = (int)Math.Ceiling(projections.Count() / (decimal)pageSize);
            switch (model.SortOrder)
            {
                case "title_desc": projections = projections.OrderByDescending(p => p.Movie.Name); break;
                case "hour": projections = projections.OrderBy(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                case "hour_desc": projections = projections.OrderByDescending(p => p.OpenHour.Hours + p.OpenHour.Minutes / 60.0); break;
                default: projections = projections.OrderBy(p => p.Movie.Name); break;
            }

            projections = projections.Skip((currentPage - 1) * pageSize).Take(pageSize);

            var projectionsModel = new ProjectionListViewModel(currentPage, maxPages, model.TitleSort,
                model.HourSort, model.SortOrder, cityId, model.UserId,
                model.Day, projections.Select(p => new ProjectionViewModel(p, defaultImage)));

            return PartialView("../BuyTicket/_ProjectionsPartial", projectionsModel);
            //}
            //catch (Exception ex)
            //{
            //    this.TempData["Error-Message"] = ex.Message;
            //    return this.RedirectToAction("Index");
            //}
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(ProjectionBookModel projection)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidClientInputException("Something went wrong when received Booking parameters");
                //this.TempData["Error-Message"] = "Sorry! We cannot book your reservation";
                //return this.RedirectToAction("Index");
            }
            //try
            //{
            await this.projectionsService.AddReservation(projection.UserId, projection.ProjectionId);
            this.TempData["Success-Message"] = "You booked a ticket!";
            return RedirectToAction("Movie", new { cityId = projection.CityId });
            //}
            //catch (Exception ex)
            //{
            //    this.TempData["Error-Message"] = ex.Message;
            //    return this.RedirectToAction("Index");
            //}
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(ProjectionBookModel projection)
        {
            if (!this.ModelState.IsValid)
            {
                throw new InvalidClientInputException("Something went wrong when received Decline parameters");
                //this.TempData["Error-Message"] = "Sorry! We cannot decline your reservation";
                //return this.RedirectToAction("Index");
            }
            //try
            //{
            await this.projectionsService.DeclineReservation(projection.UserId, projection.ProjectionId);
            this.TempData["Warning-Message"] = "You declined your reservation!";
            return RedirectToAction("Movie", new { cityId = projection.CityId });
            //}
            //catch (Exception ex)
            //{
            //    this.TempData["Error-Message"] = ex.Message;
            //    return this.RedirectToAction("Index");
            //}
        }

        private async Task<IEnumerable<City>> GetCitiesCached()
        {
            //await watchedMoviesService.GetWatchedMoviesByUserId(user.Id);
            // Ако има кеш с такъв ключ ми върни него, ако няма ми създай нов.
            return await this.cache.GetOrCreateAsync("Cities", entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(40);
                return this.cityService.GetCities();
            });
        }

        private async Task<IEnumerable<Projection>> GetTopProjectionsCached(int count)
        {
            //await watchedMoviesService.GetWatchedMoviesByUserId(user.Id);
            // Ако има кеш с такъв ключ ми върни него, ако няма ми създай нов.
            return await this.cache.GetOrCreateAsync("TopProjections", entry =>
            {
                entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(40);
                return this.projectionsService.GetTopProjections(count);
            });
        }
    }
}