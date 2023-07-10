using ProductCatalog.Models;

namespace ProductCatalog.RepoServices
{
    public interface ICategoryRepository
    {
        public IEnumerable<Category> GetAll();
        public Category GetById(int id);
    }
}
