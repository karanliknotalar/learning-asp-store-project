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
        public IAuthService AuthService { get; }
        
        public ServiceManager(RepositoryContext context, IProductService productService,
            ICategoryService categoryService, IOrderService orderService, IAuthService authService)
        {
            _context = context;
            ProductServices = productService;
            CategoryServices = categoryService;
            OrderService = orderService;
            AuthService = authService;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}