using Repositories;
using Services.Contracts;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly RepositoryContext _context;
        public IProductService ProductServices { get; }
        public ICategoryService CategoryServices { get; }
        public IOrderService OrderService { get; }

        public ServiceManager(RepositoryContext context, IProductService productService,
            ICategoryService categoryService, IOrderService orderService)
        {
            _context = context;
            ProductServices = productService;
            CategoryServices = categoryService;
            OrderService = orderService;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}