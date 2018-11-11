using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaServices.Exceptions;
using AlphaCinemaWeb.Controllers;
using AlphaCinemaWeb.Models.ProjectionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaWebTests.ControllersTests.BuyTicketControllerTests
{
    [TestClass]
    public class UpdateMovie_Should
    {
        private Mock<IProjectionService> projectionsServiceMock;
        private Mock<ICityService> cityServiceMock;
        private Mock<UserManager<User>> mockUserManager;
        private IMemoryCache memoryCacheMock;
        private string userId = "1";
        private string userName = "Ivan";
        private ClaimsPrincipal user;
        private BuyTicketController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            memoryCacheMock = new MemoryCache(new MemoryCacheOptions());
            mockUserManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);
            projectionsServiceMock = new Mock<IProjectionService>();
            cityServiceMock = new Mock<ICityService>();
            controller = new BuyTicketController(projectionsServiceMock.Object, cityServiceMock.Object,
                mockUserManager.Object, memoryCacheMock);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
            controller.TempData = new Mock<ITempDataDictionary>().Object;
            user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userId),
                 new Claim(ClaimTypes.Name, userName),
            }));
        }

        [TestMethod]
        public async Task ThrowInvalidClientInputException_WhenModelStateIsNotValid()
        {
            //Act
            var viewModel = new ProjectionListViewModel();
            controller.ModelState.AddModelError("error", "error");

            //Assert
            await Assert.ThrowsExceptionAsync<InvalidClientInputException>(async () => await controller.UpdateMovie(viewModel));
        }
    }
}
