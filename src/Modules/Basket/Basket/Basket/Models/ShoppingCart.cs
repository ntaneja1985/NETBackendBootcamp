
using Shared.DDD;

namespace Basket.Basket.Models
{
    public class ShoppingCart : Aggregate<Guid>
    {
     //private set means it can only be set within the entity.
        public string UserName { get; private set; } = default!;
        private readonly List<ShoppingCartItem> _items = new();
        public IReadOnlyList<ShoppingCartItem> Items => _items.AsReadOnly();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
}
