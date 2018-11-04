using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaData.Models.Associative
{
    public class Projection : Entity
	{
		public int MovieId { get; set; }

		public int CityId { get; set; }

		public int OpenHourId { get; set; }

		public Movie Movie { get; set; }

		public City City { get; set; }

		public OpenHour OpenHour { get; set; }

		[Range(0, 6, ErrorMessage = "Day must be between 0-6")]
		public int Day { get; set; }

        public int Seats { get; set; }

        public ICollection<WatchedMovie> WatchedMovies { get; set; }
	}
}
