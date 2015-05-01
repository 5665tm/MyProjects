namespace WF01
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
			this.TbInput = new System.Windows.Forms.TextBox();
			this.BtResult = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// TbInput
			// 
			this.TbInput.Location = new System.Drawing.Point(12, 102);
			this.TbInput.Name = "TbInput";
			this.TbInput.Size = new System.Drawing.Size(612, 20);
			this.TbInput.TabIndex = 0;
			// 
			// BtResult
			// 
			this.BtResult.Location = new System.Drawing.Point(12, 129);
			this.BtResult.Name = "BtResult";
			this.BtResult.Size = new System.Drawing.Size(612, 23);
			this.BtResult.TabIndex = 1;
			this.BtResult.Text = "Проверить";
			this.BtResult.UseVisualStyleBackColor = true;
			this.BtResult.Click += new System.EventHandler(this.BtResult_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(22, 23);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(312, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Возможные операции AND ( && ),  OR ( | ), NOT ( ! ), THEN ( > )";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(22, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(264, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Допустимо вводить выражение в любом регистре";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(637, 165);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BtResult);
			this.Controls.Add(this.TbInput);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Проверка булевого выражения";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TbInput;
		private System.Windows.Forms.Button BtResult;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
    }
}

