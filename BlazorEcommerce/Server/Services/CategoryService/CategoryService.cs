using BlazorEcommerce.Server.Data;

namespace BlazorEcommerce.Server.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<Category>> GetProductAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return new ServiceResponse<Category>
            {
                Data = category
            };
        }
    }
}
