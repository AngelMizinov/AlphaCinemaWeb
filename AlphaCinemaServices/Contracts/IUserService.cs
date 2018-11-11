using AlphaCinemaData.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
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
		Task Modify(string userId);
		Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword);
		Task<User> GetUserFromManager(ClaimsPrincipal User);
		Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId);
	}
}
