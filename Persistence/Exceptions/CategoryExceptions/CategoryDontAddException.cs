using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.CategoryExceptions
{
    public class CategoryDontAddException : Exception
    {
        public CategoryDontAddException() : base() { }

        public CategoryDontAddException(String message) : base(message) { }
    }
}
