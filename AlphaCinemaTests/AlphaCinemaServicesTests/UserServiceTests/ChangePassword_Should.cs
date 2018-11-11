using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaWeb.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.UserServiceTests
{
	[TestClass]
	public class ChangePassword_Should
	{
		private DbContextOptions<AlphaCinemaContext> contextOptions;
		private Mock<IServiceProvider> serviceProviderMock = new Mock<IServiceProvider>();
		private User user;

		[TestMethod]
		public async Task ThrowEntityDoesntExistExceptionWhenUserIsNull()
		{
			// Arrange
			contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistExceptionWhenUserIsNull")
				.Options;

			//Act and Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() =>
				userService.ChangePassword(user, It.IsAny<string>(), It.IsAny<string>()));
			}
		}

		[TestMethod]
		public async Task ThrowEntityDoesntExistExceptionWhenUserIsDeleted()
		{
			// Arrange
			contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "ThrowEntityDoesntExistExceptionWhenUserIsDeleted")
				.Options;

			user = new User() { IsDeleted = true };
			//Act and Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() =>
				userService.ChangePassword(user, It.IsAny<string>(), It.IsAny<string>()));
			}
		}
	}
}
