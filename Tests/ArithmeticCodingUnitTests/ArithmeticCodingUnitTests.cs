using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticCodingUnitTests
{
    [TestClass]
    public class ArithmeticCodingUnitTests
    {
        private ArithmeticCoding.ArithmeticCoding arithmeticCoding;

        private List<int> alphabet = new List<int> { 2, 4, 7, 10 };

        [TestInitialize]
        public void Setup()
        {
            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(alphabet);
        }

        [TestMethod]
        public void TestThatConstructorOrdersAlphabetAscending()
        {
            var unorderedAlphabet = new List<int> { 6, 1, 9, 3, 2 };
            var expectedOrderedAlphabet = new List<int> { 1, 2, 3, 6, 9 };

            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(unorderedAlphabet);

            arithmeticCoding.alphabet.Remove(alphabet.First());
            CollectionAssert.AreEqual(expectedOrderedAlphabet, arithmeticCoding.alphabet);
        }

        [TestMethod]
        public void TestThatConstructorAddsAnExtraEndOfFileSymbol()
        {
            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(alphabet);

            Assert.IsTrue(arithmeticCoding.alphabet.Count == alphabet.Count + 1);
        }

        [TestMethod]
        public void TestThatConstructorAddsExpectedEndOfFileSymbolToTheEndOfAlphabet()
        {
            var expectedAlphabet = new List<int>(alphabet);
            expectedAlphabet.Insert(0, 257);

            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(alphabet);

            CollectionAssert.AreEqual(expectedAlphabet, arithmeticCoding.alphabet);
        }

        [TestMethod]
        public void TestThatNumberOfSymbolsIncludesEndOfFileSymbol()
        {
            Assert.IsTrue(arithmeticCoding.numberOfSymbols == alphabet.Count + 1);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesCounts()
        {
            var expectedCounts = alphabet.Select(x => 1).ToList();
            expectedCounts.Add(1);

            arithmeticCoding.InitializeModel();

            CollectionAssert.AreEqual(expectedCounts, arithmeticCoding.counts);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesSums()
        {
            var alphabet = new List<int> { 2, 4, 7, 10 };
            var expectedSums = new List<int> { 0, 1, 2, 3, 4, 5 };

            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(alphabet);
            arithmeticCoding.InitializeModel();

            CollectionAssert.AreEqual(expectedSums, arithmeticCoding.sums);
        }

        [TestMethod]
        public void TestThatInitializeModelInitializesTotalSum()
        {
            var alphabet = new List<int> { 2, 4, 7, 10 };
            var expectedTotalSum = 5;

            arithmeticCoding = new ArithmeticCoding.ArithmeticCoding(alphabet);
            arithmeticCoding.InitializeModel();

            Assert.AreEqual(expectedTotalSum, arithmeticCoding.totalSum);
        }
    }
}
