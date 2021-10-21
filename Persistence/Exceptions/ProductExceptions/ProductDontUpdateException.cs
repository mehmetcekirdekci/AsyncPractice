using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.ProductExceptions
{
    public class ProductDontUpdateException : Exception
    {
        public ProductDontUpdateException() : base() { }

        public ProductDontUpdateException(String message) : base(message) { }
    }
}
