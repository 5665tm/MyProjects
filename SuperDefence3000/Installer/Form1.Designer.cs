namespace Installer
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
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Login = new System.Windows.Forms.TextBox();
			this.Password1 = new System.Windows.Forms.TextBox();
			this.Password2 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.Shortcut = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.Folder = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(188, 339);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(210, 36);
			this.button1.TabIndex = 0;
			this.button1.Text = "Установить!";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Install);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.4F);
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(344, 15);
			this.label1.TabIndex = 1;
			this.label1.Text = "Вас приветствует мастер установки Super Defender 3000!";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(386, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Введите логин и пароль которые будут использовать при аутентификации";
			// 
			// Login
			// 
			this.Login.Location = new System.Drawing.Point(131, 86);
			this.Login.MaxLength = 15;
			this.Login.Name = "Login";
			this.Login.Size = new System.Drawing.Size(125, 20);
			this.Login.TabIndex = 3;
			// 
			// Password1
			// 
			this.Password1.Location = new System.Drawing.Point(131, 125);
			this.Password1.MaxLength = 15;
			this.Password1.Name = "Password1";
			this.Password1.Size = new System.Drawing.Size(125, 20);
			this.Password1.TabIndex = 4;
			this.Password1.UseSystemPasswordChar = true;
			// 
			// Password2
			// 
			this.Password2.Location = new System.Drawing.Point(131, 151);
			this.Password2.MaxLength = 15;
			this.Password2.Name = "Password2";
			this.Password2.Size = new System.Drawing.Size(125, 20);
			this.Password2.TabIndex = 5;
			this.Password2.UseSystemPasswordChar = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(18, 92);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(38, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Логин";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(18, 132);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Пароль";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(18, 158);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 13);
			this.label5.TabIndex = 8;
			this.label5.Text = "Повторите пароль";
			// 
			// Shortcut
			// 
			this.Shortcut.AutoSize = true;
			this.Shortcut.Location = new System.Drawing.Point(12, 358);
			this.Shortcut.Name = "Shortcut";
			this.Shortcut.Size = new System.Drawing.Size(153, 17);
			this.Shortcut.TabIndex = 20;
			this.Shortcut.Text = "Ярлык на рабочем столе";
			this.Shortcut.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(12, 217);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(306, 13);
			this.label6.TabIndex = 10;
			this.label6.Text = "Выберите папку в которую будет установлено приложение";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(18, 247);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(90, 13);
			this.label7.TabIndex = 11;
			this.label7.Text = "C:\\";
			// 
			// Folder
			// 
			this.Folder.Location = new System.Drawing.Point(114, 244);
			this.Folder.MaxLength = 15;
			this.Folder.Name = "Folder";
			this.Folder.Size = new System.Drawing.Size(142, 20);
			this.Folder.TabIndex = 12;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(405, 388);
			this.Controls.Add(this.Folder);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.Shortcut);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.Password2);
			this.Controls.Add(this.Password1);
			this.Controls.Add(this.Login);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Установка";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox Login;
		private System.Windows.Forms.TextBox Password1;
		private System.Windows.Forms.TextBox Password2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.CheckBox Shortcut;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox Folder;
	}
}

