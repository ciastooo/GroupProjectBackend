using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class RouteModelDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public string Description { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public float Length { get; set; }

        [Required]
        public virtual ICollection<PlaceModelDto> StartPlace { get; set; }
        
    }
}
