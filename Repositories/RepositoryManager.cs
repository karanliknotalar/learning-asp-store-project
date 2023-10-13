using Repositories.Contracts;

namespace Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        public IProductRepository Product { get; }

        public ICategoryRepository Category { get; }

        public IOrderRepository Order { get; }

        public RepositoryManager(RepositoryContext context, IProductRepository productRepository,
            ICategoryRepository categoryRepository, IOrderRepository orderRepository)
        {
            _context = context;
            Product = productRepository;
            Category = categoryRepository;
            Order = orderRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}