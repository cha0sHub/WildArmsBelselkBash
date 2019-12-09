using System;

namespace WildArmsRandomizer.Helper
{
    internal static class RandomFunctions
    {
        public static double GetNextRandomGaussian(Random rng, double mean, double standardDeviation)
        {
            double u1 = 1.0 - rng.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rng.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + standardDeviation * randStdNormal; //random normal(mean,stdDev^2) 
            //don't deal with negative values for now
            if (randNormal < 0)
                return 0;
            return randNormal;
        }
        public static uint GenerateGaussianInt(Random rng, int mean, double standardDeviation)
        {
            return (uint)Math.Floor(GetNextRandomGaussian(rng, mean, standardDeviation));
        }
        public static ushort GenerateGaussianShort(Random rng, ushort mean, double standardDeviation, ushort maxValue = ushort.MaxValue)
        {
            return (ushort)Math.Min(Math.Round(GetNextRandomGaussian(rng, mean, standardDeviation)), maxValue);
        }
        public static byte GenerateGaussianByte(Random rng, byte mean, double standardDeviation, byte maxValue = byte.MaxValue)
        {
            return (byte)Math.Min(Math.Round(GetNextRandomGaussian(rng, mean, standardDeviation)), maxValue);
        }
    }
}
