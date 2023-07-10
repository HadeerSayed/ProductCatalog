using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Models;
using ProductCatalog.RepoServices;
using System.Diagnostics;

namespace ProductCatalog.Controllers
{
    public class HomeController : Controller
    {
        public IProductRepository Product { get; }
        public ICategoryRepository Category { get; }

        public HomeController(IProductRepository product,ICategoryRepository category)
        {
            Product = product;
            Category = category;
        }

  
        public ActionResult Index()
        {
            ViewBag.Categories = new SelectList(Category.GetAll(), "Id", "Name");
            return View(Product.GetAllByTime());
        }

        [HttpPost]
        public ActionResult Index(int CategoryID)
        {
            ViewBag.Categories = new SelectList(Category.GetAll(), "Id", "Name");
            return View(Product.GetByCategory(CategoryID));

        }


        public ActionResult Details(int id)
        {
            return View(Product.GetById(id));
        }
    }
}