using AlphaCinemaData.Context;
using AlphaCinemaData.Models;
using AlphaCinemaServices.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaServices
{
	public class UserService : IUserService
	{
		private readonly IServiceProvider serviceProvider;
		private readonly AlphaCinemaContext context;
		private readonly UserManager<User> userManager;

		public UserService(IServiceProvider serviceProvider, AlphaCinemaContext context)
		{
			this.serviceProvider = serviceProvider;
			this.context = context;
			this.userManager = serviceProvider.GetRequiredService<UserManager<User>>();
		}

		public async Task<ICollection<User>> GetAllUsers()
		{
			var users = await this.context
				.Users.ToListAsync();
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
			await this.userManager.AddToRoleAsync(user, roleName);
		}

		public async Task RemoveRole(string userId, string roleName)
		{
			var user = await GetUser(userId);
			await this.userManager.RemoveFromRoleAsync(user, roleName);
		}

		public async Task<bool> IsUserAdmin(string userId, string roleName)
		{
			var result = await this.userManager
				.IsInRoleAsync(GetUser(userId).Result, roleName);
			return result;
		}
	}
}
