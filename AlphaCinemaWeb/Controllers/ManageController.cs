using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AlphaCinema.Models;
using AlphaCinema.Models.ManageViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Models.MovieViewModels;
using AlphaCinemaWeb.Models.WatchedMovieModels;
using Microsoft.Extensions.Caching.Memory;
using AlphaCinemaData.Models.Associative;
using System.Collections.Generic;
namespace AlphaCinema.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class ManageController : Controller
	{
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private readonly IUserService userService;
		private readonly IWatchedMoviesService watchedMoviesService;
		private readonly IMemoryCache cache;

		//private readonly IUsersService usersService;

		public ManageController(
		  UserManager<User> userManager,
		  SignInManager<User> signInManager,
		  IUserService userService,
		  IWatchedMoviesService watchedMoviesService,
		  IMemoryCache cache)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.userService = userService;
			this.watchedMoviesService = watchedMoviesService;
			this.cache = cache;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[HttpGet]
		public async Task<IActionResult> Index(string userId, string sortBy)
		{
			ViewData["MovieTitleSortParam"] = String.IsNullOrEmpty(sortBy) ? "movie_title" : "";

			var user = await GetUserCached(userId);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{userId}'.");
			}
			var watchedMovies = await GetWatchedMoviesByUserIdCached(userId);

			var watchedMovieViewModels = watchedMovies.Select(wm => new WatchedMovieViewModel()
			{
				CityName = wm.Projection.City.Name,
				Hours = wm.Projection.OpenHour.Hours,
				Minutes = wm.Projection.OpenHour.Minutes,
				WatchedOn = wm.Date,
				MovieName = wm.Projection.Movie.Name
			}).ToList();

			switch (sortBy)
			{
				case "movie_title":
					watchedMovieViewModels = watchedMovieViewModels.OrderByDescending(m => m.MovieName).ToList();
					break;
			}


			var model = new IndexViewModel
			{
				UserId = user.Id,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Age = user.Age,
				WatchedMovieViewModels = watchedMovieViewModels,
				Username = user.UserName,
				CreatedOn = user.CreatedOn,
				ModifiedOn = user.ModifiedOn ?? user.CreatedOn,
				ImageUrl = user.Image,
				StatusMessage = StatusMessage
			};
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(IndexViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await this.userManager.GetUserAsync(User);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
			}
			// Ако искаме да ползваме това трябва да extend-нем user-manager, за да може да добавим нашата логика - first name, last name, age и други..


			StatusMessage = "Your profile has been updated";
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> ChangePassword()
		{
			var user = await this.userManager.GetUserAsync(User);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
			}

			var model = new ChangePasswordViewModel { StatusMessage = StatusMessage };
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var user = await this.userManager.GetUserAsync(User);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
			}

			var changePasswordResult = await this.userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
			if (!changePasswordResult.Succeeded)
			{
				AddErrors(changePasswordResult);
				return View(model);
			}

			await this.userService.Modify(user.Id);

			await this.signInManager.SignInAsync(user, isPersistent: false);
			StatusMessage = "Your password has been changed.";

			return RedirectToAction(nameof(ChangePassword));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Avatar(IFormFile avatarImage)
		{
			if (avatarImage == null)
			{
				this.StatusMessage = "Error: Please provide an image";
				return this.RedirectToAction(nameof(Index));
			}

			if (!this.IsValidImage(avatarImage))
			{
				this.StatusMessage = "Error: Please provide a .jpg or .png file smaller than 1MB";
				return this.RedirectToAction(nameof(Index));
			}
			// TODO : FIX
			//await this.usersService.SaveAvatarImageAsync(
			//	this.GetUploadsRoot(),
			//	avatarImage.FileName,
			//	avatarImage.OpenReadStream(),
			//	this.User.GetId()
			//);

			this.StatusMessage = "Profile image updated";

			return this.RedirectToAction(nameof(Index));
		}

		private string GetUploadsRoot()
		{
			var environment = this.HttpContext.RequestServices
				.GetService(typeof(IHostingEnvironment)) as IHostingEnvironment;

			return Path.Combine(environment.WebRootPath, "images", "avatars");
		}

		private bool IsValidImage(IFormFile image)
		{
			string type = image.ContentType;
			if (type != "image/png" && type != "image/jpg" && type != "image/jpeg")
			{
				return false;
			}

			return image.Length <= 1024 * 1024;
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				this.ModelState.AddModelError(string.Empty, error.Description);
			}
		}

		private async Task<User> GetUserCached(string userId)
		{
			// Ако има кеш с такъв ключ ми върни него, ако няма ми създай нов.
			return await this.cache.GetOrCreateAsync("User", entry =>
			{
				entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(40);
				return this.userService.GetUser(userId);
			});
		}

		private async Task<IEnumerable<WatchedMovie>> GetWatchedMoviesByUserIdCached(string userId)
		{
			//await watchedMoviesService.GetWatchedMoviesByUserId(user.Id);
			// Ако има кеш с такъв ключ ми върни него, ако няма ми създай нов.
			return await this.cache.GetOrCreateAsync("WatchevMovies", entry =>
			{
				entry.AbsoluteExpiration = DateTime.UtcNow.AddSeconds(40);
				return this.watchedMoviesService.GetWatchedMoviesByUserId(userId);
			});
		}
	}
}
