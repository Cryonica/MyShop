using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MyShop.Models;
using MyShop.Helpers;
using MyShop.Services.ManageShopServices;


namespace MyShop.Controllers
{
    [Route("OpenPoint")]
    public class OpenPointController : Controller
    {
        private readonly SqlDbContext _sqlDbContext;
        public OpenPointController(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }

        // GET: OpenPointController
        [HttpGet("Index")]
        public IActionResult Index()
        {
            var salePoint = SessionHelper.GetObjectFromJson<SalePoint>(HttpContext.Session, "SalePoint");
            if (salePoint == null) return Redirect ("~/SailPoints/SPList");
            try
            {
                var providedProducts = _sqlDbContext.ProvidedProducts
                    .Include(p => p.Product)
                    .Where(p => p.SalePointId == salePoint.Id)
                    .ToArray();

                var seq = Enumerable.Range(1, 10).ToList();
                ViewBag.ProvidedProducts = providedProducts;
                ViewBag.Seq = seq;
                return View();
            }
            catch
            {
                return NotFound();
            }
        }

        // POST: OpenPointController/Delete/5
        [HttpPost]
        [Route("OnlyForAPI_NotWorked")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(int Id, int Seq)
        {
            
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "Cart");
            if (cart == null)
            {
                cart = new();
            }
            Product product = _sqlDbContext.Products
            .Where(p => p.Id == Id)
            .FirstOrDefault();
            if (product != null)
            {
               var ProductInCart= cart
                    .Where(c => c.Product.Id == Id)
                    .FirstOrDefault();
                if (ProductInCart != null)
                {
                    ProductInCart.Quantity = ProductInCart.Quantity + Seq;
                }
                else
                {
                    CartLine cartLine = new CartLine
                    {
                        Product = product,
                        Quantity = Seq
                    };
                    cart.Add(cartLine);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            }
            return RedirectToAction("Index");
           
        }
    }
}
