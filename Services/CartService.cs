using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blazored.LocalStorage;


namespace ClientApp.Services
{
    public class CartService
    {

        // properties
        private readonly ILocalStorageService _localStorage;
        public event Action? OnChange;

        private List<int> cart = new List<int>();  // holds the id:s

        // Constructor injecting localStorage
        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task InitializeCartAsync()
        {
            cart = await _localStorage.GetItemAsync<List<int>>("cart") ?? new List<int>();
            NotifyStateChanged();
        }

        private int cartCount => cart.Count;  // counting products in cart

        // method for retrieving products in cart
        public int GetCartCount() => cartCount;

        // Method for getting all id:s in cart
        public List<int> GetCartItems()
        {
            return new List<int>(cart);
        }

        // method for adding product id:s to cart
        public async Task AddToCart(int productId)
        {
            if (!cart.Contains(productId))
            {
                cart.Add(productId);
                await _localStorage.SetItemAsync("cart", cart);  // Updating localStorage
                NotifyStateChanged();
            }
        }

        // Method for removing prducts from cart
        public async Task RemoveFromCart(int productId)
        {
            if (cart.Contains(productId))
            {
                cart.Remove(productId);
                await _localStorage.SetItemAsync("cart", cart);
                NotifyStateChanged();
            }
        }

        // Method for clearing cart
        public async Task ClearCart()
        {
            cart.Clear();
            await _localStorage.RemoveItemAsync("cart");  // Clear localStorage
            NotifyStateChanged();
        }

        // tells components cart has changed
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}




/*
namespace ClientApp.Services
{
    public class CartService
    {
        public event Action? OnChange;
        private int cartCount = 0;

        public int GetCartCount() => cartCount;

        public void AddToCart()
        {
            cartCount++;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
*/