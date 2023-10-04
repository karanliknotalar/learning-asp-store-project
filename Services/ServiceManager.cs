using Repositories;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly RepositoryContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ServiceManager(RepositoryContext context, IProductService productService, ICategoryService categoryService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }

        public IProductService ProductServices => _productService;

        public ICategoryService CategoryServices => _categoryService;

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}