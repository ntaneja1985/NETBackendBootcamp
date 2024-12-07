

namespace Catalog.Products.Models
{
    public class Product : Aggregate<Guid>
    {
        //make property setters private to enforce encapsulation
        public string Name { get; private set; } = default!;
        public List<string> Category { get; private set; } = new();
        public string Description { get; private set; } = default!;
        public string ImageFile { get; private set; } = default!;
        public decimal Price { get; private set; }

        //Add create method for initializing product entities.
        public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
        {
            //Validate inputs
            //Ensures product entity is always created in a valid state
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);
            var product = new Product()
            {
                Id = id,
                Name = name,
                Category = category,
                Description = description,
                ImageFile = imageFile,
                Price = price
            };
            product.AddDomainEvent(new ProductCreatedEvent(product));
            return product; 
        }

       
        
        //Add an update method for modifying Product entities.
        public void Update(string name, List<string> category, string description, string imageFile, decimal price)
        {
            //Validate inputs
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            Name = name;
            Category = category;
            Description = description;
            ImageFile = imageFile;
           


            //if price is changed, raise ProductPriceChanged domain event
            if(Price != price)
            {  
                Price = price;
                AddDomainEvent(new ProductPriceChangedEvent(this));
            }
            
        }
    }
}
