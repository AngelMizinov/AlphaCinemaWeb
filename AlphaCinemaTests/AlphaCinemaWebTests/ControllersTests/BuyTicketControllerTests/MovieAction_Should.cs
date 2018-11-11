using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Controllers;
using AlphaCinemaWeb.Models.ProjectionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaWebTests.ControllersTests.BuyTicketControllerTests
{
    [TestClass]
    public class MovieAction_Should
    {
        private Mock<IProjectionService> projectionsServiceMock;
        private Mock<ICityService> cityServiceMock;
        private Mock<UserManager<User>> mockUserManager;
        private IMemoryCache memoryCacheMock;
        private Projection projection;
        private int cityId = 1;
        private int movieId = 1;
        private int openHourId = 1;
        private City city;
        private string cityName = "TestCityName";
        private IEnumerable<Projection> projections;
        private ICollection<City> cities;
        private string userId = "1";
        private string userName = "Ivan";
        private ClaimsPrincipal user;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            projection = new Projection()
            {
                CityId = cityId,
                MovieId = movieId,
                OpenHourId = openHourId
            };
            city = new City()
            {
                Name = cityName
            };
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
            projections = new List<Projection>() { projection };
            cities = new List<City>() { city };
            user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                 new Claim(ClaimTypes.NameIdentifier, userId),
                 new Claim(ClaimTypes.Name, userName),
            }));
        }

        [TestMethod]
        public async Task ReturnViewResult_WhenParametersAreCorrect()
        {
            //Arrange
            projectionsServiceMock = new Mock<IProjectionService>();
            
            cityServiceMock = new Mock<ICityService>();

            //Act
            var controller = new BuyTicketController(projectionsServiceMock.Object, cityServiceMock.Object,
                mockUserManager.Object, memoryCacheMock);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = await controller.Movie(cityId);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public async Task ReturnsCorrectViewModel_WhenParametersAreCorrect()
        {
            //Arrange
            projectionsServiceMock = new Mock<IProjectionService>();

            cityServiceMock = new Mock<ICityService>();

            //Act
            var controller = new BuyTicketController(projectionsServiceMock.Object, cityServiceMock.Object,
                mockUserManager.Object, memoryCacheMock);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = await controller.Movie(cityId) as ViewResult;

            //Assert
            Assert.IsInstanceOfType(result.Model, typeof(ProjectionListViewModel));
        }

        [TestMethod]
        public async Task CallCorrectServiceMethod_WhenInvoked()
        {
            //Arrange
            projectionsServiceMock = new Mock<IProjectionService>();
            projectionsServiceMock
                .Setup(ps => ps.GetByTownId(It.IsAny<int>(), It.IsAny<string>(), null))
                .ReturnsAsync(projections);

            cityServiceMock = new Mock<ICityService>();
            cityServiceMock
                .Setup(city => city.GetCityName(It.IsAny<int>()))
                .Returns(Task.FromResult(cityName));

            //Act
            var controller = new BuyTicketController(projectionsServiceMock.Object, cityServiceMock.Object,
                mockUserManager.Object, memoryCacheMock);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            var result = await controller.Movie(cityId) as ViewResult;

            //Assert
            projectionsServiceMock.Verify(projection => projection.GetByTownId(It.IsAny<int>(), It.IsAny<string>(), null), Times.Once);
            cityServiceMock.Verify(city => city.GetCityName(It.IsAny<int>()), Times.Once);
        }
    }
}
