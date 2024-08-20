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
            radems.CategoryID = TestTools.GetRandomNumber(1, 7);
            for (int x = 0; x < 10; x++)
            {
                radems.Scores[x] = TestTools.GetRandomNumber(0, 3);
            }
           
            Assert.AreNotEqual(0, radems.CalculatedOperationalRisk);
            Assert.AreNotEqual(0, radems.CalculatedResponseCapacity);
            Assert.AreNotEqual(string.Empty, radems.ShortCode);
        }

        [TestMethod]
        public void TestRademsFromShortCode()
        {
            var radems = new RADeMSScore();
            radems.CategoryID = TestTools.GetRandomNumber(1, 7);
            for (int x = 0; x < 10; x++)
            {
                radems.Scores[x] = TestTools.GetRandomNumber(0, 3);
            }
            string shortCode = radems.ShortCode;
            
            RADeMSScore? testConvert = RADeMSTools.GetScoreFromShortCode(shortCode);
            Assert.IsNotNull(testConvert);
            Assert.AreEqual(radems.CategoryID, testConvert.CategoryID);
            Assert.AreEqual(radems.ShortText, testConvert.ShortText);
            for(int x = 0; x < radems.Scores.Length; x++)
            {
                Assert.AreEqual(radems.Scores[x], testConvert.Scores[x]);
            }
        }


    }
}