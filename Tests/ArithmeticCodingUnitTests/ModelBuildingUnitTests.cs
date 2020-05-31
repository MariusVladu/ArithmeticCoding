using System.Collections.Generic;
using System.Linq;
using ArithmeticCoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class ModelBuildingUnitTests
    {
        [TestMethod]
        public void TestThatGetSymbolCountsReturnsListFullOf1()
        {
            var alphabet = new List<int> { 5, 10, 15 };

            var result = ModelBuilding.GetSymbolCounts(alphabet);

            Assert.IsTrue(result.TrueForAll(x => x == 1));
        }

        [TestMethod]
        public void TestThatWhenIndexIs0GetCummulativeSumForIndexReturns0()
        {
            var counts = new List<int> { 5, 10, 15 };
            var index = 0;
            var expectedSum = 0;

            var result = ModelBuilding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatWhenIndexIs1GetCummulativeSumForIndexReturnsFirstElementCount()
        {
            var counts = new List<int> { 5, 10, 15 };
            var index = 1;
            var expectedSum = 5;

            var result = ModelBuilding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumForIndexReturnsExpectedSum()
        {
            var counts = new List<int> { 2, 4, 3, 6 };
            var index = 3;
            var expectedSum = 9;

            var result = ModelBuilding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsAddsExtraElementForTotalSum()
        {
            var counts = new List<int> { 2, 4, 3 };
            var expectedResultLength = counts.Count + 1;

            var result = ModelBuilding.GetCummulativeSums(counts);

            Assert.AreEqual(expectedResultLength, result.Count);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsLastElementIsTotalSum()
        {
            var counts = new List<int> { 2, 4, 3 };
            var expectedTotalSum = 9;

            var result = ModelBuilding.GetCummulativeSums(counts);

            Assert.AreEqual(expectedTotalSum, result.Last());
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsReturnsExpectedSums()
        {
            var counts = new List<int> { 2, 4, 3, 6 };
            var expectedSums = new List<int> { 0, 2, 6, 9, 15 };

            var result = ModelBuilding.GetCummulativeSums(counts);

            CollectionAssert.AreEqual(expectedSums, result);
        }

        [TestMethod]
        public void TestThatMapSymbolToIndexInListReturnsExpectedIndex()
        {
            var list = new List<int> { 1, 3, 5, 6, 10, 15 };
            var symbol = 10;
            var expectedIndex = 4;

            var result = ModelBuilding.MapSymbolToIndexInList(symbol, list);

            Assert.AreEqual(expectedIndex, result);
        }
    }
}
