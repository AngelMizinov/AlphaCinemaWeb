using AlphaCinema.Controllers;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Areas.Administration.Controllers;
using AlphaCinemaWeb.Areas.Administration.Models.UserManageViewModels;
using Microsoft.AspNetCore.Http;
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
	public class UserManageController_Should
	{
		private Mock<IUserService> userServiceMock = new Mock<IUserService>();
		private User user;
		private IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
		private UserManageController controller;

		[TestMethod]
		public async Task IndexAction_ReturnsViewResult()
		{
			// Arrange && Act
			var controller = SetupController(1);

			var result = await controller.Index();

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public async Task IndexAction_ReturnsCorrectViewModel()
		{
			// Arrange && Act
			var controller = SetupController(1);

			var result = await controller.Index() as ViewResult;

			// Assert
			Assert.IsInstanceOfType(result.Model, typeof(UsersListViewModel));
		}

		[TestMethod]
		public async Task IndexAction_CallCorrectServiceMethod()
		{
			// Arrange && Act
			var controller = SetupController(1);

			var result = await controller.Index() as ViewResult;

			// Assert

			userServiceMock.Verify(s => s.GetAllUsers(), Times.Once);
			userServiceMock.Verify(a => a.IsUserAdmin(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
		}

		[TestMethod]
		public async Task SetAdminAction_ReturnsToIndexUserManage_WhenUserIsNull_RedirectResult()
		{
			// Arrange
			var controller = this.SetupController(2);
			string userId = "userId";

			// Act
			var result = await controller.SetAdmin(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("Index", redirectResult.ActionName);
			Assert.AreEqual("UserManage", redirectResult.ControllerName);
			Assert.IsNull(redirectResult.RouteValues);
		}

		[TestMethod]
		public async Task SetAdminAction_ReturnsToIndexUserManage_WhenUserIsNotNull_RedirectResult()
		{
			// Arrange
			string userId = "userId";

			// Act
			var controller = SetupController(3);

			var result = await controller.SetAdmin(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("Index", redirectResult.ActionName);
			Assert.AreEqual("UserManage", redirectResult.ControllerName);
			Assert.IsNull(redirectResult.RouteValues);
		}

		[TestMethod]
		public async Task RemoveAdminAction_ReturnsToIndexUserManage_WhenUserIsNull_RedirectResult()
		{
			// Arrange
			string userId = "userId";

			// Act
			var controller = SetupController(2);

			var result = await controller.SetAdmin(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("Index", redirectResult.ActionName);
			Assert.AreEqual("UserManage", redirectResult.ControllerName);
			Assert.IsNull(redirectResult.RouteValues);
		}

		[TestMethod]
		public async Task RemoveAdminAction_ReturnsToIndexUserManage_WhenUserIsNotNull_RedirectResult()
		{
			// Arrange
			string userId = "userId";

			// Act
			var controller = SetupController(3);

			var result = await controller.RemoveAdmin(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("Index", redirectResult.ActionName);
			Assert.AreEqual("UserManage", redirectResult.ControllerName);
			Assert.IsNull(redirectResult.RouteValues);
		}


		private Mock<IUserService> SetupMockService(int test)
		{
			switch (test)
			{
				case 1:
					userServiceMock
							.Setup(x => x.GetAllUsers())
					.ReturnsAsync(new List<User>() { new User() { FirstName = "Test" } });

					userServiceMock
						.Setup(x => x.IsUserAdmin(It.IsAny<string>(), It.IsAny<string>()))
						.ReturnsAsync(true);
					break;

				case 2:
					userServiceMock
						.Setup(x => x.GetUser(It.IsAny<string>()))
						.ReturnsAsync(Task.FromResult<User>(null).Result);
					break;

				case 3:
					user = new User() { FirstName = "Test me" };
					userServiceMock
						.Setup(m => m.GetUser(It.IsAny<string>()))
						.ReturnsAsync(user);
					break;
			}
			return userServiceMock;
		}

		private UserManageController SetupController(int test)
		{
			switch (test)
			{
				case 1:
					// user list and is admin true
					userServiceMock = SetupMockService(test);
					break;
				case 2:
					// user == null
					userServiceMock = SetupMockService(test);
					break;
				case 3:
					// user != null
					userServiceMock = SetupMockService(test);
					break;
			}

			controller = new UserManageController(userServiceMock.Object, cache)
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
