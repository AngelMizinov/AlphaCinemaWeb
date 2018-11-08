using AlphaCinemaWeb.Areas.Administration.Models.UserManageViewModels;
using AlphaCinemaWeb.Models.ProjectionModels;
using System;

namespace AlphaCinemaWeb.Models.WatchedMovieModels
{
	public class WatchedMovieViewModel
	{
		public WatchedMovieViewModel()
		{

		}

		public WatchedMovieViewModel(string movieName, string cityName, int hours, int minutes, DateTime watchedOn)
		{
			MovieName = movieName;
			CityName = cityName;
			Hours = hours;
			Minutes = minutes;
			WatchedOn = watchedOn;
		}

		public string MovieName { get; set; }

		public string CityName { get; set; }
		public int Hours { get; set; }
		public int Minutes { get; set; }
		public DateTime WatchedOn { get; set; }
	}
}
