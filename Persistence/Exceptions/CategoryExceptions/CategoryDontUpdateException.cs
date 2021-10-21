using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Exceptions.CategoryExceptions
{
    public class CategoryDontUpdateException : Exception
    {
        public CategoryDontUpdateException() : base() { }

        public CategoryDontUpdateException(String message) : base(message) { }
    }
}
