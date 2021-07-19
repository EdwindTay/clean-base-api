using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Core.Exceptions
{
    public class FriendlyException : Exception
    {
        public FriendlyException() : base()
        {
        }

        public FriendlyException(string msg) : base(msg)
        {
        }
    }
}
