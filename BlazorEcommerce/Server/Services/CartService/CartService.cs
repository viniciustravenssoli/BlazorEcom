using BlazorEcommerce.Server.Data;

namespace BlazorEcommerce.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CartProductReponse>>> GetCardProducts(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartProductReponse>>
            {
                Data = new List<CartProductReponse>()
            };

            foreach (var cartItem in cartItems) 
            {
                var product = await _context.Products
                    .Where(p => p.Id == cartItem.ProductId)
                    .FirstOrDefaultAsync();

                if (product is null)
                {
                    continue;
                }

                var productVariant = await _context.ProductVariants
                    .Where(v => v.ProductId == cartItem.ProductId
                        && v.ProductTypeId == cartItem.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();

                if (productVariant is null)
                {
                    continue;
                }

                var cartProduct = new CartProductReponse
                {
                    ProductId = cartItem.ProductId,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = cartItem.Quantity,
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
    }
}
