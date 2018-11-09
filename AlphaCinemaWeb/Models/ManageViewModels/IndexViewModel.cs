using AlphaCinemaWeb.Models.MovieViewModels;
using AlphaCinemaWeb.Models.WatchedMovieModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinema.Models.ManageViewModels
{
	public class IndexViewModel
	{
		public IndexViewModel()
		{

		}
		public string Username { get; set; }
		[Required]
		[RegularExpression(@"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "First name cannot have any numbers.")]
		[StringLength(15, ErrorMessage = "First name should be between 3 and 50 symbols.",
			MinimumLength = 3)]
		public string FirstName { get; set; }
		[Required]
		[RegularExpression(@"^([a-zA-Z]+\s)*[a-zA-Z]+$", ErrorMessage = "Last name cannot have any numbers.")]
		[StringLength(15, ErrorMessage = "Last name should be between 3 and 50 symbols.",
			MinimumLength = 3)]
		public string LastName { get; set; }
		[Required]
		[Range(1, 100, ErrorMessage ="Age must be between 1 and 100")]
		public int Age { get; set; }
		public string UserId { get; set; }
		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime CreatedOn { get; set; }
		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		public string ImageUrl { get; set; }


		public string StatusMessage { get; set; }

		public ICollection<WatchedMovieViewModel> WatchedMovieViewModels { get; set; }

	}
}
