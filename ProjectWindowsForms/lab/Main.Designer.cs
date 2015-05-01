namespace lab
{
	partial class Main
	{
		/// <summary>
		/// Требуется переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Обязательный метод для поддержки конструктора - не изменяйте
		/// содержимое данного метода при помощи редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.calcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label479 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.num1show = new System.Windows.Forms.Label();
			this.num2show = new System.Windows.Forms.Label();
			this.lab_multiply = new System.Windows.Forms.Label();
			this.lab_sum = new System.Windows.Forms.Label();
			this.lab_max_divisor = new System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputToolStripMenuItem,
            this.calcToolStripMenuItem,
            this.quitToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(437, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// inputToolStripMenuItem
			// 
			this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
			this.inputToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
			this.inputToolStripMenuItem.Text = "Input";
			this.inputToolStripMenuItem.Click += new System.EventHandler(this.inputToolStripMenuItem_Click);
			// 
			// calcToolStripMenuItem
			// 
			this.calcToolStripMenuItem.Name = "calcToolStripMenuItem";
			this.calcToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.calcToolStripMenuItem.Text = "Calc";
			this.calcToolStripMenuItem.Click += new System.EventHandler(this.calcToolStripMenuItem_Click);
			// 
			// quitToolStripMenuItem
			// 
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
			// 
			// label479
			// 
			this.label479.AutoSize = true;
			this.label479.Location = new System.Drawing.Point(13, 28);
			this.label479.Name = "label479";
			this.label479.Size = new System.Drawing.Size(48, 13);
			this.label479.TabIndex = 1;
			this.label479.Text = "Число1:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Число2:";
			// 
			// num1show
			// 
			this.num1show.AutoSize = true;
			this.num1show.Location = new System.Drawing.Point(58, 28);
			this.num1show.Name = "num1show";
			this.num1show.Size = new System.Drawing.Size(13, 13);
			this.num1show.TabIndex = 3;
			this.num1show.Text = "0";
			// 
			// num2show
			// 
			this.num2show.AutoSize = true;
			this.num2show.Location = new System.Drawing.Point(58, 45);
			this.num2show.Name = "num2show";
			this.num2show.Size = new System.Drawing.Size(13, 13);
			this.num2show.TabIndex = 4;
			this.num2show.Text = "0";
			// 
			// lab_multiply
			// 
			this.lab_multiply.AutoSize = true;
			this.lab_multiply.Location = new System.Drawing.Point(13, 74);
			this.lab_multiply.Name = "lab_multiply";
			this.lab_multiply.Size = new System.Drawing.Size(102, 13);
			this.lab_multiply.TabIndex = 5;
			this.lab_multiply.Text = "Multiply: отключено";
			// 
			// lab_sum
			// 
			this.lab_sum.AutoSize = true;
			this.lab_sum.Location = new System.Drawing.Point(13, 59);
			this.lab_sum.Name = "lab_sum";
			this.lab_sum.Size = new System.Drawing.Size(102, 13);
			this.lab_sum.TabIndex = 6;
			this.lab_sum.Text = "Summa: отключено";
			// 
			// lab_max_divisor
			// 
			this.lab_max_divisor.AutoSize = true;
			this.lab_max_divisor.Location = new System.Drawing.Point(13, 88);
			this.lab_max_divisor.Name = "lab_max_divisor";
			this.lab_max_divisor.Size = new System.Drawing.Size(120, 13);
			this.lab_max_divisor.TabIndex = 7;
			this.lab_max_divisor.Text = "Max divisor: отключено";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(437, 112);
			this.Controls.Add(this.lab_max_divisor);
			this.Controls.Add(this.lab_sum);
			this.Controls.Add(this.lab_multiply);
			this.Controls.Add(this.num2show);
			this.Controls.Add(this.num1show);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label479);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Main";
			this.Text = "Бутакова Е.К. ПИЭ (б)–21 Вариант 6";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem calcToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
		private System.Windows.Forms.Label label479;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label num1show;
		private System.Windows.Forms.Label num2show;
		private System.Windows.Forms.Label lab_multiply;
		private System.Windows.Forms.Label lab_sum;
		private System.Windows.Forms.Label lab_max_divisor;
	}
}

