using ArithmeticCoding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class BaseArithmeticCodingUnitTests
    {
        private BaseArithmeticCoding baseArithmeticCoding;

        private List<int> alphabet = new List<int> { 2, 4, 7, 10 };

        [TestInitialize]
        public void Setup()
        {
            baseArithmeticCoding = new BaseArithmeticCoding(alphabet);
        }

        [TestMethod]
        public void TestThatConstructorOrdersAlphabetAscending()
        {
            var unorderedAlphabet = new List<int> { 6, 1, 9, 3, 2 };
            var expectedOrderedAlphabet = new List<int> { 1, 2, 3, 6, 9 };

            baseArithmeticCoding = new BaseArithmeticCoding(unorderedAlphabet);

            baseArithmeticCoding.alphabet.Remove(alphabet.Last());
            CollectionAssert.AreEqual(expectedOrderedAlphabet, baseArithmeticCoding.alphabet);
        }

        [TestMethod]
        public void TestThatConstructorAddsAnExtraEndOfFileSymbol()
        {
            baseArithmeticCoding = new BaseArithmeticCoding(alphabet);

            Assert.IsTrue(baseArithmeticCoding.alphabet.Count == alphabet.Count + 1);
        }

        [TestMethod]
        public void TestThatConstructorAddsExpectedEndOfFileSymbolToTheEndOfAlphabet()
        {
            var expectedAlphabet = new List<int>(alphabet);
            expectedAlphabet.Add(alphabet.Last() + 1);

            baseArithmeticCoding = new BaseArithmeticCoding(alphabet);

            CollectionAssert.AreEqual(expectedAlphabet, baseArithmeticCoding.alphabet);
        }

        [TestMethod]
        public void TestThatNumberOfSymbolsIncludesEndOfFileSymbol()
        {
            Assert.IsTrue(baseArithmeticCoding.numberOfSymbols == alphabet.Count + 1);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesCounts()
        {
            var expectedCounts = alphabet.Select(x => 1).ToList();
            expectedCounts.Add(1);

            baseArithmeticCoding.InitializeModel();

            CollectionAssert.AreEqual(expectedCounts, baseArithmeticCoding.counts);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesSums()
        {
            var alphabet = new List<int> { 2, 4, 7, 10 };
            var expectedSums = new List<int> { 0, 1, 2, 3, 4, 5 };

            baseArithmeticCoding = new BaseArithmeticCoding(alphabet);
            baseArithmeticCoding.InitializeModel();

            CollectionAssert.AreEqual(expectedSums, baseArithmeticCoding.sums);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesTotalSum()
        {
            var alphabet = new List<int> { 2, 4, 7, 10 };
            var expectedTotalSum = 5;

            baseArithmeticCoding = new BaseArithmeticCoding(alphabet);
            baseArithmeticCoding.InitializeModel();

            Assert.AreEqual(expectedTotalSum, baseArithmeticCoding.totalSum);
        }

        [TestMethod]
        public void TestThatInitializeSymbolCountsReturnsListFullOf1()
        {
            var alphabet = new List<int> { 5, 10, 15 };

            var result = baseArithmeticCoding.InitializeSymbolCounts(alphabet);

            Assert.IsTrue(result.TrueForAll(x => x == 1));
        }

        [TestMethod]
        public void TestThatWhenIndexIs0GetCummulativeSumForIndexReturns0()
        {
            var counts = new List<int> { 5, 10, 15 };
            var index = 0;
            var expectedSum = 0;

            var result = baseArithmeticCoding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatWhenIndexIs1GetCummulativeSumForIndexReturnsFirstElementCount()
        {
            var counts = new List<int> { 5, 10, 15 };
            var index = 1;
            var expectedSum = 5;

            var result = baseArithmeticCoding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumForIndexReturnsExpectedSum()
        {
            var counts = new List<int> { 2, 4, 3, 6 };
            var index = 3;
            var expectedSum = 9;

            var result = baseArithmeticCoding.GetCummulativeSumForIndex(counts, index);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsAddsExtraElementForTotalSum()
        {
            var counts = new List<int> { 2, 4, 3 };
            var expectedResultLength = counts.Count + 1;

            var result = baseArithmeticCoding.GetCummulativeSums(counts);

            Assert.AreEqual(expectedResultLength, result.Count);
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsLastElementIsTotalSum()
        {
            var counts = new List<int> { 2, 4, 3 };
            var expectedTotalSum = 9;

            var result = baseArithmeticCoding.GetCummulativeSums(counts);

            Assert.AreEqual(expectedTotalSum, result.Last());
        }

        [TestMethod]
        public void TestThatGetCummulativeSumsReturnsExpectedSums()
        {
            var counts = new List<int> { 2, 4, 3, 6 };
            var expectedSums = new List<int> { 0, 2, 6, 9, 15 };

            var result = baseArithmeticCoding.GetCummulativeSums(counts);

            CollectionAssert.AreEqual(expectedSums, result);
        }

        [TestMethod]
        public void TestThatUpdateModelIncreasesSymbolCountBy1()
        {
            baseArithmeticCoding.counts = new List<int> { 1, 1, 1, 1, 1 };
            baseArithmeticCoding.sums = new List<int> { 0, 1, 2, 3, 4, 5 };
            var symbolIndex = 2;
            var countBeforeUpdate = baseArithmeticCoding.counts[symbolIndex];

            baseArithmeticCoding.UpdateModel(symbolIndex);

            var countAfterUpdate = baseArithmeticCoding.counts[symbolIndex];
            Assert.AreEqual(countBeforeUpdate + 1, countAfterUpdate);
        }

        [TestMethod]
        public void TestThatUpdateCummulativeSumsStartingFromIndexUpdatesSumsAsExpected()
        {
            var counts = new List<int> { 1, 2, 1, 1 };
            var sums =   new List<int> { 0, 1, 2, 3, 4 };
            var expectedSums = new List<int> { 0, 1, 3, 4, 5 };
            var startIndex = 1;

            baseArithmeticCoding.UpdateCummulativeSumsStartingFromIndex(sums, startIndex, counts);

            CollectionAssert.AreEqual(expectedSums, sums);
        }

        [TestMethod]
        public void TestThatWhenStartIndexIs0UpdateCummulativeSumsStartingFromIndexUpdatesSumsAsExpected()
        {
            var counts = new List<int> { 1, 2, 1, 1 };
            var sums = new List<int> { 0, 1, 2, 3, 4 };
            var expectedSums = new List<int> { 0, 1, 3, 4, 5 };
            var startIndex = 0;

            baseArithmeticCoding.UpdateCummulativeSumsStartingFromIndex(sums, startIndex, counts);

            CollectionAssert.AreEqual(expectedSums, sums);
        }

        [TestMethod]
        public void TestThatUpdateModelUpdatesTotalSum()
        {
            baseArithmeticCoding.counts = new List<int> { 1, 1, 1, 1, 1 };
            baseArithmeticCoding.sums = new List<int> { 0, 1, 2, 3, 4, 5 };
            baseArithmeticCoding.totalSum = 5;
            var symbolIndex = 2;
            var totalSumBeforeUpdate = baseArithmeticCoding.totalSum;

            baseArithmeticCoding.UpdateModel(symbolIndex);

            var totalSumAfterUpdate = baseArithmeticCoding.totalSum;
            Assert.AreEqual(totalSumBeforeUpdate + 1, totalSumAfterUpdate);
        }
    }
}
