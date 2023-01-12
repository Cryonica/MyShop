using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Helpers;
using MyShop.Models;
using MyShop.Services;
using System.Linq;


namespace MyShop.Controllers
{
    
    public class SailPointsController : Controller
    {
        // GET: SailPointsController
        private readonly SqlDbContext _sqlDbContext;
        public SailPointsController(SqlDbContext sqlDbContext)
        {
            _sqlDbContext = sqlDbContext;
        }
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public ActionResult OpenPoint(int Id)
        {
            using(_sqlDbContext)
            {
                SalePoint salePoint = _sqlDbContext.SalePoints
                    .Where(sp => sp.Id == Id)
                    .FirstOrDefault();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "SalePoint", salePoint);
            }
            
            return RedirectToAction("Index", "OpenPoint");
        }

        public ActionResult SPList()
        {

            var SP= _sqlDbContext.SalePoints.ToList();
            ViewBag.SailPoints = SP;
                return View();
        }

        // GET: SailPointsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SailPointsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SailPointsController/Create
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

        // GET: SailPointsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SailPointsController/Edit/5
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

        // GET: SailPointsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SailPointsController/Delete/5
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
