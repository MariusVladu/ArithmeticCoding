using BitReaderWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ArithmeticCoding.IntegrationTests
{
    [TestClass]
    public class ArithmeticCodingIntegrationTests
    {
        private Encoder encoder;
        private Decoder decoder;

        private List<int> alphabet = new List<int> { 97, 98, 99, 100, 101 };
        private string fileContent = "abbababababaaccbaababc";

        private string inputFilePath = "inputFile.txt";
        private string outputFilePath = "outputFile.txt"; 
        private string decodedFilePath = "decoded.txt";

        [TestInitialize]
        public void Setup()
        {
            encoder = new Encoder(alphabet);
            decoder = new Decoder(alphabet);
        }

        [TestMethod]
        public void TestThatEncodeFileCreatesNonEmptyOutputFile()
        {
            File.WriteAllText(inputFilePath, fileContent);

            encoder.EncodeFile(inputFilePath, outputFilePath);

            var outputFileLength = new FileInfo(outputFilePath).Length;
            Assert.IsTrue(outputFileLength > 0);
        }

        [TestMethod]
        public void TestThatEncodedFileCanBeDecoded()
        {
            File.WriteAllText(inputFilePath, fileContent);

            encoder.EncodeFile(inputFilePath, outputFilePath);
            decoder.DecodeFile(outputFilePath, decodedFilePath);

            var decodedContent = File.ReadAllText(decodedFilePath);
            Assert.AreEqual(fileContent, decodedContent);
        }

        [TestMethod]
        public void TestThatWhenUsingCompleteAlphabetEncodedFileCanBeDecoded()
        {
            encoder = new Encoder(GetCompleteAlphabet());
            decoder = new Decoder(GetCompleteAlphabet());
            File.WriteAllText(inputFilePath, fileContent);

            encoder.EncodeFile(inputFilePath, outputFilePath);
            decoder.DecodeFile(outputFilePath, decodedFilePath);

            var decodedContent = File.ReadAllText(decodedFilePath);
            Assert.AreEqual(fileContent, decodedContent);
        }

        [TestMethod]
        public void TestThatArrayIsTheSameAfterEncodeAndDecode()
        {
            encoder = new Encoder(alphabet);
            decoder = new Decoder(alphabet);
            var array = fileContent.Select(x => (int)x).ToArray();

            encoder.EncodeArray(array, GetBitWriter(outputFilePath));
            var bitsToRead = new FileInfo(outputFilePath).Length * 8;
            var returnedArray = decoder.DecodeToArray(bitsToRead, GetBitReader(outputFilePath));

            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i], returnedArray[i]);
            }

            Assert.AreEqual(array.Length, returnedArray.Length);
        }

        [TestMethod]
        public void TestThatRandomArrayIsTheSameAfterEncodeAndDecode()
        {
            encoder = new Encoder(GetCompleteAlphabet());
            decoder = new Decoder(GetCompleteAlphabet());
            var array = GetRandomArray(5000);

            encoder.EncodeArray(array, GetBitWriter(outputFilePath));
            var bitsToRead = new FileInfo(outputFilePath).Length * 8;
            var returnedArray = decoder.DecodeToArray(bitsToRead, GetBitReader(outputFilePath));

            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i], returnedArray[i]);
            }

            Assert.AreEqual(array.Length, returnedArray.Length);
        }

        private List<int> GetCompleteAlphabet()
        {
            var completeAlphabet = new List<int>();

            for (int i = -256; i < 256; i++)
            {
                completeAlphabet.Add(i);
            }

            return completeAlphabet;
        }

        private BitReader GetBitReader(string filePath)
        {
            var inputFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new BitReader(inputFileStream);
        }

        private BitWriter GetBitWriter(string filePath)
        {
            var outputFileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            return new BitWriter(outputFileStream);
        }

        public static int[] GetRandomArray(int length)
        {
            var array = new int[length];
            var random = new Random();

            for (int i = 0; i < length; i++)
                array[i] = random.Next(-256, 256);

            return array;
        }
    }
}
