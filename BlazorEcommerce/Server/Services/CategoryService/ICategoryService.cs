namespace BlazorEcommerce.Server.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();

        Task<ServiceResponse<Category>> GetProductAsync(int id);
    }
}
