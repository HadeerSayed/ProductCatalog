using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Models;
using ProductCatalog.Models.Context;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProductCatalog.RepoServices
{
    public class ProductRepoService:IProductRepository
    {

        public ProductCatalogContext Db { get; }
        public IHttpContextAccessor _httpContext { get; }

        public ProductRepoService(ProductCatalogContext db , IHttpContextAccessor httpContext)
        {
            Db = db;
            _httpContext = httpContext;
        }


        public IEnumerable<Product> GetAll()
        {
            return Db.Products.Include(p => p.Category).Include(p=>p.user).ToList();

        }

        public Product GetById(int id)
        {
            Product p = Db.Products.Where(p=>p.Id==id).Include(p => p.Category).FirstOrDefault();
            return p;

        }

        public void Create(Product P )
        {
            Product product = new Product()
            {
                Name = P.Name,
                CreationDate = DateTime.Now.Date,
                price = P.price,
                StartDate = P.StartDate,
                Duration = P.Duration,
                CategoryId = P.CategoryId,
                userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            Db.Products.Add(product);
            Db.SaveChanges();
   

        }


        public void Update(Product p)
        {
            var Product = GetById(p.Id);

            Product.price = p.price;
            Product.Duration = p.Duration;
            Product.StartDate = p.StartDate;
            Product.CreationDate = p.CreationDate;
            Product.Name = p.Name;
            Product.CategoryId = p.CategoryId;

            Db.SaveChanges();

        }


        public void Delete(int id)
        {
            var Product = GetById(id);

            Db.Products.Remove(Product);
            Db.SaveChanges();

        }

        public IEnumerable<Product> GetAllByTime()
        {

            var Products = Db.Products.Where(p => DateTime.Now.Date >= p.StartDate && DateTime.Now.Date < p.StartDate.AddDays(p.Duration)).Include(p=>p.Category).ToList();
            return Products;
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            var products = GetAllByTime();

            return products.Where(p=>p.CategoryId== categoryId);
        }
    }
}
