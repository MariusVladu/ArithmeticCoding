using System.Collections.Generic;

namespace ArithmeticCoding
{
    public static class ModelBuilding
    {
        public static List<int> GetCummulativeSums(List<int> counts)
        {
            var cummulativeSums = new List<int>();

            for (int i = 0; i <= counts.Count; i++)
            {
                cummulativeSums.Add(GetCummulativeSumForIndex(counts, i));
            }

            return cummulativeSums;
        }

        public static int GetCummulativeSumForIndex(List<int> counts, int index)
        {
            var cummulativeSum = 0;
            for (int i = 0; i < index; i++)
            {
                cummulativeSum += counts[i];
            }

            return cummulativeSum;
        }

        public static List<int> GetSymbolCounts(List<int> alphabet)
        {
            var counts = new List<int>();

            for (int i = 0; i < alphabet.Count; i++)
            {
                counts.Add(GetSymbolCount(i));
            }

            return counts;
        }

        public static int GetSymbolCount(int symbol)
        {
            return 1;
        }

        public static int MapSymbolToIndexInList(int symbol, List<int> list)
        {
            return list.IndexOf(symbol);
        }

        public static int GetSymbolIndexByCummulativeSum(uint cummulativeSum, List<int> sums)
        {
            var i = sums.Count - 1;
            while (cummulativeSum < sums[i])
                i--;

            return i;
        }
    }
}
