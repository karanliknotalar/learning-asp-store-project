namespace Services.Contracts
{
    public interface IServiceManager
    {
        IProductService ProductServices { get; }
        ICategoryService CategoryServices { get; }
        void Save();
    }
}