namespace BlazorEcommerce.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductReponse>>> GetCardProducts(List<CartItem> cartItems);
    }
}
