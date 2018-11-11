using AlphaCinema.Controllers;
using AlphaCinema.Models.ManageViewModels;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaWebTests.ControllersTests
{
	[TestClass]
	public class ManageController_Should
	{
		private Mock<IUserService> userServiceMock = new Mock<IUserService>();
		private Mock<IWatchedMoviesService> watchedMoviesServiceMock = new Mock<IWatchedMoviesService>();
		private readonly UserManager<User> userManager;
		private readonly SignInManager<User> signInManager;
		private User user;
		private IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
		private ManageController controller;

		[TestMethod]
		public async Task IndexGetAction_ThrowApplicationException_WhenUserIsNull()
		{
			// Arrange
			string userId = "userId";
			string sortBy = "sortOrder";
			// Act
			var controller = SetupController(1);

			// Assert
			await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
							controller.Index(userId, sortBy));
		}

		[TestMethod]
		public async Task IndexAction_ReturnsCorrectViewModel()
		{
			// Arrange
			string userId = "userId";
			string sortBy = "sortBy";
			//  Act
			var controller = SetupController(2);

			var result = await controller.Index(userId, sortBy) as ViewResult;

			// Assert
			Assert.IsInstanceOfType(result.Model, typeof(IndexViewModel));
		}

		[TestMethod]
		public async Task IndexAction_ReturnsViewResult()
		{
			// Arrange
			string userId = "userId";
			string sortBy = "sortBy";
			//  Act
			var controller = SetupController(2);

			var result = await controller.Index(userId, sortBy);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public async Task IndexAction_CallCorrectServiceMethod()
		{
			// Arrange
			string userId = "userId";
			string sortBy = "sortBy";
			//  Act
			var controller = SetupController(2);

			var result = await controller.Index(userId, sortBy) as ViewResult;

			// Assert

			userServiceMock.Verify(u => u.GetUser(It.IsAny<string>()), Times.Once);
			watchedMoviesServiceMock.Verify(wm => wm.GetWatchedMoviesByUserId(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public async Task ChangePasswordGetAction_ThrowApplicationException_WhenUserIsNull()
		{
			// Arrange
			string userId = "userId";
			//  Act
			var controller = SetupController(1);

			// Assert
			await Assert.ThrowsExceptionAsync<ApplicationException>(() =>
							controller.ChangePassword(userId));
		}

		[TestMethod]
		public async Task ChangePasswordGetAction_CallCorrectServiceMethod()
		{
			// Arrange
			string userId = "userId";
			//  Act
			var controller = SetupController(2);

			var result = await controller.ChangePassword(userId) as ViewResult;

			// Assert

			userServiceMock.Verify(u => u.GetUser(It.IsAny<string>()), Times.Once);
		}

		[TestMethod]
		public async Task ChangePasswordGetAction_ReturnsCorrectViewModel()
		{
			// Arrange
			string userId = "userId";
			//  Act
			var controller = SetupController(2);

			var result = await controller.ChangePassword(userId) as ViewResult;

			// Assert
			Assert.IsInstanceOfType(result.Model, typeof(ChangePasswordViewModel));
		}

		[TestMethod]
		public async Task ChangePasswordGetAction_ReturnsViewResult()
		{
			// Arrange
			string userId = "userId";
			//  Act
			var controller = SetupController(2);

			var result = await controller.ChangePassword(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public async Task ChangePasswordPostAction_InvalidModelState_RedisplaysView()
		{
			// Arrange
			controller = this.SetupController(1);
			controller.ModelState.AddModelError("error", "error");
			var viewModel = new ChangePasswordViewModel();

			// Act
			var result = await controller.ChangePassword(viewModel);

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
			var viewResult =  (ViewResult)result;
			Assert.IsInstanceOfType(viewResult.Model, typeof(ChangePasswordViewModel));
			Assert.IsNull(viewResult.ViewName); // should not return any other view
		}

		[TestMethod]
		public async Task ChangePasswordPostAction_CallCorrectServicesMethods()
		{
			// Arrange
			string userId = "userId";
			var viewModel = new ChangePasswordViewModel()
			{
				UserId = userId,
				ConfirmPassword = "confirm",
				NewPassword = "confirm",
				OldPassword = "djoni",
				StatusMessage = "bravo"
			};
			//  Act
			var controller = SetupController(3);

			var result = await controller.ChangePassword(viewModel);

			// Assert

			userServiceMock.Verify(u => u.GetUserFromManager(It.IsAny<ClaimsPrincipal>()), Times.Once);

			userServiceMock.Verify(u => u.ChangePassword
			(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);

			userServiceMock.Verify(u => u.Modify(It.IsAny<string>()), Times.Once);

		}

		[TestMethod]
		public async Task ChangePasswordPostAction_ReturnsViewResult_WhenChangePasswordResultFailed()
		{
			// Arrange
			string userId = "userId";
			var viewModel = new ChangePasswordViewModel()
			{
				UserId = userId,
				ConfirmPassword = "confirm",
				NewPassword = "confirm",
				OldPassword = "djoni",
				StatusMessage = "bravo"
			};
			//  Act
			var controller = SetupController(4);

			var result = await controller.ChangePassword(viewModel);
			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public async Task ChangePasswordPostAction_RedirectsToManageIndexAction_WhenSuccessfull()
		{
			// Arrange
			string userId = "userId";
			var viewModel = new ChangePasswordViewModel()
			{
				UserId = userId,
				ConfirmPassword = "confirm",
				NewPassword = "confirm",
				OldPassword = "djoni",
				StatusMessage = "bravo"
			};
			//  Act
			var controller = SetupController(3);

			var result = await controller.ChangePassword(viewModel);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("ChangePassword", redirectResult.ActionName);
			Assert.AreEqual("Manage", redirectResult.ControllerName);
			Assert.IsNotNull(redirectResult.RouteValues);
		}

		private Mock<IUserService> SetupMUserServiceMock(int test)
		{
			switch (test)
			{
				case 1:
					userServiceMock
						.Setup(x => x.GetUser(It.IsAny<string>()))
						.ReturnsAsync(Task.FromResult<User>(null).Result);
					break;

				case 2:
					user = new User() { FirstName = "Test me" };
					userServiceMock
						.Setup(m => m.GetUser(It.IsAny<string>()))
						.ReturnsAsync(user);
					break;
				case 3:
					user = new User() { FirstName = "test mee" };

					userServiceMock
						.Setup(x => x.GetUserFromManager(It.IsAny<ClaimsPrincipal>()))
						.ReturnsAsync(user);

					userServiceMock
						.Setup(x => x.ChangePassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
						.ReturnsAsync(IdentityResult.Success);

					userServiceMock
						.Setup(x => x.Modify(It.IsAny<string>()))
						.Returns(Task.CompletedTask);
					break;
				case 4:
					user = new User() { FirstName = "test mee" };

					userServiceMock
						.Setup(x => x.GetUserFromManager(It.IsAny<ClaimsPrincipal>()))
						.ReturnsAsync(user);

					userServiceMock
						.Setup(x => x.ChangePassword(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>()))
						.ReturnsAsync(IdentityResult.Failed());

					userServiceMock
						.Setup(x => x.Modify(It.IsAny<string>()))
						.Returns(Task.CompletedTask);
					break;
			}
			return userServiceMock;
		}

		private Mock<IWatchedMoviesService> SetupWatchedServiceMock(int test)
		{
			switch (test)
			{
				case 1:
					break;

				case 2:
					watchedMoviesServiceMock
						.Setup(x => x.GetWatchedMoviesByUserId(It.IsAny<string>()))
						.ReturnsAsync(new List<WatchedMovie>()
						{
							new WatchedMovie()
							{
								Date = DateTime.Now,
								Projection = new Projection()
								{
									Movie = new Movie() {Name = "MovieNameTest" },
									OpenHour = new OpenHour() {Hours = 5, Minutes = 2 },
									City = new City() {Name = "CityNameTest"}
								},
								//User = new User()
							}
						});
					break;
			}
			return watchedMoviesServiceMock;
		}

		private ManageController SetupController(int test)
		{
			switch (test)
			{
				case 1:
					// User == null
					userServiceMock = SetupMUserServiceMock(test);
					break;
				case 2:
					// user != null && watched movies returns valid list
					userServiceMock = SetupMUserServiceMock(test);
					watchedMoviesServiceMock = SetupWatchedServiceMock(2);
					break;
				case 3:
					// ChangePassword Post
					userServiceMock = SetupMUserServiceMock(test);
					break;
				case 4:
					userServiceMock = SetupMUserServiceMock(test);
					break;
			}

			controller = new ManageController(userManager, signInManager,
				userServiceMock.Object, watchedMoviesServiceMock.Object, cache)
			{
				ControllerContext = new ControllerContext()
				{
					HttpContext = new DefaultHttpContext()
					{
						User = new ClaimsPrincipal()
					}
				},
				TempData = new Mock<ITempDataDictionary>().Object
			};

			return controller;
		}


	}
}
