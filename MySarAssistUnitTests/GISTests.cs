using MySarAssistModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySarAssistUnitTests
{
    [TestClass]

    public class GISTests
    {
        [TestMethod]
        public void TestUTMConvertToShort()
        {
            double lat = 49.944868;
            double lng = -125.195953;
            Coordinate coord = new Coordinate(lat, lng);
            string utm = coord.UTM;
            string shortUtm = coord.ShortUTM;

            Assert.AreEqual("424 348", shortUtm);
        }

       
    }
}
