using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Products.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id):base("Product",id)
        {
            
        }

    }
}
