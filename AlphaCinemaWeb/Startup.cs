using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AlphaCinemaData.Context;
using AlphaCinemaServices;
using AlphaCinemaServices.Contracts;
using AlphaCinemaData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlphaCinema
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<AlphaCinemaContext>(options =>
				options.UseSqlServer(Environment.GetEnvironmentVariable("AlphaCinemaConnection", EnvironmentVariableTarget.User) ?? "AlphaCinemaConnection"));

			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<AlphaCinemaContext>()
				.AddDefaultTokenProviders();

			services.AddResponseCaching();

			services.AddMvc(options =>
			{
				options.CacheProfiles.Add("Default",
				new CacheProfile()
				{
					Duration = 3600
				});
			});
			services.AddMemoryCache();

			services.AddScoped<IProjectionService, ProjectionService>();
			services.AddScoped<ICityService, CityService>();
			services.AddScoped<IMovieService, MovieService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IWatchedMoviesService, WatchedMoviesService>();
			services.AddScoped<IMovieGenreService, MovieGenreService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
		{
			if (env.IsDevelopment())
			{
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
                //Добавих ги за да видим защо гърми azure, тъй като той компилира в production
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseResponseCaching();


			// seed an admin account
			//AdministrationManager(serviceProvider);

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "Administration",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");

			});
		}
	}
}


//		private void AdministrationManager(IServiceProvider serviceProvider)
//		{
//			const string adminRoleName = "Administrator";
//			string[] roleNames = { adminRoleName, "Manager", "Member" };


//			CreateRole(serviceProvider, adminRoleName);

//			Get these value from "appsettings.json" file.
//			string adminUserEmail = "krasimir@alpha.com";
//			string adminPwd = "Krasimir123!";
//			AddUserToRole(serviceProvider, adminUserEmail, adminPwd, adminRoleName);
//		}

//		private void CreateRole(IServiceProvider serviceProvider, string roleName)
//		{
//			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

//			Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
//			roleExists.Wait();

//			if (!roleExists.Result)
//			{
//				Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
//				roleResult.Wait();
//			}
//		}

//		private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail,
//			string userPwd, string roleName)
//		{
//			var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

//			Task<User> checkUser = userManager.FindByEmailAsync(userEmail);
//			checkUser.Wait();

//			var user = checkUser.Result;

//			if (checkUser.Result == null)
//			{
//				var newUser = new User
//				{
//					FirstName = "Krasimir",
//					LastName = "Etov",
//					Age = 21,
//					Email = userEmail,
//					UserName = userEmail
//				};

//				Task<IdentityResult> taskCreateUser = userManager.CreateAsync(newUser, userPwd);
//				taskCreateUser.Wait();

//				if (taskCreateUser.Result.Succeeded)
//				{
//					user = newUser;
//				}
//			}
//			Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(user, roleName);
//			newUserRole.Wait();
//		}
//	}
//}
