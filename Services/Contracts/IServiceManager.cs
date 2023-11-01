namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductServices { get; }
        ICategoryService CategoryServices { get; }
        IOrderService OrderService { get; }
        IAuthService AuthService { get; }
        void Save();
    }
}