using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.ServicesTests.CityServiceTests
{
    [TestClass]
    public class AddCity_Should
    {
        [TestMethod]
        [DataRow("TestName")]
        public async Task AddNewCity_WhenParametersAreCorrect(string cityName)
        {
            var contextOptions = new DbContextOptionsBuilder<AlphaCinemaContext>()
               .UseInMemoryDatabase(databaseName: "AddNewCity_WhenParametersAreCorrect")
               .Options;

            var city = new City()
            {
                Name = cityName
            };

            using (var context = new AlphaCinemaContext(contextOptions))
            {
                await context.Cities.AddAsync(city);
                await context.SaveChangesAsync();

                var cityService = new CityService(context);

                city = await cityService.AddCity(cityName);
            }

            //Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                Assert.IsInstanceOfType(city, typeof(City));
                Assert.IsNotNull(city);
            }
        }

        [TestMethod]
        [DataRow("Pl")]
        public void Throws_InvalidClientInputException_WhenCityNameIsNotCorrect(string cityName)
        {
            var mockContext = new Mock<AlphaCinemaContext>();

            var cityService = new CityService(mockContext.Object);

            Assert.ThrowsExceptionAsync<InvalidClientInputException>(() => cityService.AddCity(cityName));
        }

        [TestMethod]
        [DataRow("TestName")]
        public void Throws_EntityAlreadyExistsException_WhenCityAlreadyExists(string cityName)
        {

            var mockContext = new Mock<AlphaCinemaContext>();

        }

    }
}
