using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
