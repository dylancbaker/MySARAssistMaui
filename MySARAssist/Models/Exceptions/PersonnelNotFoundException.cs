using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models.Exceptions
{
    public class PersonnelNotFoundException : Exception
    {
        public PersonnelNotFoundException(string message) : base(message) { }

        public PersonnelNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
