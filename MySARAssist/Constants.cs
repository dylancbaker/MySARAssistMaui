using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist
{
    public static class Constants
    {
       public static string dbPath = System.IO.Path.Combine(FileSystem.AppDataDirectory, "mysarassist.db3");
    }
}
