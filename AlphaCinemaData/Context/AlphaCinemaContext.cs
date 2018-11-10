using AlphaCinemaData.Configurations;
using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCinemaData.Context
{
	public class AlphaCinemaContext : IdentityDbContext<User>
	{
		public DbSet<City> Cities { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<MovieGenre> MoviesGenres { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Projection> Projections { get; set; }
		public DbSet<OpenHour> OpenHours { get; set; }
		public DbSet<WatchedMovie> WatchedMovies { get; set; }
		public AlphaCinemaContext() { }
		public AlphaCinemaContext(DbContextOptions<AlphaCinemaContext> options)
		: base(options)
		{
		}

		public async Task<int> SaveChangesAsync()
		{
			this.ApplyAuditInfoRules();
			this.ApplyDeletionRules();

			return await base.SaveChangesAsync();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>()
				.HasData(new IdentityRole { Name = "Admin"});

			builder.ApplyConfiguration(new CityConfiguration());
			builder.ApplyConfiguration(new GenreConfiguration());
			builder.ApplyConfiguration(new MovieConfiguration());
			builder.ApplyConfiguration(new MovieGenreConfiguration());
			builder.ApplyConfiguration(new OpenHourConfiguration());
			builder.ApplyConfiguration(new ProjectionConfiguration());
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new WatchedMovieConfiguration());

			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}

		private void ApplyDeletionRules()
		{
			var entitiesForDeletion = this.ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Deleted && e.Entity is IDeletable);

			foreach (var entry in entitiesForDeletion)
			{
				var entity = (IDeletable)entry.Entity;
				entity.DeletedOn = DateTime.Now;
				entity.IsDeleted = true;
				entry.State = EntityState.Modified;
			}
		}

		private void ApplyAuditInfoRules()
		{
			var newlyCreatedEntities = this.ChangeTracker.Entries()
				.Where(e => e.Entity is IAuditable && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

			foreach (var entry in newlyCreatedEntities)
			{
				var entity = (IAuditable)entry.Entity;

				if (entry.State == EntityState.Added && entity.CreatedOn == null)
				{
					entity.CreatedOn = DateTime.Now;
				}
				else
				{
					entity.ModifiedOn = DateTime.Now;
				}
			}
		}
	}
}
