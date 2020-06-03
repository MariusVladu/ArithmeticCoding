using System;
using System.Collections.Generic;
using ArithmeticCoding;
using BitReaderWriter.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class EncoderUnitTests
    {
        private Encoder encoder;
        private Mock<IBitReader> bitReaderMock;
        private Mock<IBitWriter> bitWriterMock;

        private List<int> alphabet = new List<int> { 2, 4, 7, 10 };

        [TestInitialize]
        public void Setup()
        {
            bitReaderMock = new Mock<IBitReader>();
            bitWriterMock = new Mock<IBitWriter>();

            encoder = new Encoder(alphabet)
            {
                bitReader = bitReaderMock.Object,
                bitWriter = bitWriterMock.Object
            };
        }

        [TestMethod]
        public void TestThatWhenUnderflowBitsIs0WriteUnderflowBitsNeverCallsBitWriterWriteNBits()
        {
            encoder.underflowBits = 0;

            encoder.WriteUnderflowBits(0);

            bitWriterMock.Verify(x => x.WriteNBits(It.IsAny<int>(), It.IsAny<uint>()), Times.Never);
        }

        [TestMethod]
        public void TestThatWriteUnderflowBitsCallsBitWriterWriteNBitsExpectedNumberOfTimes()
        { 
            uint bit = 0;
            var expectedNumberOfCalls = 5;
            encoder.underflowBits = expectedNumberOfCalls;

            encoder.WriteUnderflowBits(bit);

            bitWriterMock.Verify(x => x.WriteNBits(1, bit), Times.Exactly(expectedNumberOfCalls));
        }

        [TestMethod]
        public void TestThatWriteUnderflowBitsSetsUnderflowBitsTo0()
        {
            encoder.underflowBits = 5;

            encoder.WriteUnderflowBits(1);

            Assert.AreEqual(0, encoder.underflowBits);
        }

        [TestMethod]
        public void TestThatMapSymbolToIndexInListReturnsExpectedIndex()
        {
            var list = new List<int> { 1, 3, 5, 6, 10, 15 };
            var symbol = 10;
            var expectedIndex = 4;

            var result = encoder.MapSymbolToIndexInList(symbol, list);

            Assert.AreEqual(expectedIndex, result);
        }

        [Ignore("Flawed implementation logic ?")]
        [TestMethod]
        public void TestThatWhenLowIsInTheFirstQuarterAndHighIsInTheThirdQuarterFinishEncodingWrites1()
        {
            encoder.low = Convert.ToUInt32("00111001110111011110000011000111", 2);
            encoder.high = Convert.ToUInt32("10111001110111011110000011000111", 2);

            encoder.FinishEncoding();

            bitWriterMock.Verify(x => x.WriteNBits(2, 1), Times.Once);
        }

        [Ignore("Flawed implementation logic ?")]
        [TestMethod]
        public void TestThatWhenLowIsInTheSecondQuarterAndHighIsInTheForthQuarterFinishEncodingWrites2()
        {
            encoder.low = Convert.ToUInt32("01111001110111011110000011000111", 2);
            encoder.high = Convert.ToUInt32("11111001110111011110000011000111", 2);

            encoder.FinishEncoding();

            bitWriterMock.Verify(x => x.WriteNBits(2, 2), Times.Once);
        }

        [Ignore("Flawed implementation logic ?")]
        [TestMethod]
        public void TestThatWhenLowIsInTheFirstQuarterAndHighIsInTheForthQuarterFinishEncodingWrites2()
        {
            encoder.low = Convert.ToUInt32("00111001110111011110000011000111", 2);
            encoder.high = Convert.ToUInt32("11111001110111011110000011000111", 2);

            encoder.FinishEncoding();

            bitWriterMock.Verify(x => x.WriteNBits(2, 2), Times.Once);
        }
    }
}
