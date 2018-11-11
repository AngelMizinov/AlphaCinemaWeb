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
	public class GetAllUsers_Should
	{
		private readonly DbContextOptions<AlphaCinemaContext> contextOptions =
			new DbContextOptionsBuilder<AlphaCinemaContext>()
			.UseInMemoryDatabase(databaseName: "AddEntityToBase_WhenEntityIsCorrect")
				.Options;

		[TestMethod]
		public async Task CorrectlyReturnAllUsers()
		{
			// Arrange
			var serviceProviderMock = new Mock<IServiceProvider>();
			var user = new User()
			{
				FirstName = "Krasimir",
				LastName = "Etov",
				Age = 21,
			};
			//Act
			using (var actContext = new AlphaCinemaContext(contextOptions))
			{
				await actContext.Users.AddAsync(user);
				await actContext.SaveChangesAsync();
			}

			//Assert
			using (var assertContext = new AlphaCinemaContext(contextOptions))
			{
				var userService = new UserService(serviceProviderMock.Object, assertContext);
				var users = await userService.GetAllUsers();
				Assert.IsTrue(users.Count == 1);
			}
		}
	}
}
