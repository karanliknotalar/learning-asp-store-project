using Repositories;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly RepositoryContext _context;
        public IProductService ProductServices { get; }
        public ICategoryService CategoryServices { get; }

        public ServiceManager(RepositoryContext context, IProductService productService,
            ICategoryService categoryService)
        {
            _context = context;
            ProductServices = productService;
            CategoryServices = categoryService;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}