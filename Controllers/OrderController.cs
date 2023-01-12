using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Helpers;
using MyShop.Models;
using MyShop.Services;
using MyShop.Services.ManageShopServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly SqlDbContext _sqlDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public OrderController(SqlDbContext sqlDbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _sqlDbContext = sqlDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET: OrderController
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "Cart");//берем записи из корзины
            SalePoint salePoint = SessionHelper.GetObjectFromJson<SalePoint>(HttpContext.Session, "SalePoint");
            if (cart == null) return Redirect("~/OpenPoint"); //если из нет то корзина пустая

            // Check if ProductQuantity greater than availableProduct Quantity and set min if greater
            var providedProducts = _sqlDbContext.ProvidedProducts
                    .Include(p => p.Product)
                    .Where(p => p.SalePointId == salePoint.Id)
                    .ToArray();
            foreach (var cartitem in cart)
            {
                var availableProduct = providedProducts.Where(p => p.ProductId == cartitem.Product.Id).FirstOrDefault();
                if (availableProduct != null)
                {
                    cartitem.Quantity = Math.Min(cartitem.Quantity, availableProduct.ProductQuantity);
                }
            }
            List<SaleData> saleDatas = new();
            cart.ForEach(lp =>
            {
                var saledata = new SaleData
                {
                    Product = lp.Product,
                    ProductQuantity = lp.Quantity,
                    ProductIdAmount = lp.Quantity * lp.Product.Price,
                    
                };
                saleDatas.Add(saledata);
            });
            ViewBag.SaleData = saleDatas;
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Order", saleDatas);
            
            return View();
        }
        [HttpPost]
        [Route("MakeOrder")]
        public IActionResult MakeOrder(string Name, string Address, string City, string State, string ZipCode, string PhoneNumber)
        {
            HttpContext.Request.EnableBuffering();

            var saleData= SessionHelper.GetObjectFromJson<List<SaleData>>(HttpContext.Session, "Order");//берем записи из корзины
            if (saleData == null) return Redirect("~/OpenPoint"); //если из нет то корзина пустая
            
            OrderService orderService = new OrderService(_sqlDbContext, HttpContext.Session);

            orderService.PlaceOrder(saleData, User.Identity.Name);
            
            HttpContext.Session.Remove("Order");
            HttpContext.Session.Remove("Cart");
            var response = HttpContext.Features.Get<IHttpResponseFeature>();
            response.Headers.Append("Cache-Control", "no-cache, no-store");
            response.Headers.Append("Expires", "0");

            return Redirect("/SailPoints/SPList");
        }

        // GET: OrderController/Details/5
        [HttpGet]
        [Route("Details {id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost("Create_OnlyForAPI_NotWorked")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return StatusCode(202);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpGet]
        [Route("Edit_OnlyForAPI_NotWorked/{id}")]
        public ActionResult Edit(int id)
        {
            return StatusCode(202);
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [Route("Edit{id}/OnlyForAPI_NotWorked_Link")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return StatusCode(202);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpDelete]
        [Route("Delete{id}/OnlyForAPI_NotWorked")]
        public ActionResult Delete(int id)
        {
            return StatusCode(202);
        }

        // POST: OrderController/Delete/5
        [HttpDelete]
        [Route("Delete{id}/OnlyForAPI_NotWorked_link")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return StatusCode(202);
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}
