using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MyShop.Models
{
    [DataContract(IsReference = true)]
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int? BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public int SalePointId { get; set; }
        public SalePoint SalePoint { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<SaleData> SaleData { get; set; }
    }
}