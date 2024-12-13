using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Messaging.Events
{
    public record ProductPriceChangedIntegrationEvent : IntegrationEvent
    {
        public Guid ProductId { get; set; } = default!;
        public string Name { get; set;} = default!;

        public List<string> Category { get; set; } = default!;
        public string Description { get; set;}= default!;

        public string ImageFile { get; set;} = default!;

        public decimal Price { get; set; } = default!;

    }
}
