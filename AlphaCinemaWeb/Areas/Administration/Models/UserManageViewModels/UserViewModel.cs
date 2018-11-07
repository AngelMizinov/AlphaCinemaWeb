using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.UserManageViewModels
{
	public class UserViewModel
	{
		public UserViewModel()
		{

		}

		public UserViewModel(User user)
		{
			Username = user.UserName;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Age = user.Age;
			this.Id = user.Id;
		}

		public string Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }
		public bool IsAdmin { get; set; }
	}
}
