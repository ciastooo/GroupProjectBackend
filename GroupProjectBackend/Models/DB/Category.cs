using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class Category
    {
        public Category()
        {
            Places = new HashSet<Place>();
        }

        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(255)]
        public string Description { get; set; }

        public virtual ICollection<Place> Places { get; set; }
    }
}
