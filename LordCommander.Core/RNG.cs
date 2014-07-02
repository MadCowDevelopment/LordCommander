using System;

namespace LordCommander.Core
{
    internal static class RNG
    {
        private static readonly Random Random = new Random((int) DateTime.Now.Ticks);

        public static bool Chance(double chance)
        {
            if (chance < 0 || chance > 100)
                throw new ArgumentOutOfRangeException("chance", "Chance needs to be between 0 and 100");

            return chance != 0 && Random.NextDouble()*100 <= chance;
        }
    }
}