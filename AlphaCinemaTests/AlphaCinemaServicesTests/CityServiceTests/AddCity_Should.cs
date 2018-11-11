using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices;
using AlphaCinemaServices.Exceptions;
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
    public class AddCity_Should
    {
        [TestMethod]
        public async Task Throw_InvalidClientInputException_When_ParametersAreNotValid()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
           new DbContextOptionsBuilder<AlphaCinemaContext>()
           .UseInMemoryDatabase(databaseName: "Throw_InvalidClientInputException_When_ParametersAreNotValid")
               .Options;

            // Arrange
            var cityName = "Te";

            var city = new City()
            {
                Name = cityName
            };

            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.Cities.AddAsync(city);
                await actContext.SaveChangesAsync();
            }

            // Act && Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                await Assert.ThrowsExceptionAsync<InvalidClientInputException>(() =>
                cityServices.AddCity(cityName));
            }
        }

        [TestMethod]
        public async Task Create_NewCity_When_CorrectParameters_ArePassed()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
          new DbContextOptionsBuilder<AlphaCinemaContext>()
          .UseInMemoryDatabase(databaseName: "Create_NewCity_When_CorrectParameters_ArePassed")
              .Options;

            // Arrange
            var cityName = "TestCityName";
            
            // Act && Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                var city = await cityServices.AddCity(cityName);
                
                Assert.AreEqual(cityName, city.Name);
            }
        }

        [TestMethod]
        public async Task Throw_EntityAlreadyExistsException_When_ObjectAlreadyExists()
        {
            DbContextOptions<AlphaCinemaContext> contextOptions =
           new DbContextOptionsBuilder<AlphaCinemaContext>()
           .UseInMemoryDatabase(databaseName: "Throw_InvalidClientInputException_When_ParametersAreNotValid")
               .Options;

            // Arrange
            var cityName = "TestCityName";

            var city = new City()
            {
                Name = cityName
            };

            var sameCity = new City()
            {
                Name = cityName
            };


            using (var actContext = new AlphaCinemaContext(contextOptions))
            {
                await actContext.Cities.AddAsync(city);
                await actContext.Cities.AddAsync(sameCity);
                await actContext.SaveChangesAsync();
            }

            // Act && Assert
            using (var assertContext = new AlphaCinemaContext(contextOptions))
            {
                var cityServices = new CityService(assertContext);
                await Assert.ThrowsExceptionAsync<EntityAlreadyExistsException>(() =>
                cityServices.AddCity(cityName));
            }
        }


    }
}
