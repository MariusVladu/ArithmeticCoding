using ArithmeticCoding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SimpleGUI
{
    public partial class UI : Form
    {
        private string filePath;
        private List<int> alphabet;

        public UI()
        {
            InitializeComponent();

            alphabet = GetCompleteAlphabet();
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
                return;

            filePath = fileDialog.FileName;

            this.labelLoadedFile.Text = Path.GetFileName(filePath) + " loaded";
            this.statusLabel.Text = Path.GetFileName(filePath) + " loaded";
        }

        private void buttonEncodeFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }
            var encodedFilePath = GetEncodedFilePath(filePath);

            var encoder = new Encoder(alphabet);

            encoder.EncodeFile(filePath, encodedFilePath);

            var compressionRatio = GetCompressionRatio(filePath, encodedFilePath);

            this.statusLabel.Text = $"File encoded to {Path.GetFileName(encodedFilePath)}. {compressionRatio}";
            MessageBox.Show("Done.");
        }

        private void buttonDecodeFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return;
            }

            if (!filePath.EndsWith("encoded"))
            {
                this.statusLabel.Text = "Cannot decode. The loaded file does not have the '.encoded' extension";
                MessageBox.Show("Cannot decode. The loaded file does not have the '.encoded' extension");
                return;
            }

            var decodedFilePath = GetDecodedFilePath(filePath);

            var decoder = new Decoder(alphabet);

            decoder.DecodeFile(filePath, decodedFilePath);

            this.statusLabel.Text = $"File decoded to {Path.GetFileName(decodedFilePath)}";
            MessageBox.Show("Done.");
        }

        private string GetEncodedFilePath(string filePath)
        {
            return $"{filePath}.encoded";
        }

        private string GetDecodedFilePath(string encodedFilePath)
        {
            var filePathWithoutEncoded = encodedFilePath.Replace(".encoded", "");

            var originalFileExtension = Path.GetExtension(filePathWithoutEncoded);

            var filePathWithoutExtension = Path.ChangeExtension(filePathWithoutEncoded, "").Trim('.');

            return $"{filePathWithoutExtension}Decoded{originalFileExtension}";
        }

        private string GetCompressionRatio(string uncompressedFilePath, string compressedFilePath)
        {
            var uncompressedSize = new FileInfo(uncompressedFilePath).Length;
            var compressedSize = new FileInfo(compressedFilePath).Length;

            var compressionRatio = Decimal.Divide(uncompressedSize, compressedSize);

            return $"Compression ratio is {compressionRatio.ToString("#.##")} : 1";
        }

        private void radioButtonSpecificAlphabet_CheckedChanged(object sender, EventArgs e)
        {
            if(this.radioButtonSpecificAlphabet.Checked)
            {
                this.textBoxSpecificAlphabet.Enabled = true;
            }
            else
            {
                this.textBoxSpecificAlphabet.Enabled = false;

                alphabet = GetCompleteAlphabet();
                this.statusLabel.Text = $"Complete ASCII alphabet loaded.";
            }
        }

        private List<int> GetSpecificAlphabet()
        {
            return textBoxSpecificAlphabet.Text
                .ToCharArray()
                .Select(x => (int)x)
                .ToList();
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

        private void textBoxSpecificAlphabet_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxSpecificAlphabet.Text))
            {
                alphabet = GetSpecificAlphabet();
                this.statusLabel.Text = $"Alphabet = {string.Join(",", alphabet)}";
            }
        }
    }
}
