using MyShop.Models;
using System.Collections.Generic;
using System.Linq;


namespace MyShop.Services.ManageShopServices
{
    public class Cart: ICart
    {
        private readonly List<CartLine> lineCollection;
        
        public void AddItem(Product product, int quantity)
        {
            
            
            CartLine line = lineCollection
                .Where(p => p.Product.Id == product.Id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveItem(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.Id == product.Id);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
        
        
        public IEnumerable<Product> GetItems()
        {
            return lineCollection
                .Select(a => a.Product)
                .ToList();
        }
        public decimal TotalCost()
        {
            return lineCollection
                .Sum(lc => lc.Product.Price * lc.Quantity);
                    
        }
        public class CartLine
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}
