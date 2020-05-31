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
    public class IntervalsUnitTests
    {
        [TestMethod]
        public void TestThatWhenValueStartsWith0IsInTheFirstHalfReturnsTrue()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);

            var result = Intervals.IsInTheFirstHalf(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith1IsInTheFirstHalfReturnsFalse()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);

            var result = Intervals.IsInTheFirstHalf(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith1IsInTheSecondHalfReturnsTrue()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);

            var result = Intervals.IsInTheSecondHalf(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith0IsInTheSecondHalfReturnsFalse()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);

            var result = Intervals.IsInTheSecondHalf(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith00IsInTheFirstQuarterReturnsTrue()
        {
            var value = Convert.ToUInt32("00101101000111001001000011000011", 2);

            var result = Intervals.IsInTheFirstQuarter(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueDoesNotStartWith00IsInTheFirstQuarterReturnsFalse()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);

            var result = Intervals.IsInTheFirstQuarter(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith01IsInTheSecondQuarterReturnsTrue()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);

            var result = Intervals.IsInTheSecondQuarter(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueDoesNotStartWith01IsInTheSecondQuarterReturnsFalse()
        {
            var value = Convert.ToUInt32("10101101000111001001000011000011", 2);

            var result = Intervals.IsInTheSecondQuarter(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith10IsInTheThirdQuarterReturnsTrue()
        {
            var value = Convert.ToUInt32("10101101000111001001000011000011", 2);

            var result = Intervals.IsInTheThirdQuarter(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueDoesNotStartWith10IsInTheThirdQuarterReturnsFalse()
        {
            var value = Convert.ToUInt32("01101101000111001001000011000011", 2);

            var result = Intervals.IsInTheThirdQuarter(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenValueStartsWith11IsInTheForthQuarterReturnsTrue()
        {
            var value = Convert.ToUInt32("11101101000111001001000011000011", 2);

            var result = Intervals.IsInTheForthQuarter(value);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValueDoesNotStartWith11IsInTheForthQuarterReturnsFalse()
        {
            var value = Convert.ToUInt32("10101101000111001001000011000011", 2);

            var result = Intervals.IsInTheForthQuarter(value);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TestThatWhenBothValuesStartWith0ValuesAreInTheSameHalfReturnsTrue()
        {
            var value1 = Convert.ToUInt32("00101101000111001001000011000011", 2);
            var value2 = Convert.ToUInt32("00101101000111001001000011000011", 2);

            var result = Intervals.ValuesAreInTheSameHalf(value1, value2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenBothValuesStartWith1ValuesAreInTheSameHalfReturnsTrue()
        {
            var value1 = Convert.ToUInt32("10101101000111001001000011000011", 2);
            var value2 = Convert.ToUInt32("10101101000111001001000011000011", 2);

            var result = Intervals.ValuesAreInTheSameHalf(value1, value2);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestThatWhenValuesStartWithDifferentBitsValuesAreInTheSameHalfReturnsFalse()
        {
            var value1 = Convert.ToUInt32("10101101000111001001000011000011", 2);
            var value2 = Convert.ToUInt32("00101101000111001001000011000011", 2);

            var result = Intervals.ValuesAreInTheSameHalf(value1, value2);

            Assert.IsFalse(result);
        }
    }
}
