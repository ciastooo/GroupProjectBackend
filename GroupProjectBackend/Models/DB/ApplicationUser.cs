using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GroupProjectBackend.Models.DB
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Roles = new HashSet<IdentityUserRole<string>>();
            Claims = new List<IdentityUserClaim<string>>();
            Logins = new List<IdentityUserLogin<string>>();
        }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [MaxLength(50)]
        public string Surname { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; }
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; }
    }
}
