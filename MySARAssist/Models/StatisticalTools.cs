using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySARAssist.Models
{
    static class StatisticalTools
    {
        public static double calculateSpacing(double rd, double correctiveFactor, double idealPOD)
        {
            if (idealPOD > 0)
            {
                //double effectiveSweepWidth = rd * correctiveFactor;
                double spacing = rd * 5;
                //double calc = calculatePOD(rd, correctiveFactor, spacing);
                while (calculatePOD(rd, correctiveFactor, spacing) < idealPOD && spacing > 0)
                {
                    //calc = calculatePOD(rd, correctiveFactor, spacing);
                    spacing -= 1;
                }

                return spacing;
            }
            else
            {
                return 0;
            }
        }

        public static double calculatePOD(double rd, double correctiveFactor, double spacing)
        {
            double effectiveSweepWidth = rd * correctiveFactor;
            if (spacing > 0)
            {
                double coverage = Math.Round(effectiveSweepWidth / spacing, 2);
                if (coverage <= 0) { return 0; }
                if (coverage <= 0.05) { return 0.048771; }
                if (coverage <= 0.1) { return 0.095163; }
                if (coverage <= 0.15) { return 0.139292; }
                if (coverage <= 0.2) { return 0.181269; }
                if (coverage <= 0.25) { return 0.221199; }
                if (coverage <= 0.3) { return 0.259182; }
                if (coverage <= 0.35) { return 0.295312; }
                if (coverage <= 0.4) { return 0.32968; }
                if (coverage <= 0.45) { return 0.362372; }
                if (coverage <= 0.5) { return 0.393469; }
                if (coverage <= 0.55) { return 0.42305; }
                if (coverage <= 0.6) { return 0.451188; }
                if (coverage <= 0.65) { return 0.477954; }
                if (coverage <= 0.7) { return 0.503415; }
                if (coverage <= 0.75) { return 0.527633; }
                if (coverage <= 0.8) { return 0.550671; }
                if (coverage <= 0.85) { return 0.572585; }
                if (coverage <= 0.9) { return 0.59343; }
                if (coverage <= 0.95) { return 0.613259; }
                if (coverage <= 1) { return 0.632121; }
                if (coverage <= 1.05) { return 0.650062; }
                if (coverage <= 1.1) { return 0.667129; }
                if (coverage <= 1.15) { return 0.683363; }
                if (coverage <= 1.2) { return 0.698806; }
                if (coverage <= 1.25) { return 0.713495; }
                if (coverage <= 1.3) { return 0.727468; }
                if (coverage <= 1.35) { return 0.74076; }
                if (coverage <= 1.4) { return 0.753403; }
                if (coverage <= 1.45) { return 0.76543; }
                if (coverage <= 1.5) { return 0.77687; }
                if (coverage <= 1.55) { return 0.787752; }
                if (coverage <= 1.6) { return 0.798103; }
                if (coverage <= 1.65) { return 0.80795; }
                if (coverage <= 1.7) { return 0.817316; }
                if (coverage <= 1.75) { return 0.826226; }
                if (coverage <= 1.8) { return 0.834701; }
                if (coverage <= 1.85) { return 0.842763; }
                if (coverage <= 1.9) { return 0.850431; }
                if (coverage <= 1.95) { return 0.857726; }
                if (coverage <= 2) { return 0.864665; }
                else if (coverage > 2) { return 0.864665; }
                else { return 0; }
            }
            else { return 0; }
        }
    }
}
