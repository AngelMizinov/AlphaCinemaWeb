using AlphaCinemaData.Models.Abstract;
using AlphaCinemaData.Models.Associative;
using AlphaCinemaData.Models.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaData.Models
{
    public class User : IdentityUser, IAuditable, IDeletable
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int Age { get; set; }

		//[Required(ErrorMessage = "The email address is required")]
		//[EmailAddress(ErrorMessage = "Invalid Email Address")]
		//public override string Email { get; set; }

		public string AvatarImageName { get; set; }

		public ICollection<WatchedMovie> WatchedMovies { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? CreatedOn { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? ModifiedOn { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }
	}
}
