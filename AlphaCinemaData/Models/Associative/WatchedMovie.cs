using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;

namespace AlphaCinemaData.Models.Associative
{
    public class WatchedMovie : Entity
	{
		public string UserId { get; set; }
		public int ProjectionId { get; set; }

		public User User { get; set; }

		public Projection Projection { get; set; }

		public DateTime Date { get; set; }

	}
}
