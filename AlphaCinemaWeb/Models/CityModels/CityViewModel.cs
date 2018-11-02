using AlphaCinemaData.Models;

namespace AlphaCinemaWeb.Models.CityModels
{
    public class CityViewModel
    {
        public CityViewModel(City city)
        {
            this.Name = city.Name;
            this.Id = city.Id;
        }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}
