using AlphaCinemaData.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Models.GenreViewModels
{
	public class GenreViewModel
	{
		public GenreViewModel()
		{
           
		}

		public GenreViewModel(Genre genre)
		{
			this.Name = genre.Name;
			this.Id = genre.Id;
		}

		[Required]
		[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Genre name cannot have any numbers.")]
		[StringLength(15, ErrorMessage = "Genre name should be between 3 and 50 symbols.",
			MinimumLength = 3)]
		public string Name { get; set; }

		public int Id { get; set; }

        public bool IsChecked { get; set; } 
    }
}