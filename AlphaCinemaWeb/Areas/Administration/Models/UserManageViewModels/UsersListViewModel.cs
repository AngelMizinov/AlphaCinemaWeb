using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.UserManageViewModels
{
	public class UsersListViewModel
	{
		public UsersListViewModel(IEnumerable<UserViewModel> userViewModels)
		{
			this.UserViewModels = userViewModels;
		}

		public IEnumerable<UserViewModel> UserViewModels { get; set; }
	}
}
