using OnlineStore.Domain.Abstract;
using OnlineStore.Domain.Entities;
using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository repo;
        private readonly IOrderProcessor orderProcessor;
        public CartController(IProductRepository repos, IOrderProcessor OrderProcessor)
        {
            this.repo = repos;
            this.orderProcessor = OrderProcessor;
        }
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel() { Cart = cart, ReturnUrl = returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
       
        public ViewResult Checkout(Cart cart , ShippingDetails shippingDetails)
        {
           
            if (cart.Lines.Count()==0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else { return View(shippingDetails); }


        }
        // GET: Cart
        public RedirectToRouteResult AddToCart(Cart cart,int ProductId, string returnUrl)
        {
            Product product = repo.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if(product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index",new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart,int ProductId, string returnUrl)
        {
            Product product = repo.Products.FirstOrDefault(p => p.ProductId == ProductId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        //private Cart GetCart()
        //{
        //    Cart cart = (Cart)Session["Cart"];
        //    if (cart==null)
        //    {
        //       cart = new Cart();
        //        Session["Cart"] = cart;
        //    }
        //    return cart;
        //}
    }
}