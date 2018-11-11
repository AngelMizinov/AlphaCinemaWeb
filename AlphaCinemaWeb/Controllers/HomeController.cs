using Microsoft.AspNetCore.Mvc;

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
			return View();
		}
	}
}
