using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistUnitTests
{
    internal static class TestTools
    {

        public static int GetRandomNumber(int lower, int upper)
        {
            Random rnd = new Random();
            return rnd.Next(lower, upper);
        }
    }
}
