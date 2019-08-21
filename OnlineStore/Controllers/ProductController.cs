using OnlineStore.Domain.Abstract;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository repos;
        public ProductController(IProductRepository repo)
        {
            this.repos = repo;
           
        }
        // GET: Employee
        public ViewResult List(string category, int page =1)
        {
            int PageSize = 3;
            ProductListViewModel model = new ProductListViewModel() {
                Products = repos.Products.Where(p => p.Category == category || category == null)
                .OrderBy(p => p.ProductId)
                          .Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    // TotalItems = repos.Products.Count(),
                    TotalItems = (category == null) ?
                  repos.Products.Count() : repos.Products.Where(c => c.Category==category).Count(),
                  CurrentPage = page,
                  ItemsPerPage = PageSize,
             

              },
              CurrentCategory=category


            };
                
                
             
            

            return View(model);

            //int PageSize = 2;
            //return View(repos.Products.OrderBy(p => p.ProductId)
            //    .Skip((page-1)* PageSize)
            //    .Take(PageSize));

        }
    }
}