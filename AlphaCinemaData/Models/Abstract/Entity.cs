using AlphaCinemaData.Models.Contracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaData.Models.Abstract
{
    public abstract class Entity : IDeletable
	{
		public int Id { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

	}
}
