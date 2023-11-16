﻿using BlazorEcommerce.Shared;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set;} = new List<Product>();
        public string Message { get; set; } = "Loading products";

        public int CurrentPage { get; set; } = 1;

        public int PageCount { get; set; }

        public string LastSearchText { get; set; } = string.Empty;

        public event Action ProductsChanged;

        public async Task<ServiceResponse<Product>> GetProduct(int id)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{id}");

            return result;
        }

        public async Task GetProducts(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                // se categoryUrl for nulo manda um requisiçao para obter toda a lista de produtos
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured") :
                // se categoryUrl nao for nulo manda uma requisao para obter lista de produtos pela categoria
                await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/category/{categoryUrl}");

            if(result != null && result.Data != null)
                Products = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
            {
                Message = "No products found";
            }

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchBySuggestions(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");

            return result.Data;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResult>>($"api/product/search/{searchText}/{page}");

            if (result is not null && result.Data is not null)
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;

            if (Products.Count == 0) Message = "No Products Found.";
                ProductsChanged?.Invoke();
            
        }
    }
}
