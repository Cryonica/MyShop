using Microsoft.AspNetCore.Http;
using MyShop.Helpers;
using MyShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


namespace MyShop.Services.ManageShopServices
{
    public class OrderService
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly ISession _isession;
        public OrderService(SqlDbContext context, ISession session)
        {
            _isession = session;
            _sqlDbContext = context;
           
          }   
        public void PlaceOrder(List<SaleData> items, string userName)
        {
            // Add SaleDatas
            foreach (SaleData saleData in items)
            {
                _sqlDbContext.Products.Attach(saleData.Product);
                _sqlDbContext.SaleDatas.Add(saleData);
            }
            
            
            //Create Sale
            Buyer buyer = _sqlDbContext.Buyers.Where(b => b.Email == userName).FirstOrDefault();
            SalePoint salePoint = SessionHelper.GetObjectFromJson<SalePoint>(_isession, "SalePoint");
            _sqlDbContext.SalePoints.Attach(salePoint);
            if (buyer != null) _sqlDbContext.Buyers.Attach(buyer);

            var order = new Sale
                  {
                      Buyer = buyer,
                      SaleData = items,
                      OrderDate = DateTime.Today,
                      OrderTime = DateTime.Now.TimeOfDay,
                      TotalAmount = items.Sum(p => p.Product.Price),
                      SalePoint = salePoint
                  };
            _sqlDbContext.Sales.Add(order);
            
            //Write data to DataBase
            _sqlDbContext.SaveChanges();

            // Update Provided product

            var ProvededProducts = _sqlDbContext.ProvidedProducts
                .Where(pp => pp.SalePoint == salePoint)
                .ToList();
            var updatedProvidedProducts = ProvededProducts
                .Select(pp =>
                {
                    var productInCart = items.Where(p => p.Product.Id == pp.ProductId).FirstOrDefault();
                    if (productInCart != null) pp.ProductQuantity -= productInCart.ProductQuantity;
                    return pp;
                })
                .ToList();
            _sqlDbContext.ProvidedProducts.UpdateRange(updatedProvidedProducts);
            _sqlDbContext.SaveChanges();
        }
    }
}
