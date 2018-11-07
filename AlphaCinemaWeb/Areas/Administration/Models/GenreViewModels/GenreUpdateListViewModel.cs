using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.GenreViewModels
{
	public class GenreUpdateListViewModel
	{
		public GenreUpdateListViewModel()
		{

		}

		public GenreUpdateListViewModel(IEnumerable<GenreUpdateViewModel> genres)
		{
			this.Genres = genres;
		}

		public IEnumerable<GenreUpdateViewModel> Genres { get; set; }
	}
}
