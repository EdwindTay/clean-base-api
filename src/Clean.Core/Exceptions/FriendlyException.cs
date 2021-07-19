using System;

namespace Clean.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base()
        {
        }

        public NotFoundException(string msg) : base(msg)
        {
        }
    }
}
