using BitReaderWriter.Contracts;
using System.Collections.Generic;
using System.IO;

namespace ArithmeticCoding
{
    public class Decoder : BaseArithmeticCoding
    {
        public long bitsToRead;
        public uint code;

        public Decoder(List<int> alphabet) : base(alphabet)
        {
        }

        public void DecodeFile(string inputFilePath, string outputFilePath)
        {
            bitsToRead = new FileInfo(inputFilePath).Length * 8;

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

        public int[] DecodeToArray(long bitsToRead, IBitReader bitReader)
        {
            this.bitsToRead = bitsToRead;
            this.bitReader = bitReader;
            InitializeModel();

            InitializeCodeWithTheFirst32Bits();
            var values = new List<int>();

            while (true)
            {
                var decodedSymbolIndex = DecodeNextSymbol();
                var decodedSymbol = alphabet[decodedSymbolIndex];

                if (decodedSymbol == endOfFileSymbol)
                    break;

                values.Add(decodedSymbol);

                UpdateModel(decodedSymbolIndex);
            }

            bitReader.Dispose();

            return values.ToArray();
        }

        private int DecodeNextSymbol()
        {
            range = (ulong)(high - low) + 1;

            var cummulativeSum = (uint)(((code - low + 1) * (ulong)totalSum - 1) / range);
            var symbolIndex = GetSymbolIndexByCummulativeSum(cummulativeSum, sums);

            high = GetHighForSymbol(symbolIndex);
            low = GetLowForSymbol(symbolIndex);

            while (true)
            {
                if (Intervals.ValuesAreInTheSameHalf(low, high))
                {
                    high = BitwiseOperations.ShiftLeft1PositionAndFill(high, 1);
                    low = BitwiseOperations.ShiftLeft1PositionAndFill(low, 0);
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

                code = BitwiseOperations.ShiftLeft1PositionAndFill(code, (byte)ReadNextBit());
            }

            return symbolIndex;
        }

        public int GetSymbolIndexByCummulativeSum(uint cummulativeSum, List<int> sums)
        {
            var i = sums.Count - 1;
            while (cummulativeSum < sums[i])
                i--;

            return i;
        }

        public void InitializeCodeWithTheFirst32Bits()
        {
            for (int i = 0; i < 32; i++)
            {
                code = (code << 1) + ReadNextBit();
            }
        }

        public uint ReadNextBit()
        {
            if (bitsToRead-- > 0)
            {
                return (uint)bitReader.ReadBit();
            }

            return 0;
        }
    }
}
