using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityViewModel
    {
		public CityViewModel()
		{

		}

        public CityViewModel(City city)
        {
            this.Name = city.Name;
            this.Id = city.Id;
        }

		[Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City name cannot have any numbers.")]
		[StringLength(15, ErrorMessage = "City name should be between 3 and 50 symbols.",
			MinimumLength = 3)]
		public string Name { get; set; }

        public int Id { get; set; }
	}
}
