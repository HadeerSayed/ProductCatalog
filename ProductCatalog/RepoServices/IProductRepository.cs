using ProductCatalog.Models;

namespace ProductCatalog.RepoServices
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAll();
        public Product GetById(int id);
        public void Create(Product P);
        public void Update(Product p);
        public void Delete(int id);
        public IEnumerable<Product> GetAllByTime();
        public IEnumerable<Product> GetByCategory(int categoryId);
    }
}
