using MySarAssistModels.RADeMS;
using System.Drawing;

namespace MySarAssistUnitTests
{
    [TestClass]
    public class RademsUnitTests
    {
        [TestMethod]
        public void TestRademsShortCode()
        {
            var radems = new RADeMSScore();
            for (int x = 0; x < 10; x++)
            {
                radems.Scores[x] = TestTools.GetRandomNumber(0, 3);
            }
           
            Assert.AreNotEqual(0, radems.CalculatedOperationalRisk);
            Assert.AreNotEqual(0, radems.CalculatedResponseCapacity);
        }

      

    }
}