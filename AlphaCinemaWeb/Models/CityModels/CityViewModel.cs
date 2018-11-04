using AlphaCinemaData.Models;
using AlphaCinemaData.Models.Associative;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityViewModel
    {
        public CityViewModel(City city)
        {
            this.Name = city.Name;
            this.Id = city.Id;
			this.IsDeleted = city.IsDeleted;
			this.DeletedOn = city.DeletedOn;
			this.Projections = city.Projections;
        }

        public string Name { get; set; }

        public int Id { get; set; }
		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }
		public ICollection<Projection> Projections { get; set; }
	}
}
