using AlphaCinema.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaTests.AlphaCinemaWebTests.ControllersTests
{
	[TestClass]
	public class HomeController_Should
	{
		[TestMethod]
		public void IndexAction_ReturnsViewResult()
		{
			// Arrange && Act
			var controller = new HomeController();

			var result = controller.Index();

			Assert.IsInstanceOfType(result, typeof(ViewResult));
		}
	}
}
