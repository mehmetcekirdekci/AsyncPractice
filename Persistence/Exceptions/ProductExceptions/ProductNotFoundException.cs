using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.ProductExceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException() : base() { }

        public ProductNotFoundException(String message) : base(message) { }
    }
}
