﻿using AlphaCinemaData.Models;
using AlphaCinemaWeb.Models.CityModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.CityModels
{
	public class CityUpdateViewModel
	{
		public CityUpdateViewModel()
		{

		}
		public CityUpdateViewModel(City city)
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

		[Required]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "City name cannot have any numbers.")]
		[StringLength(15, ErrorMessage = "City name should be between 3 and 50 symbols.",
			MinimumLength = 3)]
		public string OldName { get; set; }
	}
}