namespace lab
{
	partial class Calc
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
			if (disposing && (components != null)) {
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
			this.summ = new System.Windows.Forms.Label();
			this.max_divisor = new System.Windows.Forms.Label();
			this.multiply = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// summ
			// 
			this.summ.AutoSize = true;
			this.summ.Location = new System.Drawing.Point(13, 13);
			this.summ.Name = "summ";
			this.summ.Size = new System.Drawing.Size(35, 13);
			this.summ.TabIndex = 0;
			this.summ.Text = "label1";
			// 
			// max_divisor
			// 
			this.max_divisor.AutoSize = true;
			this.max_divisor.Location = new System.Drawing.Point(13, 31);
			this.max_divisor.Name = "max_divisor";
			this.max_divisor.Size = new System.Drawing.Size(35, 13);
			this.max_divisor.TabIndex = 1;
			this.max_divisor.Text = "label2";
			// 
			// multiply
			// 
			this.multiply.AutoSize = true;
			this.multiply.Location = new System.Drawing.Point(13, 47);
			this.multiply.Name = "multiply";
			this.multiply.Size = new System.Drawing.Size(35, 13);
			this.multiply.TabIndex = 2;
			this.multiply.Text = "label3";
			// 
			// Calc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.multiply);
			this.Controls.Add(this.max_divisor);
			this.Controls.Add(this.summ);
			this.Name = "Calc";
			this.Text = "Form3";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label summ;
		private System.Windows.Forms.Label max_divisor;
		private System.Windows.Forms.Label multiply;
	}
}