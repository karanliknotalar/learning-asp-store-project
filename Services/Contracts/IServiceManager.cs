namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductServices { get; }
        ICategoryService CategoryServices { get; }
        IOrderService OrderService { get; }
        void Save();
    }
}