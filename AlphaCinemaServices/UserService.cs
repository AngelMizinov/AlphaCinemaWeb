using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using AlphaCinemaWeb.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices
{
	public class UserService : IUserService
	{
		private readonly IServiceProvider serviceProvider;

		private readonly AlphaCinemaContext context;

		public UserService(IServiceProvider serviceProvider, AlphaCinemaContext context)
		{
			this.serviceProvider = serviceProvider;
			this.context = context;
		}

		public async Task<ICollection<User>> GetAllUsers()
		{
			var users = await this.context.Users
				.ToListAsync();
			return users;
		}

		public async Task<User> GetUser(string userId)
		{
			var user = await this.context.Users
				.Where(u => u.Id == userId)
				.FirstOrDefaultAsync();
			return user;
		}

		public async Task SetRole(string userId, string roleName)
		{
			var user = await GetUser(userId);
			// check if user is alredady added in role
			await this.serviceProvider.GetRequiredService<UserManager<User>>().AddToRoleAsync(user, roleName);
		}

		public async Task RemoveRole(string userId, string roleName)
		{
			var user = await GetUser(userId);
			await this.serviceProvider.GetRequiredService<UserManager<User>>().RemoveFromRoleAsync(user, roleName);
		}

		public async Task<bool> IsUserAdmin(string userId, string roleName)
		{
			var result = await this.serviceProvider.GetRequiredService<UserManager<User>>()
				.IsInRoleAsync(GetUser(userId).Result, roleName);
			return result;
		}

		public async Task Modify(string userId)
		{
			var user = await GetUser(userId);

			if (user == null || user.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nUser is not present in the database.");
			}
			user.ModifiedOn = DateTime.UtcNow;

			this.context.Users.Update(user);
			await this.context.SaveChangesAsync();
		}

		public async Task<IdentityResult> ChangePassword(User user, string oldPassword, string newPassword)
		{
			if (user == null || user.IsDeleted)
			{
				throw new EntityDoesntExistException($"\nUser is not present in the database.");
			}

			return await this.serviceProvider.GetRequiredService<UserManager<User>>()
				.ChangePasswordAsync(user, oldPassword, newPassword);
		}

		public async Task<User> GetUserFromManager(ClaimsPrincipal User)
		{
			return await this.serviceProvider.GetRequiredService<UserManager<User>>()
				.GetUserAsync(User);
		}

		public async Task SaveAvatarImageAsync(string root, string filename, Stream stream, string userId)
		{
			var user = await GetUser(userId);
			if (user == null)
			{
				throw new EntityDoesntExistException("User not found");
			}

			var imageName = Guid.NewGuid().ToString() + Path.GetExtension(filename);
			var path = Path.Combine(root, imageName);

			using (var fileStream = File.Create(path))
			{
				await stream.CopyToAsync(fileStream);
			}
			
			user.Image = imageName;
			await Modify(userId);
		}
	}
}
