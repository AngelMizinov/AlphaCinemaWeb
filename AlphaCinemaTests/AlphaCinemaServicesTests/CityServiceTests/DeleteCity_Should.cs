using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaWeb.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaTests.AlphaCinemaServicesTests.CityServiceTests
{
    [TestClass]
    public class DeleteCity_Should
    {
        [TestMethod]
        public async Task Throw_EntityDoesntExistException_WhenCityDoesnotExists()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
          new DbContextOptionsBuilder<AlphaCinemaContext>()
          .UseInMemoryDatabase(databaseName: "Throw_EntityDoesntExistException_WhenCityDoesnotExists")
              .Options;

            // Arrange
            var cityName = "TestCityName";

            var city = new City()
            {
                Name = cityName
            };

            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);

                await Assert.ThrowsExceptionAsync<EntityDoesntExistException>(() =>
                cityServices.DeleteCity(cityName));
            }
        }


        [TestMethod]
        public async Task DeleteCityObject_When_CityExists()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
          new DbContextOptionsBuilder<AlphaCinemaContext>()
          .UseInMemoryDatabase(databaseName: "DeleteCityObject_When_CityExists")
              .Options;

            // Arrange
            var cityName = "TestCityName";

            var city = new City()
            {
                Name = cityName
            };


            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.Cities.AddAsync(city);
                await actContext.SaveChangesAsync();
            }

            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                await cityServices.DeleteCity(cityName);

                city = await assertContext.Cities.FirstAsync(c => c.Name == cityName);

                Assert.IsTrue(city.IsDeleted == true);
            }
        }
    }
}
