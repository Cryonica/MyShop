using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public virtual ICollection<Buyer> Buyers { get; set; }
    }
}
