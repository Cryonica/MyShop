using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public virtual ICollection<ProvidedProduct> ProvidedProducts { get; set; }
        public virtual ICollection<SaleData> SaleData { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}