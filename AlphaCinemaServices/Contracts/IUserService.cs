using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices.Contracts
{
	public interface IUserService
	{
		Task<User> GetUser(string userId);
		Task<ICollection<User>> GetAllUsers();
		Task SetRole(string userId, string roleName);
		Task<bool> IsUserAdmin(string userId, string roleName);
		Task RemoveRole(string userId, string roleName);
	}
}
