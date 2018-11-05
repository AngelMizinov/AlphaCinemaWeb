using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.CityModels
{
	public class CityUpdateListViewModel
	{
		public CityUpdateListViewModel(IEnumerable<CityUpdateViewModel> cities)
		{
			this.Cities = cities;
		}

		public IEnumerable<CityUpdateViewModel> Cities{ get; set; }
	}
}
