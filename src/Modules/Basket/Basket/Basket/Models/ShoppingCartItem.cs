﻿

using Shared.DDD;

namespace Basket.Basket.Models
{
    public class ShoppingCartItem : Entity<Guid>
    {
        public Guid ShoppingCartId { get; private set; } = default!;
        public Guid ProductId { get; private set; } = default!;

        //internal set ensures quantity can only be modified within this assembly or Basket Module
        public int Quantity { get; internal set; } = default!;
        public string Color { get; private set; } = default!;
        
        
        //will come from Catalog Module with synchronous communication
        public decimal Price { get;private set; } = default!;
        public string ProductName { get; private set; } = default!;

        internal  ShoppingCartItem(Guid shoppingCartId, Guid productId, int quantity, string color, decimal price, string productName)
        {
            ShoppingCartId = shoppingCartId;
            ProductId = productId;
            Quantity = quantity;
            Color = color;
            Price = price;
            ProductName = productName;
        }
        

    }
}
