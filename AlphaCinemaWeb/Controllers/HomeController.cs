﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlphaCinema.Models;


namespace AlphaCinema.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
            this.ViewData["ReturnUrl"] = "/Home/Index";
            return View();
		}

		public IActionResult About()
		{
			ViewData["Message"] = "Your application description page.";

			return View();
		}
		public IActionResult Contact()
		{
			ViewData["Message"] = "Your contact page.";

			return View();
		}
	}
}
