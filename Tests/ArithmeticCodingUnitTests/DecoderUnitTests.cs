using ArithmeticCoding;
using BitReaderWriter.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class DecoderUnitTests
    {
        private Decoder decoder;
        private Mock<IBitReader> bitReaderMock;
        private Mock<IBitWriter> bitWriterMock;

        private List<int> alphabet = new List<int> { 2, 4, 7, 10 };

        [TestInitialize]
        public void Setup()
        {
            bitReaderMock = new Mock<IBitReader>();
            bitWriterMock = new Mock<IBitWriter>();

            decoder = new Decoder(alphabet)
            {
                bitReader = bitReaderMock.Object,
                bitWriter = bitWriterMock.Object
            };
        }

        [TestMethod]
        public void TestThatReadNextBitCallsBitReaderReadBitOnce()
        {
            decoder.bitsToRead = 5;

            decoder.ReadNextBit();

            bitReaderMock.Verify(x => x.ReadBit(), Times.Once);
        }

        [TestMethod]
        public void TestThatWhenBitsToReadIs0ReadNextBitDoesNotCallBitReaderReadBit()
        {
            decoder.bitsToRead = 0;

            decoder.ReadNextBit();

            bitReaderMock.Verify(x => x.ReadBit(), Times.Never);
        }

        [TestMethod]
        public void TestThatWhenBitsToReadIs0ReadNextBitReturns0()
        {
            decoder.bitsToRead = 0;

            var returnedBit = decoder.ReadNextBit();

            Assert.AreEqual((uint)0, returnedBit);
        }

        [TestMethod]
        public void TestThatWhenBitsToReadIsGreaterThan31InitializeCodeWithTheFirst32BitsCallsBitReaderReadBit32Times()
        {
            decoder.bitsToRead = 50;

            decoder.InitializeCodeWithTheFirst32Bits();

            bitReaderMock.Verify(x => x.ReadBit(), Times.Exactly(32));
        }

        [TestMethod]
        public void TestThatWhenBitsToReadIsLessThan32InitializeCodeWithTheFirst32BitsCallsBitReaderReadBitExpectedNumberOfTimes()
        {
            var expectedNumberOfCalls = 13; 
            decoder.bitsToRead = expectedNumberOfCalls;

            decoder.InitializeCodeWithTheFirst32Bits();

            bitReaderMock.Verify(x => x.ReadBit(), Times.Exactly(expectedNumberOfCalls));
        }

        [TestMethod]
        public void TestThatWhenBitsToReadIsLessThan32InitializeCodeWithTheFirst32BitsInitializesCodeWithTrailingZeros()
        {
            bitReaderMock
                .Setup(x => x.ReadBit())
                .Returns(1);
            var expectedCode = Convert.ToUInt32("11111111111110000000000000000000", 2);
            decoder.bitsToRead = 13;

            decoder.InitializeCodeWithTheFirst32Bits();

            Assert.AreEqual(expectedCode, decoder.code);
        }

        [TestMethod]
        public void TestThatWhenCummulativeSumIs0GetSymbolIndexByCummulativeSumReturns0()
        {
            var sums = new List<int> { 0, 1, 2, 3, 4 };

            var result = decoder.GetSymbolIndexByCummulativeSum(0, sums);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void TestThatWhenCummulativeSumIsTotalSumGetSymbolIndexByCummulativeSumReturnsLastElement()
        {
            var sums = new List<int> { 0, 1, 2, 3, 4 };
            var lastIndex = sums.Count - 1;

            var result = decoder.GetSymbolIndexByCummulativeSum(4, sums);

            Assert.AreEqual(lastIndex, result);
        }

        [TestMethod]
        public void TestThatGetSymbolIndexByCummulativeSumReturnsExpectedIndex()
        {
            var sums = new List<int> { 0, 1, 2, 3, 4 };
            uint cummulativeSum = 2;
            var expectedIndex = 2;

            var result = decoder.GetSymbolIndexByCummulativeSum(cummulativeSum, sums);

            Assert.AreEqual(expectedIndex, result);
        }
    }
}
