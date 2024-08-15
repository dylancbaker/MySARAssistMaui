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

        [TestMethod]
        public void TestRademsColor()
        {
            RADeMSScore rademsScore = new RADeMSScore();
            for (int x = 0; x < 10; x++)
            {
                rademsScore.Scores[x] = TestTools.GetRandomNumber(0, 3);
            }
            float position = (float)(rademsScore.OperationalRisk * rademsScore.ResponseCapacity) / 100;
            System.Drawing.Color sdColor = RADeMSTools.GetColorOnGradient(position);

            Assert.IsNotNull(sdColor);

            //return new Color(sdColor.R, sdColor.G, sdColor.B);

        }

    }
}