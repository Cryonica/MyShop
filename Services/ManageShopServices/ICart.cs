using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Services.ManageShopServices
{
    public interface ICart
    {
        public void AddItem(Product product, int quantity);
        public void RemoveItem(Product product);
        public IEnumerable<Product> GetItems();
        public decimal TotalCost();
    }
}
