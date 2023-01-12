using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Helpers;
using MyShop.Models;
using MyShop.Services.ManageShopServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        
        [HttpGet("Index")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult Index()
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "Cart");
            if (cart == null)
            {
                return Redirect("~/SailPoints/SPList");
            }

            ViewBag.CartProduct = cart.ToArray();
            ViewBag.Total = cart
                .Sum(lc => lc.Product.Price * lc.Quantity);
            return View();
        }

        //[Route("buy")]
        [HttpGet("buy/{id}")]
        public IActionResult Buy(string id)
        {
            return RedirectToAction("Index");
        }

        // GET: CartController/Create
        protected ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [Route("OnlyForAPI_NotWorked")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: CartController/Edit/5
        protected ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "Cart");
            if (cart != null)
            {
                cart.Remove(cart
                    .Where(c => c.Product.Id == id)
                    .FirstOrDefault());
                SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
                if (cart.Count == 0) return RedirectToAction("Index", "OpenPoint");
                return RedirectToAction("Index");
            }
            else
            {
                return StatusCode(404);
            }
            
           
        }

        
    }
}
