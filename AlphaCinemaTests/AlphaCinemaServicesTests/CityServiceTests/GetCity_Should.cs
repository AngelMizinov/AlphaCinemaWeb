using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.CityServiceTests
{
    [TestClass]
    public class GetCity_Should
    {

        [TestMethod]
        public async Task ReturnCityObject_WithTheSameName_As_Passed()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
          new DbContextOptionsBuilder<AlphaCinemaContext>()
          .UseInMemoryDatabase(databaseName: "ReturnCityObject_WithTheSameName_As_Passed")
              .Options;

            // Arrange
            var cityName = "TestCityName";
            
            var city = new City()
            {
                Name = cityName
            };

            // Act && Assert
            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.Cities.AddAsync(city);
                await actContext.SaveChangesAsync();
            }
            
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                city = await cityServices.GetCity(cityName);

                Assert.AreEqual(cityName, city.Name);
            }
        }

        [TestMethod]
        public async Task ReturnNull_When_CityDoesntExists()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
          new DbContextOptionsBuilder<AlphaCinemaContext>()
          .UseInMemoryDatabase(databaseName: "ReturnNull_When_CityDoesntExists")
              .Options;

            // Arrange
            var cityName = "TestCityName";

            var city = new City()
            {
                Name = cityName
            };

            // Act && Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                city = await cityServices.GetCity(cityName);

                Assert.AreEqual(null, city);
            }
        }

    }
}
