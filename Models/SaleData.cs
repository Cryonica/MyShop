using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class SaleData
    {
        [Key]
        public int Id { get; set; }
        public int ProductQuantity { get; set; }
        public decimal ProductIdAmount { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}