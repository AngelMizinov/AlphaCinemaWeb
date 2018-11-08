using AlphaCinemaWeb.Models.GenreViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCinemaWeb.Areas.Administration.Models.GenreViewModels
{
    public class GenreListViewModel
    {

        public GenreListViewModel()
        {

        }

        public GenreListViewModel(ICollection<GenreViewModel> genres)
        {
            this.Genres = genres;
        }

        public ICollection<GenreViewModel> Genres { get; set; }



    }
}
