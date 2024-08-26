using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models.Exceptions
{
    class NotFoundException : Exception
    {
            public NotFoundException(string message) : base(message) { }

            public NotFoundException(string message, Exception inner) : base(message, inner) { }

    }
}
