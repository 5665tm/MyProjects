namespace YarrowAlgorithm
{
	partial class Form1
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.bitCounter = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.mNumeric = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.CountRandom = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.bitCounter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mNumeric)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(13, 88);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(272, 52);
			this.button1.TabIndex = 0;
			this.button1.Text = "Generate (Yarrow-160)";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.GenerateYarrow160_Click);
			// 
			// button2
			// 
			this.button2.Enabled = false;
			this.button2.Location = new System.Drawing.Point(13, 156);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(272, 42);
			this.button2.TabIndex = 1;
			this.button2.Text = "Частотный тест";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.FreqTest_Click);
			// 
			// button3
			// 
			this.button3.Enabled = false;
			this.button3.Location = new System.Drawing.Point(13, 204);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(272, 42);
			this.button3.TabIndex = 2;
			this.button3.Text = "Тест на последовательность одинаковых бит";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.SequenceTest_Click);
			// 
			// button4
			// 
			this.button4.Enabled = false;
			this.button4.Location = new System.Drawing.Point(13, 252);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(272, 42);
			this.button4.TabIndex = 3;
			this.button4.Text = "Тест на произвольные отклонения";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.ExtendedTest_Click);
			// 
			// bitCounter
			// 
			this.bitCounter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.bitCounter.Location = new System.Drawing.Point(165, 12);
			this.bitCounter.Name = "bitCounter";
			this.bitCounter.Size = new System.Drawing.Size(120, 20);
			this.bitCounter.TabIndex = 4;
			this.bitCounter.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "n-битный счетчик Co";
			// 
			// mNumeric
			// 
			this.mNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mNumeric.Location = new System.Drawing.Point(165, 38);
			this.mNumeric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.mNumeric.Name = "mNumeric";
			this.mNumeric.Size = new System.Drawing.Size(120, 20);
			this.mNumeric.TabIndex = 6;
			this.mNumeric.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.mNumeric.ValueChanged += new System.EventHandler(this.mNumeric_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(92, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "m - число циклов";
			// 
			// CountRandom
			// 
			this.CountRandom.AutoSize = true;
			this.CountRandom.Location = new System.Drawing.Point(16, 69);
			this.CountRandom.Name = "CountRandom";
			this.CountRandom.Size = new System.Drawing.Size(203, 13);
			this.CountRandom.TabIndex = 8;
			this.CountRandom.Tag = "";
			this.CountRandom.Text = "Всего элементов сгенерируется: 3200";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 306);
			this.Controls.Add(this.CountRandom);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.mNumeric);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.bitCounter);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(313, 344);
			this.MinimumSize = new System.Drawing.Size(313, 344);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.bitCounter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mNumeric)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.NumericUpDown bitCounter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown mNumeric;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label CountRandom;
	}
}

