using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.UserServiceTests
{
	[TestClass]
	public class GetUser_Should
	{
		private readonly DbContextOptions<AlphaCinemaContext> contextOptions =
			new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
				.Options;

		[TestMethod]
		public async Task ReturnUserIfUserIdIsFound()
		{
			// Arrange
			string userId = "myId";
			var serviceProviderMock = new Mock<IServiceProvider>();
			var user = new User()
			{
				Id = userId,
				FirstName = "Krasimir",
				LastName = "Etov",
				Age = 21,
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

				var result = await userService.GetUser(userId);
				Assert.IsNotNull(result);
				Assert.AreEqual(result.Id, userId);
			}
		}

		[TestMethod]
		public async Task ReturnNullIfUserIdIsNotFound()
		{
			// Arrange
			var serviceProviderMock = new Mock<IServiceProvider>();
			// Act && Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				var result = await userService.GetUser("no such id");

				Assert.IsNull(result);
			}
		}
	}
}
