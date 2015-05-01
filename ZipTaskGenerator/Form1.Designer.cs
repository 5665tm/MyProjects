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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.button5 = new System.Windows.Forms.Button();
			this.tbTexts = new System.Windows.Forms.MaskedTextBox();
			this.btRun = new System.Windows.Forms.Button();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.uploadUnZip = new System.Windows.Forms.CheckBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.tbSavePath = new System.Windows.Forms.MaskedTextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.button3 = new System.Windows.Forms.Button();
			this.tbKeys = new System.Windows.Forms.MaskedTextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.tbAcc = new System.Windows.Forms.MaskedTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.tbConfig = new System.Windows.Forms.MaskedTextBox();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.progressBar = new System.Windows.Forms.ProgressBar();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(376, 433);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox5);
			this.tabPage1.Controls.Add(this.btRun);
			this.tabPage1.Controls.Add(this.checkBox2);
			this.tabPage1.Controls.Add(this.uploadUnZip);
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(368, 407);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Zip";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.button5);
			this.groupBox5.Controls.Add(this.tbTexts);
			this.groupBox5.Location = new System.Drawing.Point(6, 192);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(356, 56);
			this.groupBox5.TabIndex = 8;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Папка с текстами";
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(275, 17);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(75, 23);
			this.button5.TabIndex = 1;
			this.button5.Text = "Обзор";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.btTexts_Click);
			// 
			// tbTexts
			// 
			this.tbTexts.Location = new System.Drawing.Point(6, 19);
			this.tbTexts.Name = "tbTexts";
			this.tbTexts.Size = new System.Drawing.Size(263, 20);
			this.tbTexts.TabIndex = 0;
			// 
			// btRun
			// 
			this.btRun.Location = new System.Drawing.Point(281, 378);
			this.btRun.Name = "btRun";
			this.btRun.Size = new System.Drawing.Size(75, 23);
			this.btRun.TabIndex = 7;
			this.btRun.Text = "Пуск";
			this.btRun.UseVisualStyleBackColor = true;
			this.btRun.Click += new System.EventHandler(this.btRun_Click);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(12, 384);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(242, 17);
			this.checkBox2.TabIndex = 6;
			this.checkBox2.Text = "Генерировать задание для UploadManager";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// uploadUnZip
			// 
			this.uploadUnZip.AutoSize = true;
			this.uploadUnZip.Location = new System.Drawing.Point(12, 361);
			this.uploadUnZip.Name = "uploadUnZip";
			this.uploadUnZip.Size = new System.Drawing.Size(229, 17);
			this.uploadUnZip.TabIndex = 5;
			this.uploadUnZip.Text = "Генерировать задание для UploadUnZip";
			this.uploadUnZip.UseVisualStyleBackColor = true;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.button4);
			this.groupBox4.Controls.Add(this.tbSavePath);
			this.groupBox4.Location = new System.Drawing.Point(6, 254);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(356, 59);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Куда сохранить";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(275, 17);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 1;
			this.button4.Text = "Обзор";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.btSavePath_Click);
			// 
			// tbSavePath
			// 
			this.tbSavePath.Location = new System.Drawing.Point(6, 19);
			this.tbSavePath.Name = "tbSavePath";
			this.tbSavePath.Size = new System.Drawing.Size(263, 20);
			this.tbSavePath.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.button3);
			this.groupBox3.Controls.Add(this.tbKeys);
			this.groupBox3.Location = new System.Drawing.Point(6, 130);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(356, 56);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Папка с ключами";
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(275, 17);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 1;
			this.button3.Text = "Обзор";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.btKeys_Click);
			// 
			// tbKeys
			// 
			this.tbKeys.Location = new System.Drawing.Point(6, 19);
			this.tbKeys.Name = "tbKeys";
			this.tbKeys.Size = new System.Drawing.Size(263, 20);
			this.tbKeys.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.button2);
			this.groupBox2.Controls.Add(this.tbAcc);
			this.groupBox2.Location = new System.Drawing.Point(6, 68);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(356, 56);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Файл с аккаунтами";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(275, 17);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Обзор";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.btAcc_Click);
			// 
			// tbAcc
			// 
			this.tbAcc.Location = new System.Drawing.Point(6, 19);
			this.tbAcc.Name = "tbAcc";
			this.tbAcc.Size = new System.Drawing.Size(263, 20);
			this.tbAcc.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.tbConfig);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(356, 56);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Файл с конфигом";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(275, 17);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Обзор";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.btConfig_Click);
			// 
			// tbConfig
			// 
			this.tbConfig.Location = new System.Drawing.Point(6, 19);
			this.tbConfig.Name = "tbConfig";
			this.tbConfig.Size = new System.Drawing.Size(263, 20);
			this.tbConfig.TabIndex = 0;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(368, 407);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Ftp";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(368, 407);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Url";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(368, 407);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Proxy";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// progressBar
			// 
			this.progressBar.ForeColor = System.Drawing.SystemColors.WindowText;
			this.progressBar.Location = new System.Drawing.Point(12, 451);
			this.progressBar.MarqueeAnimationSpeed = 0;
			this.progressBar.Maximum = 1000;
			this.progressBar.Name = "progressBar";
			this.progressBar.Size = new System.Drawing.Size(376, 23);
			this.progressBar.Step = 1;
			this.progressBar.TabIndex = 1;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 486);
			this.Controls.Add(this.progressBar);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.Text = "Packer";
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

	}

	#endregion

	private System.Windows.Forms.TabControl tabControl1;
	private System.Windows.Forms.TabPage tabPage1;
	private System.Windows.Forms.TabPage tabPage2;
	private System.Windows.Forms.ProgressBar progressBar;
	private System.Windows.Forms.GroupBox groupBox4;
	private System.Windows.Forms.Button button4;
	private System.Windows.Forms.MaskedTextBox tbSavePath;
	private System.Windows.Forms.GroupBox groupBox3;
	private System.Windows.Forms.Button button3;
	private System.Windows.Forms.MaskedTextBox tbKeys;
	private System.Windows.Forms.GroupBox groupBox2;
	private System.Windows.Forms.Button button2;
	private System.Windows.Forms.MaskedTextBox tbAcc;
	private System.Windows.Forms.GroupBox groupBox1;
	private System.Windows.Forms.Button button1;
	private System.Windows.Forms.MaskedTextBox tbConfig;
	private System.Windows.Forms.TabPage tabPage3;
	private System.Windows.Forms.TabPage tabPage4;
	private System.Windows.Forms.Button btRun;
	private System.Windows.Forms.CheckBox checkBox2;
	private System.Windows.Forms.CheckBox uploadUnZip;
	private System.Windows.Forms.GroupBox groupBox5;
	private System.Windows.Forms.Button button5;
	private System.Windows.Forms.MaskedTextBox tbTexts;

}