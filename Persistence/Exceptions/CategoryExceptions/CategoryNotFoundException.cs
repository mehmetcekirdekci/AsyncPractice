using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.CategoryExceptions
{
    public class CategoryNotFoundException : Exception
    {
        public CategoryNotFoundException() : base() { }

        public CategoryNotFoundException(String message) : base(message) { }
    }
}
