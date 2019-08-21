using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository repos;

        public AdminController(IProductRepository repos)
        {
            this.repos = repos;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View(repos.Products);
        }
        [HttpGet]
        public ActionResult Edit(int ProductId)
        {
            Product product = repos.Products.FirstOrDefault(p =>p.ProductId == ProductId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit( Product product)
        {
            if (ModelState.IsValid)
            {
                repos.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else { return View(product); }
           
        }

        public ActionResult Create()
        {
            
            return View(new Product());
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repos.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else { return View(product); }
     

        }

        public ActionResult Delete(int ProductId)
        {

            Product pro = repos.DeleteProduct(ProductId);
            if (pro != null)
            {
                TempData["Message"] = string.Format("{0} has been Deleted", pro.Name);
                
            }
            return RedirectToAction("Index");

        }
    }
}