using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;

        public RepositoryManager(IProductRepository productRepository, RepositoryContext context,
            ICategoryRepository categoryRepository)
        {
            Product = productRepository;
            _context = context;
            Category = categoryRepository;
        }

        public IProductRepository Product { get; }

        public ICategoryRepository Category { get; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}