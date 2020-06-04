using BitReaderWriter.Contracts;
using System.Collections.Generic;
using System.IO;

namespace ArithmeticCoding
{
    public class Encoder : BaseArithmeticCoding
    {
        public Encoder(List<int> alphabet) : base(alphabet)
        {
        }

        public void EncodeFile(string inputFilePath, string outputFilePath)
        {
            InitializeBitReaderWriter(inputFilePath, outputFilePath);
            InitializeModel();

            var fileLengthInBytes = new FileInfo(inputFilePath).Length;
            for (int i = 0; i < fileLengthInBytes; i++)
            {
                var symbolIndex = MapSymbolToIndexInList((int)bitReader.ReadNBits(8), alphabet);

                EncodeSymbol(symbolIndex);

                UpdateModel(symbolIndex);
            }

            EncodeSymbol(MapSymbolToIndexInList(endOfFileSymbol, alphabet));
            FinishEncoding();

            bitReader.Dispose();
            bitWriter.Dispose();
        }

        public void EncodeArray(int[] array, IBitWriter bitWriter)
        {
            this.bitWriter = bitWriter;
            InitializeModel();

            foreach(var value in array)
            {
                var symbolIndex = MapSymbolToIndexInList(value, alphabet);

                EncodeSymbol(symbolIndex);

                UpdateModel(symbolIndex);
            }

            EncodeSymbol(MapSymbolToIndexInList(endOfFileSymbol, alphabet));
            FinishEncoding();

            bitWriter.Dispose();
        }

        public void EncodeSymbol(int symbolIndex)
        {
            range = (ulong)(high - low) + 1;

            high = GetHighForSymbol(symbolIndex);
            low = GetLowForSymbol(symbolIndex);

            while (true)
            {
                if (Intervals.ValuesAreInTheSameHalf(low, high))
                {
                    var bit = low >> 31;
                    bitWriter.WriteNBits(1, bit);

                    WriteUnderflowBits(~bit);

                    high = BitwiseOperations.ShiftLeft1PositionAndFill(high, 1);
                    low = BitwiseOperations.ShiftLeft1PositionAndFill(low, 0);
                }
                else if (Intervals.IsInTheSecondQuarter(low) && Intervals.IsInTheThirdQuarter(high))
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

        public void WriteUnderflowBits(uint bit)
        {
            while (underflowBits > 0)
            {
                bitWriter.WriteNBits(1, bit);

                underflowBits--;
            }
        }

        public void FinishEncoding()
        {
            uint valueToWrite;
            if (Intervals.IsInTheFirstQuarter(low) && Intervals.IsInTheThirdQuarter(high))
            {
                valueToWrite = 1;
            }
            else if (Intervals.IsInTheSecondQuarter(low) && Intervals.IsInTheForthQuarter(high))
            {
                valueToWrite = 2;
            }
            else valueToWrite = 1;

            bitWriter.WriteNBits(1, valueToWrite >> 1);
            WriteUnderflowBits(~(valueToWrite >> 1));
            bitWriter.WriteNBits(1, valueToWrite & 1);
        }

        public int MapSymbolToIndexInList(int symbol, List<int> list)
        {
            return list.IndexOf(symbol);
        }
    }
}
