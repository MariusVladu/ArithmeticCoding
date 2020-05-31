using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace ArithmeticCoding.IntegrationTests
{
    [TestClass]
    public class ArithmeticCodingIntegrationTests
    {
        private ArithmeticCoding arithmeticCoding;

        private List<int> alphabet = new List<int> { 97, 98, 99, 100, 101 };
        private string fileContent = "abbababababaaccbaababc";

        private string inputFilePath = "inputFile.txt";
        private string outputFilePath = "outputFile.txt"; 
        private string decodedFilePath = "decoded.txt";

        [TestInitialize]
        public void Setup()
        {
            arithmeticCoding = new ArithmeticCoding(alphabet);
        }

        [TestMethod]
        public void TestThatEncodeFileCreatesNonEmptyOutputFile()
        {
            File.WriteAllText(inputFilePath, fileContent);

            arithmeticCoding.EncodeFile(inputFilePath, outputFilePath);

            var outputFileLength = new FileInfo(outputFilePath).Length;
            Assert.IsTrue(outputFileLength > 0);
        }

        [TestMethod]
        public void TestThatEncodedFileCanBeDecoded()
        {
            File.WriteAllText(inputFilePath, fileContent);

            arithmeticCoding.EncodeFile(inputFilePath, outputFilePath);
            arithmeticCoding.DecodeFile(outputFilePath, decodedFilePath);

            var decodedContent = File.ReadAllText(decodedFilePath);
            Assert.AreEqual(fileContent, decodedContent);
        }

        [TestMethod]
        public void TestThatWhenUsingCompleteAlphabetEncodedFileCanBeDecoded()
        {
            File.WriteAllText(inputFilePath, fileContent);

            arithmeticCoding = new ArithmeticCoding(GetCompleteAlphabet());
            arithmeticCoding.EncodeFile(inputFilePath, outputFilePath);
            arithmeticCoding.DecodeFile(outputFilePath, decodedFilePath);

            var decodedContent = File.ReadAllText(decodedFilePath);
            Assert.AreEqual(fileContent, decodedContent);
        }

        private List<int> GetCompleteAlphabet()
        {
            var completeAlphabet = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                completeAlphabet.Add(i);
            }

            return completeAlphabet;
        }
    }
}
