using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCinemaData.Models.Abstract
{
	public interface IAuditable
	{
		DateTime CreatedOn { get; set; }

		DateTime? ModifiedOn { get; set; }
	}
}
