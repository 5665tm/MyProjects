namespace lab
{
	partial class Input
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.number1box = new System.Windows.Forms.TextBox();
			this.number2box = new System.Windows.Forms.TextBox();
			this.summa_check = new System.Windows.Forms.CheckBox();
			this.max_divisor_check = new System.Windows.Forms.CheckBox();
			this.multiply_check = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "number1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "number2";
			// 
			// number1box
			// 
			this.number1box.Location = new System.Drawing.Point(67, 13);
			this.number1box.Name = "number1box";
			this.number1box.Size = new System.Drawing.Size(53, 20);
			this.number1box.TabIndex = 2;
			// 
			// number2box
			// 
			this.number2box.Location = new System.Drawing.Point(67, 40);
			this.number2box.Name = "number2box";
			this.number2box.Size = new System.Drawing.Size(53, 20);
			this.number2box.TabIndex = 3;
			// 
			// summa_check
			// 
			this.summa_check.AutoSize = true;
			this.summa_check.Location = new System.Drawing.Point(136, 12);
			this.summa_check.Name = "summa_check";
			this.summa_check.Size = new System.Drawing.Size(61, 17);
			this.summa_check.TabIndex = 4;
			this.summa_check.Text = "Summa";
			this.summa_check.UseVisualStyleBackColor = true;
			// 
			// max_divisor_check
			// 
			this.max_divisor_check.AutoSize = true;
			this.max_divisor_check.Location = new System.Drawing.Point(136, 29);
			this.max_divisor_check.Name = "max_divisor_check";
			this.max_divisor_check.Size = new System.Drawing.Size(79, 17);
			this.max_divisor_check.TabIndex = 5;
			this.max_divisor_check.Text = "Max divisor";
			this.max_divisor_check.UseVisualStyleBackColor = true;
			// 
			// multiply_check
			// 
			this.multiply_check.AutoSize = true;
			this.multiply_check.Location = new System.Drawing.Point(136, 46);
			this.multiply_check.Name = "multiply_check";
			this.multiply_check.Size = new System.Drawing.Size(61, 17);
			this.multiply_check.TabIndex = 6;
			this.multiply_check.Text = "Multiply";
			this.multiply_check.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(221, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(45, 48);
			this.button1.TabIndex = 7;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Input
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(278, 72);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.multiply_check);
			this.Controls.Add(this.max_divisor_check);
			this.Controls.Add(this.summa_check);
			this.Controls.Add(this.number2box);
			this.Controls.Add(this.number1box);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Input";
			this.Text = "Введите значения";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox number1box;
		private System.Windows.Forms.TextBox number2box;
		private System.Windows.Forms.CheckBox summa_check;
		private System.Windows.Forms.CheckBox max_divisor_check;
		private System.Windows.Forms.CheckBox multiply_check;
		private System.Windows.Forms.Button button1;
	}
}