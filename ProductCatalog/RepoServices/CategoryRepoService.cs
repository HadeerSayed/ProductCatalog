using ProductCatalog.Models;
using ProductCatalog.Models.Context;

namespace ProductCatalog.RepoServices
{
    public class CategoryRepoService:ICategoryRepository
    {
        public ProductCatalogContext Db { get; }

        public CategoryRepoService(ProductCatalogContext db)
        {
            Db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return Db.Categories.ToList();

        }

        public Category GetById(int id)
        {
            Category c = Db.Categories.Find(id);
            return c;

        }
    }
}
