using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinema.Models.ManageViewModels
{
	public class IndexViewModel
	{
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }

		public string ImageUrl { get; set; }


		public string StatusMessage { get; set; }

	}
}
