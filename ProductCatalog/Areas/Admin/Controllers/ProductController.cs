using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using ProductCatalog.Models.Context;
using ProductCatalog.RepoServices;
using System.Data;

namespace ProductCatalog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryRepository Category;

        public IProductRepository Product { get; }

        public ProductController(IProductRepository product,ICategoryRepository category)
        {
            Product = product;
            Category = category;
        }
   
        
        public ActionResult Index()
        {
            return View(Product.GetAll());
        }

        

 
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(Category.GetAll(), "Id", "Name");
            return View();
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product P )
        {
            try
            {
                Product.Create(P);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.mess = "there are an error";
                return View();
            }
        }

      
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(Category.GetAll(), "Id", "Name");
            return View(Product.GetById(id));
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product p)
        {
            try
            {
                Product.Update(p);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.mess = "there are an error";
                return View();
            }
        }

    
        public ActionResult Delete(int id)
        {
            return View(Product.GetById(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product P)
        {
            try
            {
                Product.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.mess = "there are an error";
                return View();
            }
        }
    }
}
