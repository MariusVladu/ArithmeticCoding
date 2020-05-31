using ArithmeticCoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class BitwiseOperationsUnitTests
    {
        [TestMethod]
        public void TestThatWhenValueStartsWith0GetMostSignificantInPlaceReturnsExpectedValue()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);
            var expectedResult = Convert.ToUInt32("00000000000000000000000000000000", 2);

            var result = BitwiseOperations.GetMostSignificantInPlace(value);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith1GetMostSignificantInPlaceReturnsExpectedValue()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);
            var expectedResult = Convert.ToUInt32("10000000000000000000000000000000", 2);

            var result = BitwiseOperations.GetMostSignificantInPlace(value);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestThatWhenFillBitIs0ExtractSecondMostSignificantBitAndFillReturnsExpectedValue()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);
            var expectedResult = Convert.ToUInt32("11011010001110010010000110000110", 2);

            var result = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(value, 0);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestThatWhenFillBitIs1ExtractSecondMostSignificantBitAndFillReturnsExpectedValue()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);
            var expectedResult = Convert.ToUInt32("11011010001110010010000110000111", 2);

            var result = BitwiseOperations.ExtractSecondMostSignificantBitAndFill(value, 1);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
