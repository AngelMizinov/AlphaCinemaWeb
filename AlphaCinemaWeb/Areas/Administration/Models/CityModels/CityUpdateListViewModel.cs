using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.CityModels
{
	public class CityUpdateListViewModel
	{
        public CityUpdateListViewModel()
        {
            
        }

		public CityUpdateListViewModel(IEnumerable<CityUpdateViewModel> cities)
		{
			this.Cities = cities;
            
        }


        [Required]
        [RegularExpression(@"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "City name cannot have any numbers.")]
        [StringLength(15, ErrorMessage = "City name should be between 3 and 50 symbols.",
            MinimumLength = 3)]
        public string Name { get; set; }

        public int Id { get; set; }

        public IEnumerable<CityUpdateViewModel> Cities{ get; set; }
	}
}
