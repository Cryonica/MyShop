using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Helpers;
using MyShop.Models;
using MyShop.Services.ManageShopServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        [Route("index")]
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

        [HttpPost]
        public IActionResult Buy(string id)
        {
            return RedirectToAction("Index");
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        [Route("/ControllerName/Delete")]
        public IActionResult Delete(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartLine>>(HttpContext.Session, "Cart");
            cart.Remove(cart
                    .Where(c => c.Product.Id == id)
                    .FirstOrDefault());
            SessionHelper.SetObjectAsJson(HttpContext.Session, "Cart", cart);
            if (cart.Count == 0) return RedirectToAction("Index", "OpenPoint");
            return RedirectToAction("Index");
            //return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
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
    }
}
