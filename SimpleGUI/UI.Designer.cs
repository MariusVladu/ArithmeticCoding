namespace SimpleGUI
{
    partial class UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelLoadedFile = new System.Windows.Forms.Label();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonEncodeFile = new System.Windows.Forms.Button();
            this.buttonDecodeFile = new System.Windows.Forms.Button();
            this.textBoxSpecificAlphabet = new System.Windows.Forms.TextBox();
            this.groupBoxAlphabet = new System.Windows.Forms.GroupBox();
            this.radioButtonCompleteAlphabet = new System.Windows.Forms.RadioButton();
            this.radioButtonSpecificAlphabet = new System.Windows.Forms.RadioButton();
            this.statusStrip1.SuspendLayout();
            this.groupBoxAlphabet.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelLoadedFile
            // 
            this.labelLoadedFile.AutoEllipsis = true;
            this.labelLoadedFile.AutoSize = true;
            this.labelLoadedFile.Location = new System.Drawing.Point(93, 17);
            this.labelLoadedFile.Name = "labelLoadedFile";
            this.labelLoadedFile.Size = new System.Drawing.Size(79, 13);
            this.labelLoadedFile.TabIndex = 4;
            this.labelLoadedFile.Text = "No File Loaded";
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(12, 12);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(75, 23);
            this.buttonLoadFile.TabIndex = 3;
            this.buttonLoadFile.Text = "Load File";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 236);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(433, 20);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
            this.statusLabel.Size = new System.Drawing.Size(86, 15);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "No File Loaded";
            // 
            // buttonEncodeFile
            // 
            this.buttonEncodeFile.Location = new System.Drawing.Point(12, 41);
            this.buttonEncodeFile.Name = "buttonEncodeFile";
            this.buttonEncodeFile.Size = new System.Drawing.Size(75, 23);
            this.buttonEncodeFile.TabIndex = 6;
            this.buttonEncodeFile.Text = "Encode File";
            this.buttonEncodeFile.UseVisualStyleBackColor = true;
            this.buttonEncodeFile.Click += new System.EventHandler(this.buttonEncodeFile_Click);
            // 
            // buttonDecodeFile
            // 
            this.buttonDecodeFile.Location = new System.Drawing.Point(348, 41);
            this.buttonDecodeFile.Name = "buttonDecodeFile";
            this.buttonDecodeFile.Size = new System.Drawing.Size(75, 23);
            this.buttonDecodeFile.TabIndex = 7;
            this.buttonDecodeFile.Text = "Decode File";
            this.buttonDecodeFile.UseVisualStyleBackColor = true;
            this.buttonDecodeFile.Click += new System.EventHandler(this.buttonDecodeFile_Click);
            // 
            // textBoxSpecificAlphabet
            // 
            this.textBoxSpecificAlphabet.Enabled = false;
            this.textBoxSpecificAlphabet.Location = new System.Drawing.Point(6, 65);
            this.textBoxSpecificAlphabet.Multiline = true;
            this.textBoxSpecificAlphabet.Name = "textBoxSpecificAlphabet";
            this.textBoxSpecificAlphabet.Size = new System.Drawing.Size(399, 73);
            this.textBoxSpecificAlphabet.TabIndex = 9;
            this.textBoxSpecificAlphabet.TextChanged += new System.EventHandler(this.textBoxSpecificAlphabet_TextChanged);
            // 
            // groupBoxAlphabet
            // 
            this.groupBoxAlphabet.Controls.Add(this.radioButtonSpecificAlphabet);
            this.groupBoxAlphabet.Controls.Add(this.textBoxSpecificAlphabet);
            this.groupBoxAlphabet.Controls.Add(this.radioButtonCompleteAlphabet);
            this.groupBoxAlphabet.Location = new System.Drawing.Point(12, 83);
            this.groupBoxAlphabet.Name = "groupBoxAlphabet";
            this.groupBoxAlphabet.Size = new System.Drawing.Size(411, 144);
            this.groupBoxAlphabet.TabIndex = 11;
            this.groupBoxAlphabet.TabStop = false;
            this.groupBoxAlphabet.Text = "Alphabet";
            // 
            // radioButtonCompleteAlphabet
            // 
            this.radioButtonCompleteAlphabet.AutoSize = true;
            this.radioButtonCompleteAlphabet.Checked = true;
            this.radioButtonCompleteAlphabet.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCompleteAlphabet.Name = "radioButtonCompleteAlphabet";
            this.radioButtonCompleteAlphabet.Size = new System.Drawing.Size(134, 17);
            this.radioButtonCompleteAlphabet.TabIndex = 12;
            this.radioButtonCompleteAlphabet.TabStop = true;
            this.radioButtonCompleteAlphabet.Text = "Use complete alphabet";
            this.radioButtonCompleteAlphabet.UseVisualStyleBackColor = true;
            // 
            // radioButtonSpecificAlphabet
            // 
            this.radioButtonSpecificAlphabet.AutoSize = true;
            this.radioButtonSpecificAlphabet.Location = new System.Drawing.Point(6, 42);
            this.radioButtonSpecificAlphabet.Name = "radioButtonSpecificAlphabet";
            this.radioButtonSpecificAlphabet.Size = new System.Drawing.Size(203, 17);
            this.radioButtonSpecificAlphabet.TabIndex = 12;
            this.radioButtonSpecificAlphabet.Text = "Use specific alphabet (ASCII symbols)";
            this.radioButtonSpecificAlphabet.UseVisualStyleBackColor = true;
            this.radioButtonSpecificAlphabet.CheckedChanged += new System.EventHandler(this.radioButtonSpecificAlphabet_CheckedChanged);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 256);
            this.Controls.Add(this.groupBoxAlphabet);
            this.Controls.Add(this.buttonDecodeFile);
            this.Controls.Add(this.buttonEncodeFile);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.labelLoadedFile);
            this.Controls.Add(this.buttonLoadFile);
            this.Name = "UI";
            this.Text = "Arithmetic Coder";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBoxAlphabet.ResumeLayout(false);
            this.groupBoxAlphabet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLoadedFile;
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Button buttonEncodeFile;
        private System.Windows.Forms.Button buttonDecodeFile;
        private System.Windows.Forms.TextBox textBoxSpecificAlphabet;
        private System.Windows.Forms.GroupBox groupBoxAlphabet;
        private System.Windows.Forms.RadioButton radioButtonSpecificAlphabet;
        private System.Windows.Forms.RadioButton radioButtonCompleteAlphabet;
    }
}

