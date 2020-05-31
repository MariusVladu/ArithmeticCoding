using BitReaderWriter;
using BitReaderWriter.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArithmeticCoding
{
    public class ArithmeticCoding
    {
        public readonly int numberOfSymbols;
        public readonly int endOfFileSymbol;
        public List<int> alphabet;

        private IBitReader bitReader;
        private IBitWriter bitWriter;
        private long bitsToRead;

        public uint low;
        public uint high;
        public ulong range;

        public uint code;
        public int underflowBits;

        public List<int> counts;
        public List<int> sums;
        public int totalSum;

        public ArithmeticCoding(List<int> alphabet)
        {
            this.alphabet = alphabet.OrderBy(x => x).ToList();

            endOfFileSymbol = this.alphabet.Last() + 1;
            this.alphabet.Add(endOfFileSymbol);

            numberOfSymbols = this.alphabet.Count;
        }

        public void EncodeFile(string inputFilePath, string outputFilePath)
        {
            InitializeBitReaderWriter(inputFilePath, outputFilePath);

            InitializeModel();

            var fileLengthInBytes = new FileInfo(inputFilePath).Length;
            for (int i = 0; i < fileLengthInBytes; i++)
            {
                var symbolIndex = ModelBuilding.MapSymbolToIndexInList((int)bitReader.ReadNBits(8), alphabet);

                EncodeSymbol(symbolIndex);

                UpdateModel(symbolIndex);
            }

            EncodeSymbol(ModelBuilding.MapSymbolToIndexInList(endOfFileSymbol, alphabet));

            FinishEncoding();

            bitReader.Dispose();
            bitWriter.Dispose();
        }

        public void DecodeFile(string inputFilePath, string outputFilePath)
        {
            InitializeBitReaderWriter(inputFilePath, outputFilePath);

            InitializeModel();

            InitializeCodeWithTheFirst32Bits();

            while(true)
            {
                var decodedSymbolIndex = DecodeNextSymbol();
                var decodedSymbol = alphabet[decodedSymbolIndex];

                if (decodedSymbol == endOfFileSymbol)
                    break;

                bitWriter.WriteNBits(8, (uint)decodedSymbol);

                UpdateModel(decodedSymbolIndex);
            }

            bitReader.Dispose();
            bitWriter.Dispose();
        }

        private void InitializeBitReaderWriter(string inputFilePath, string outputFilePath)
        {
            bitsToRead = new FileInfo(inputFilePath).Length * 8;

            var inputFileStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read);
            bitReader = new BitReader(inputFileStream);

            var outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);
            bitWriter = new BitWriter(outputFileStream);
        }

        public void InitializeModel()
        {
            high = UInt32.MaxValue;
            low = 0;

            counts = ModelBuilding.GetSymbolCounts(alphabet);
            sums = ModelBuilding.GetCummulativeSums(counts);

            totalSum = sums[numberOfSymbols];
            underflowBits = 0;
        }

        public void EncodeSymbol(int symbolIndex)
        {
            range = (ulong)(high - low) + 1;

            high = GetHighForSymbol(symbolIndex);
            low = GetLowForSymbol(symbolIndex);

            while(true)
            {
                if (Intervals.ValuesAreInTheSameHalf(low, high))
                {
                    var bit = low >> 31;
                    bitWriter.WriteNBits(1, bit);

                    WriteUnderflowBits(~bit);

                    high = (high << 1) + 1;
                    low = low << 1;
                }
                else if(Intervals.IsInTheSecondQuarter(low) && Intervals.IsInTheThirdQuarter(high))
                {
                    high = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(high, 1);
                    low = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(low, 0);

                    underflowBits++;
                }
                else
                {
                    return;
                }
            }
        }

        private int DecodeNextSymbol()
        {
            range = (ulong)(high - low) + 1;

            var cummulativeSum = (uint)(((code - low + 1) * (ulong)totalSum - 1) / range);
            var symbolIndex = ModelBuilding.GetSymbolIndexByCummulativeSum(cummulativeSum, sums);

            if (symbolIndex == alphabet.IndexOf(endOfFileSymbol))
                return symbolIndex;

            high = GetHighForSymbol(symbolIndex);
            low = GetLowForSymbol(symbolIndex);

            while (true)
            {
                if (Intervals.ValuesAreInTheSameHalf(low, high))
                {
                    high = (high << 1) + 1;
                    low = low << 1;
                }
                else if (Intervals.IsInTheSecondQuarter(low) && Intervals.IsInTheThirdQuarter(high))
                {
                    high = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(high, 1);
                    low = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(low, 0);

                    code ^= 0x40000000;
                }
                else
                {
                    break;
                }

                code = (code << 1) + ReadNextBit();
            }

            return symbolIndex;
        }

        public void FinishEncoding()
        {
            //underflowBits++;

            //var bit = (uint)(Intervals.IsInTheFirstHalf(low) ? 0 : 1);

            //bitWriter.WriteNBits(1, bit);
            //WriteUnderflowBits(~bit);

            uint valueToWrite;
            if (Intervals.IsInTheFirstQuarter(low) && Intervals.IsInTheThirdQuarter(high))
            {
                valueToWrite = 1;
            }
            else if (Intervals.IsInTheSecondQuarter(low) && Intervals.IsInTheForthQuarter(high))
            {
                valueToWrite = 2;
            }
            else valueToWrite = 2;

            bitWriter.WriteNBits(2, valueToWrite);
        }

        public void WriteUnderflowBits(uint bit)
        {
            while (underflowBits > 0)
            {
                bitWriter.WriteNBits(1, bit);

                underflowBits--;
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

        public void UpdateModel(int symbolIndex)
        {
            counts[symbolIndex]++;

            ModelBuilding.UpdateCummulativeSumsStartingFromIndex(sums, symbolIndex, counts);
            totalSum = sums[numberOfSymbols];
        }

        private void InitializeCodeWithTheFirst32Bits()
        {
            for (int i = 0; i < 32; i++)
            {
                code = (code << 1) + ReadNextBit();
            }
        }

        public uint ReadNextBit()
        {
            if (bitsToRead > 0)
            {
                bitsToRead--;
                return (uint)bitReader.ReadBit();
            }

            return 0;
        }
    }
}
