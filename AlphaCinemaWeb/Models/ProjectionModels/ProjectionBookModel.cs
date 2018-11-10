using System.ComponentModel.DataAnnotations;

namespace AlphaCinemaWeb.Models.ProjectionModels
{
    public class ProjectionBookModel
    {
        public ProjectionBookModel()
        {

        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CityId { get; set; }

        [Required]
        public int ProjectionId { get; set; }
    }
}
