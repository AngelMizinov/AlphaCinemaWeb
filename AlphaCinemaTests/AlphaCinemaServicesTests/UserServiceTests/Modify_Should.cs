using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.UserServiceTests
{
	[TestClass]
	public class Modify_Should
	{
		private readonly DbContextOptions<AlphaCinemaContext> contextOptions =
			new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
				.Options;

		[TestMethod]
		public async Task ThrowEntityDoesntExistExceptionWhenUserIsNotFound()
		{
			// Arrange

			var serviceProviderMock = new Mock<IServiceProvider>();

			//Act and Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() => userService.Modify("UserId"));
			}
		}

		[TestMethod]
		public async Task ThrowEntityDoesntExistExceptionWhenUserIsDeleted()
		{
			// Arrange

			var serviceProviderMock = new Mock<IServiceProvider>();

			var user = new User()
			{
				Id = "my id",
				FirstName = "Krasimir",
				LastName = "Etov",
				Age = 21,
				IsDeleted = true
			};

			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				await actContext.Users.AddAsync(user);
				await actContext.SaveChangesAsync();
			}
			//Act and Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() => userService.Modify(user.Id));
			}
		}

		[TestMethod]
		public async Task SuccessfullyModifyDateWhenUserIsValid()
		{
			// Arrange

			var serviceProviderMock = new Mock<IServiceProvider>();
			string userId = "djoni";
			var user = new User()
			{
				Id = userId,
				FirstName = "Krasimir",
				LastName = "Etov",
				Age = 21
			};

			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				await actContext.Users.AddAsync(user);
				await actContext.SaveChangesAsync();
			}
			// Act && Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				await userService.Modify(userId);
				// please don't kill me
				user = await userService.GetUser(userId);
				Assert.AreEqual(DateTime.Now.Year, user.ModifiedOn.Value.Year);
			}
		}
	}
}
