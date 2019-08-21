using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class NavController : Controller
    {
        private readonly IProductRepository repos;

        // GET: Nav
        public NavController(IProductRepository repos)
        {
            this.repos = repos;
        }
        public  PartialViewResult Menu(string category=null)
        {
            @ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repos.Products
                   .Select(x => x.Category)
                   .Distinct().OrderBy(x => x);
                   
            return PartialView(categories);
        }
    }
}