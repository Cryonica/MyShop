using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class SalePoint
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public virtual ICollection<ProvidedProduct> ProvidedProducts { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
