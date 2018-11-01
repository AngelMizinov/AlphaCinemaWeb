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

namespace AlphaCinema.Controllers
{
	[Authorize]
	[Route("[controller]/[action]")]
	public class ManageController : Controller
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public ManageController(
		  UserManager<ApplicationUser> userManager,
		  SignInManager<ApplicationUser> signInManager)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var user = await this.userManager.GetUserAsync(User);
			if (user == null)
			{
				throw new ApplicationException($"Unable to load user with ID '{this.userManager.GetUserId(User)}'.");
			}

			var model = new IndexViewModel
			{
				// firstname, lastname, age, 
				Username = user.UserName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				IsEmailConfirmed = user.EmailConfirmed,
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

			var email = user.Email;
			if (model.Email != email)
			{
				var setEmailResult = await this.userManager.SetEmailAsync(user, model.Email);
				if (!setEmailResult.Succeeded)
				{
					throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
				}
			}

			var phoneNumber = user.PhoneNumber;
			if (model.PhoneNumber != phoneNumber)
			{
				var setPhoneResult = await this.userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
				if (!setPhoneResult.Succeeded)
				{
					throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
				}
			}

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
	}
}
