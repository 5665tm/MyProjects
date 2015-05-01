using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Installer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Кликнули на кнопку "Установить"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Install(object sender, EventArgs e)
		{
			// Проверяем все поля на корректность
			if (!CheckRegex(Login, "Логин")
				|| !CheckRegex(Password1, "Пароль")
				|| !CheckRegex(Password2, "Пароль")
				// TODO Возможная Уязвимость #4 Ошибка канонизации
				// Уязвимость, возникающая, когда приложение анализирует имя файла прежде, чем операционная система канонизирует его.
				// ввода любых симвлолов кроме цифр и латинских букв
				// В данном случае пользователь не может ввести ничего кроме цифр и латинских букв, за счет чего
				// достигается безопасность ввода
				|| !CheckRegex(Folder, "Папка установки"))
			{
				return;
			}
			// проверим что пользователь не устанавливает программу в системные папки
			if (Folder.Text.ToUpperInvariant() == @"WINDOWS" || Folder.Text.ToUpperInvariant() == @"USERS")
			{
				MessageBox.Show(@"Невозможно установить программу в системную папку. Выберите другое имя");
				return;
			}
			// проверим что пароли совпадают
			if (Password1.Text != Password2.Text)
			{
				MessageBox.Show(@"Пароли не совпадают. Введите значения заново");
				return;
			}
			// TODO Возможная Уязвимость #5 Минимизация привилегий
			// Во-первых проверим что пароль пользователя достаточно надежен
			// выше уже проводилась проверка на то что пароль содержит не менее 4 символов
			// теперь убедимся в том что пароль не является простым
			if (!Regex.Match(Password1.Text, "[a-z]").Success
				|| !Regex.Match(Password1.Text, "[A-Z]").Success
				|| !Regex.Match(Password1.Text, "[0-9]").Success)
			{
				MessageBox.Show(@"Пароль должен содержать как минимум одну цифру, одну заглавную букву латинского алфавита, и одну прописную");
				return;
			}
			// Во вторых проверим что логин и пароль не совпадают
			if (Password1.Text == Login.Text)
			{
				MessageBox.Show(@"Логин и пароль не должны совпадать!");
				return;
			}
			// Во третьих даннные приложения, такие как логин и пароль будем хранить в реестре, в папке HKEY_CURRENT_USER
			// В четвертых пароль будем хранить в виде результата хеширования MD5
			MD5 md5 = MD5.Create();
			byte[] passBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(Password1.Text));
			var sb = new StringBuilder();
			foreach (byte t in passBytes)
			{
				sb.Append(t.ToString("x2"));
			}
			string md5Pass = sb.ToString();
			const string KEY_NAME = @"SOFTWARE";
			const string KEY_S = @"SuperDefence3000";
			RegistryKey reg = Registry.CurrentUser.OpenSubKey(KEY_NAME, true);
			if (reg != null)
			{
				reg.CreateSubKey(KEY_S);
			}
			reg = Registry.CurrentUser.OpenSubKey(KEY_NAME + "\\" + KEY_S, true);
			if (reg != null)
			{
				reg.SetValue("Login", Login.Text);
				reg.SetValue("Password", md5Pass);
				reg.Close();
			}
			try
			{
				if (!Directory.Exists(@"C:\" + Folder.Text))
				{
					// Инсталлятор необходимо запускать от имени администратора!
					// Из проводника запрос на получение прав появится автоматически
					// Для того что бы запустить с правами админа из студии, необходимо
					// нажать CTRL + F5 и следовать инструкциям
					Directory.CreateDirectory(@"C:\" + Folder.Text);
				}
				File.Copy("SuperDefence3000.exe", @"C:\" + Folder.Text + @"\SuperDefence3000.exe", true);
				if (Shortcut.Checked)
				{
					AppShortcutToDesktop("SuperDefence3000");
				}
				MessageBox.Show(@"Установка завершена");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		/// <summary>
		///     Создает ярлык на приложение
		/// </summary>
		/// <param name="linkName">Имя ярлыка</param>
		private void AppShortcutToDesktop(string linkName)
		{
			string deskDir = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			using (var writer = new StreamWriter(deskDir + "\\" + linkName + ".url"))
			{
				string app = @"C:\" + Folder.Text + @"\" + linkName + @".exe";
				writer.WriteLine("[InternetShortcut]");
				writer.WriteLine("URL=file:///" + app);
				writer.WriteLine("IconIndex=0");
				string icon = app.Replace('\\', '/');
				writer.WriteLine("IconFile=" + icon);
				writer.Flush();
			}
		}

		/// <summary>
		///     Метод возвращает TRUE в случае если в указанном TextBox только цифры и буквы латинского алфавита
		/// </summary>
		/// <param name="inputSource">Проверяемый TextBox</param>
		/// <param name="name">Имя поля</param>
		/// <returns>Результат проверки</returns>
		private static bool CheckRegex(TextBox inputSource, string name)
		{
			// TODO Возможная Уязвимость #3.2 Фильтрация входных данных
			// Выполняем проверку всех входных данных
			if (inputSource.Text.Length < 4)
			{
				// ограничение на 15 симвлов сверху уже прописано в TextBox'ах
				MessageBox.Show(@"В поле " + name + @" должно находиться от 4 до 15 символов");
				return false;
			}
			var rg = new Regex(@"[^a-zA-Z0-9]");
			if (rg.IsMatch(inputSource.Text))
			{
				MessageBox.Show(@"В поле " + name + @" могут содержаться только числа и латинские буквы");
				return false;
			}
			return true;
		}
	}
}