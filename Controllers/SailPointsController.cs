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
        
        [HttpGet]
        [Route("[controller]/OpenPoint/{id}")]
        public ActionResult OpenPoint(int Id)
        {
            if (Id >0)
            {
                SalePoint salePoint = _sqlDbContext.SalePoints
                    .Where(sp => sp.Id == Id)
                    .FirstOrDefault();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "SalePoint", salePoint);

                return RedirectToAction("Index", "OpenPoint");
            }
            else
            {
                return StatusCode(409);
            }
            
        }
        [HttpGet]
        //[Route("SPList")]
        public ActionResult SPList()
        {

            var SP= _sqlDbContext.SalePoints.ToList();
            ViewBag.SailPoints = SP;
                return View();
        }

        [HttpGet("[controller]/Details/{id}")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet("[controller]/Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SailPointsController/Create
        [HttpPost]
        [Route("[controller]/OnlyForWebAPI_NotWorked_Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpPost]
        [Route("[controller]/OnlyForAPI_NotWorked_Edit/{id}")]
        public ActionResult Edit(int id)
        {
            return StatusCode(200);
        }

        // POST: SailPointsController/Edit/5
        [HttpPost]
        [Route("[controller]/OnlyForAPI_NotWorked_Edit_Link/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [HttpDelete]
        [Route("[controller]/OnlyForAPI_NotWorked_Delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SailPointsController/Delete/5
        [HttpDelete]
        [Route("[controller]/OnlyForAPI_NotWorked_Delete_Link/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
}
