using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.ProductExceptions
{
    public class ProductDontAddException : Exception
    {
        public ProductDontAddException() : base() { }

        public ProductDontAddException(String message) : base(message) { }
    }
}
