using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.Dto
{
    public class CategoryModelDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual PlaceModelDto Place { get; set; }
    }
}
