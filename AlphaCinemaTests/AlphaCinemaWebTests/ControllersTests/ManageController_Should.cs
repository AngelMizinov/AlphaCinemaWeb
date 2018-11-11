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

				case 3:
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
					// user dasdas null
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
