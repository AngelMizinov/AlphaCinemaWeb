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

		[TestMethod]
		public async Task IndexAction_ReturnsViewResult()
		{
			// Arrange
			var userServiceMock = this.SetupMockService();
			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

			var controller = new UserManageController(userServiceMock.Object, cache);

			// Act
			var result = await controller.Index();

			// Assert
			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}

		[TestMethod]
		public async Task IndexAction_ReturnsCorrectViewModel()
		{
			// Arrange
			var userServiceMock = this.SetupMockService();
			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

			// Act
			var controller = new UserManageController(userServiceMock.Object, cache);
			var result = await controller.Index() as ViewResult;

			// Assert
			Assert.IsInstanceOfType(result.Model, typeof(UsersListViewModel));
		}

		[TestMethod]
		public async Task IndexAction_CallCorrectServiceMethod()
		{
			// Arrange
			var userServiceMock = this.SetupMockService();
			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

			// Act
			var controller = new UserManageController(userServiceMock.Object, cache);
			var result = await controller.Index() as ViewResult;

			// Assert

			userServiceMock.Verify(s => s.GetAllUsers(), Times.Once);
			userServiceMock.Verify(a => a.IsUserAdmin(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
		}

		[TestMethod]
		public async Task SetAdminAction_Returns_To_IndexUserManage_WhenUserIsNull_RedirectResult()
		{
			// Arrange
			var userServiceMock = this.SetupMockService();
			var controller = this.SetupController();
			string userId = "userId";
			userServiceMock
				.Setup(x => x.GetUser(It.IsAny<string>()))
				.ReturnsAsync(Task.FromResult<User>(null).Result);

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
		public async Task SetAdminAction_Returns_To_IndexUserManage_WhenUserIsNotNull_RedirectResult()
		{
			// Arrange
			string userId = "userId";
			User user = new User() { FirstName = "Test me" };

			var userServiceMock = new Mock<IUserService>();
			userServiceMock
				.Setup(m => m.GetUser(It.IsAny<string>()))
				.ReturnsAsync(user);

			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

			var controller = new UserManageController(userServiceMock.Object, cache)
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
		public async Task RemoveAdminAction_Returns_To_IndexUserManage_WhenUserIsNull_RedirectResult()
		{
			// Arrange
			var userServiceMock = this.SetupMockService();
			var controller = this.SetupController();
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
		public async Task RemoveAdminAction_Returns_To_IndexUserManage_WhenUserIsNotNull_RedirectResult()
		{
			// Arrange
			string userId = "userId";
			User user = new User() { FirstName = "Test me" };

			var userServiceMock = new Mock<IUserService>();
			userServiceMock
				.Setup(m => m.GetUser(It.IsAny<string>()))
				.ReturnsAsync(user);

			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

			var controller = new UserManageController(userServiceMock.Object, cache)
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

			// Act
			var result = await controller.RemoveAdmin(userId);

			// Assert
			Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
			var redirectResult = (RedirectToActionResult)result;
			Assert.AreEqual("Index", redirectResult.ActionName);
			Assert.AreEqual("UserManage", redirectResult.ControllerName);
			Assert.IsNull(redirectResult.RouteValues);
		}


		private Mock<IUserService> SetupMockService()
		{
			var userServiceMock = new Mock<IUserService>();

			userServiceMock
				.Setup(x => x.GetAllUsers())
				.ReturnsAsync(new List<User>() { new User() { FirstName = "Test" } });

			userServiceMock
				.Setup(x => x.IsUserAdmin(It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(true);

			userServiceMock
				.Setup(x => x.GetUser(It.IsAny<string>()))
				.ReturnsAsync(Task.FromResult<User>(null).Result);

			return userServiceMock;
		}

		private UserManageController SetupController()
		{
			var userServiceMock = SetupMockService();
			IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());


			var controller = new UserManageController(userServiceMock.Object, cache)
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
