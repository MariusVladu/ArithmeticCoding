using BitReaderWriter;
using BitReaderWriter.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArithmeticCoding
{
    public class BaseArithmeticCoding
    {
        public readonly int numberOfSymbols;
        public int endOfFileSymbol;
        public List<int> alphabet;

        public IBitReader bitReader;
        public IBitWriter bitWriter;

        public uint low;
        public uint high;
        public ulong range;

        public int underflowBits;

        public List<int> counts;
        public List<int> sums;
        public int totalSum;

        public BaseArithmeticCoding(List<int> alphabet)
        {
            this.alphabet = alphabet.OrderBy(x => x).ToList();

            endOfFileSymbol = this.alphabet.Last() + 1;
            this.alphabet.Add(endOfFileSymbol);

            numberOfSymbols = this.alphabet.Count;
        }

        public void InitializeBitReaderWriter(string inputFilePath, string outputFilePath)
        {
            var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
            bitReader = new BitReader(inputFileStream);

            var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            bitWriter = new BitWriter(outputFileStream);
        }

        public void InitializeModel()
        {
            high = UInt32.MaxValue;
            low = 0;

            counts = InitializeSymbolCounts(alphabet);
            sums = GetCummulativeSums(counts);

            totalSum = sums[numberOfSymbols];
            underflowBits = 0;
        }

        public void UpdateModel(int symbolIndex)
        {
            counts[symbolIndex]++;

            UpdateCummulativeSumsStartingFromIndex(sums, symbolIndex, counts);
            totalSum = sums[numberOfSymbols];
        }

        public List<int> InitializeSymbolCounts(List<int> alphabet)
        {
            var counts = new List<int>();

            for (int i = 0; i < alphabet.Count; i++)
            {
                counts.Add(1);
            }

            return counts;
        }

        public List<int> GetCummulativeSums(List<int> counts)
        {
            var cummulativeSums = new List<int>();

            for (int i = 0; i <= counts.Count; i++)
            {
                cummulativeSums.Add(GetCummulativeSumForIndex(counts, i));
            }

            return cummulativeSums;
        }

        public int GetCummulativeSumForIndex(List<int> counts, int index)
        {
            var cummulativeSum = 0;
            for (int i = 0; i < index; i++)
            {
                cummulativeSum += counts[i];
            }

            return cummulativeSum;
        }

        public void UpdateCummulativeSumsStartingFromIndex(List<int> sums, int startIndex, List<int> counts)
        {
            for (int i = startIndex + 1; i < sums.Count; i++)
            {
                sums[i] = sums[i - 1] + counts[i - 1];
            }
        }

        public uint GetHighForSymbol(int symbolIndex)
        {
            return (uint)(low + range * (ulong)sums[symbolIndex + 1] / (ulong)totalSum - 1);
        }

        public uint GetLowForSymbol(int symbolIndex)
        {
            return (uint)(low + range * (ulong)sums[symbolIndex] / (ulong)totalSum);
        }
    }
}
