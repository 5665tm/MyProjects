namespace SuperDefence3000
{
	partial class Form1
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
			if (disposing && (components != null))
			{
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
			this.btUpper = new System.Windows.Forms.Button();
			this.tbInput = new System.Windows.Forms.TextBox();
			this.lbOut = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.DateBt = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btUpper
			// 
			this.btUpper.Location = new System.Drawing.Point(12, 104);
			this.btUpper.Name = "btUpper";
			this.btUpper.Size = new System.Drawing.Size(259, 38);
			this.btUpper.TabIndex = 0;
			this.btUpper.Text = "Показать цвет";
			this.btUpper.UseVisualStyleBackColor = true;
			this.btUpper.Click += new System.EventHandler(this.btColor_Click);
			// 
			// tbInput
			// 
			this.tbInput.Location = new System.Drawing.Point(12, 75);
			this.tbInput.MaxLength = 20;
			this.tbInput.Name = "tbInput";
			this.tbInput.Size = new System.Drawing.Size(260, 20);
			this.tbInput.TabIndex = 3;
			// 
			// lbOut
			// 
			this.lbOut.AutoSize = true;
			this.lbOut.Location = new System.Drawing.Point(12, 221);
			this.lbOut.Name = "lbOut";
			this.lbOut.Size = new System.Drawing.Size(0, 13);
			this.lbOut.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(264, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Введите три целых числа разделенных пробелами";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label2.Location = new System.Drawing.Point(12, 47);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(169, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Числа должны быть от 0 до 255";
			// 
			// DateBt
			// 
			this.DateBt.Location = new System.Drawing.Point(12, 196);
			this.DateBt.Name = "DateBt";
			this.DateBt.Size = new System.Drawing.Size(259, 38);
			this.DateBt.TabIndex = 7;
			this.DateBt.Text = "Вывести дату в заголовок (для пользователей)";
			this.DateBt.UseVisualStyleBackColor = true;
			this.DateBt.Visible = false;
			this.DateBt.Click += new System.EventHandler(this.DateToTitle_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 261);
			this.Controls.Add(this.DateBt);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbOut);
			this.Controls.Add(this.tbInput);
			this.Controls.Add(this.btUpper);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(300, 300);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(300, 300);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Super Defence 3000";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btUpper;
		private System.Windows.Forms.TextBox tbInput;
		private System.Windows.Forms.Label lbOut;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button DateBt;
	}
}

