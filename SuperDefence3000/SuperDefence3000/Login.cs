using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SuperDefence3000
{
	public partial class Login : Form
	{
		/// <summary>
		///     Пользователь решил закрыть окно аутентификации
		/// </summary>
		private bool _closeApplication = true;

		public Login()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Событие возникающее при закрытии формы
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Login_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_closeApplication)
			{
				Form1.CloseUser = true;
			}
		}

		/// <summary>
		///     Вход в качестве гостя
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogGuest(object sender, EventArgs e)
		{
			_closeApplication = false;
			Close();
		}

		/// <summary>
		///     Вход в качестве пользователя
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LogUser(object sender, EventArgs e)
		{
			const string KEY_NAME = @"SOFTWARE";
			const string KEY_S = @"SuperDefence3000";
			RegistryKey reg = Registry.CurrentUser.OpenSubKey(KEY_NAME + "\\" + KEY_S, true);
			if (reg != null)
			{
				// узнаем логин и хеш пароля
				var login = reg.GetValue("Login");
				var passmd5 = reg.GetValue("Password");
				// генерируем хеш введенного пользователем пароля
				MD5 md5 = MD5.Create();
				byte[] passBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(tbPass.Text));
				var sb = new StringBuilder();
				foreach (byte t in passBytes)
				{
					sb.Append(t.ToString("x2"));
				}
				string inputPass = sb.ToString();
				// выполняем проверку существуют ли такие ключи
				if (login != null && passmd5 != null)
				{
					// совпадают ли введенные данные?
					if ((string) login == tbLogin.Text
						&& (string) passmd5 == inputPass)
					{
						// УРА! Мы прошли аутентификацию!
						_closeApplication = false;
						Form1.IsUser = true;
						Close();
					}
					else
					{
						MessageBox.Show(@"Неверное имя пользователя или пароль");
					}
				}
				else
				{
					MessageBox.Show(@"Данные пользователя не найдены, либо повреждены. Переустановите приложение или войдите как гость");
				}
				reg.Close();
			}
			else
			{
				MessageBox.Show(@"Данные пользователя не найдены, либо повреждены. Переустановите приложение или войдите как гость");
			}
		}
	}
}