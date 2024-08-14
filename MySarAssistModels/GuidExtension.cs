using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistModels
{
    public static class GuidExtension
    {
        public static object ValueOrDBNull(this Guid g)
        {
            if (g == Guid.Empty) { return DBNull.Value; }
            else { return g; }
        }
    }
}
