using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaCinema.Models;
using Microsoft.Extensions.Caching.Memory;

namespace AlphaCinema.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			this.ViewData["ReturnUrl"] = "/Home/Index";
			return View();
		}

	}
}
