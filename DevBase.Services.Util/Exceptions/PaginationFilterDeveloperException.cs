using System;

namespace DevBase.Services.Util.Exceptions
{
    public class PaginationFilterDeveloperException : Exception
    {
        public PaginationFilterDeveloperException()
        {

        }

        public PaginationFilterDeveloperException(string message) : base(message)
        {

        }

        public PaginationFilterDeveloperException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
