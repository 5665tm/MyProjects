using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SuperDefence3000
{
	public partial class Form1 : Form
	{
		/// <summary>
		///     В программу вошел пользователь или гость?
		/// </summary>
		private static bool _isUser;

		/// <summary>
		///     Пользователь решил не входить в программу
		/// </summary>
		private static bool _closeUser;

		/// <summary>
		///     В программу вошел пользователь или гость?
		/// </summary>
		public static bool IsUser { set { _isUser = value; } }

		/// <summary>
		///     Пользователь решил не входить в программу
		/// </summary>
		public static bool CloseUser { set { _closeUser = value; } }

		public Form1()
		{
			InitializeComponent();
			// открываем окно ввода логина и пароля
			var login = new Login();
			login.ShowDialog();
			// открываем доп. возможности для пользователя
			if (_isUser)
			{
				DateBt.Visible = true;
			}
		}

		/// <summary>
		///     Пользователь нажал на кнопку "Показать цвет"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btColor_Click(object sender, EventArgs e)
		{
			ThreeStringsToColor(tbInput.Text);
			AddToDataBase(tbInput.Text);
		}

		/// <summary>
		///     Окрашивает форму в цвет в зависимости от введенной строки
		/// </summary>
		/// <param name="inputData">Входная строка</param>
		private void ThreeStringsToColor(string inputData)
		{
			var intArray = new int[3];
			// TODO Возможная Уязвимость #1 Переполнение буфера (ошибка индексации массива)
			// Входная строка разделяется пробелами и значения заносятся в массив stringsArray
			// Существует опасность выхода за пределы массива
			// Возможная мера противодействия: использование кодового блока if-else для проверки размерности массива
			// удалим дубликаты пробелов
			bool spaceCopy = true;
			while (spaceCopy)
			{
				spaceCopy = false;
				inputData = inputData.Replace("  ", " ");
				for (int i = 1; i < inputData.Length; i++)
				{
					if (inputData[i] == ' ' && inputData[i - 1] == ' ')
					{
						spaceCopy = true;
					}
				}
			}
			// Не будет лишним так же удалить пробелы в начале и в конце строки, при помощи функции Trim()
			// и разделим строку на части по пробелам
			var inputArray = inputData.Trim().Split();
			if (inputArray.Length == 3)
			{
				for (int i = 0; i < inputArray.Length; i++)
				{
					// TODO Возможная Уязвимость #3.1 Все входные данные зловредны, пока не доказано обратное.
					// Не является подтвержденным тот факт, что пользователь ввел в программу именно числа
					// Также, поскольку числа являются компонентами RGB, необходимо следить за тем
					// что бы они были в диапазоне от 0 до 255
					// Возможная мера противодействия - ввести в программу соответствующие проверки
					// 1 - действительно ли число?
					try
					{
						int comp = Convert.ToInt32(inputArray[i]);
						// 2 - число лежит в диапазоне от 0 до 255 ?
						if (comp >= 0 && comp <= 255)
						{
							intArray[i] = comp;
						}
						else
						{
							MessageBox.Show(@"Все числа должны быть от 0 до 255!");
							return;
						}
					}
					catch (FormatException ex)
					{
						MessageBox.Show(@"Все введенные параметры должны являться целыми числами!");
						return;
					}
				}
				BackColor = Color.FromArgb(intArray[0], intArray[1], intArray[2]);
			}
			else
			{
				MessageBox.Show(@"Вы должны ввести три значения!");
			}
		}

		/// <summary>
		///     Метод имитирует добавление новой записи в базу данных
		/// </summary>
		/// <param name="inputString"></param>
		private void AddToDataBase(string inputString)
		{
			// для простоты примера пусть id = 0
			const int ID = 0;
			// TODO Возможная Уязвимость #2 SQL - инъекция
			// Предположим что программа заносит в базу данных находящуюся на сервере все значения введенные пользователем,
			// даже неверные, к примеру для того чтобы разработчики могли на основе этих сведений улучшать
			// свой программный продукт. В связи с тем что входные данные никак не обрабатываются,
			// появляется опасность SQL инъекции
			// Нельзя делать как в примере ниже!!!
			string badQuerry = "INSERT INTO MYDATA VALUES(" + ID + ",'" + inputString + "');";
			// Возможная мера противодействия: использование параметризированного запроса следующим образом
			// (Пример для SQL Server)
			const string GOOD_QUERY = "INSERT INTO MYDATA VALUES(Id=@id AND InputString=@inputString);";
			var sqlConn = new SqlConnection();
			var sqlCmd = new SqlCommand(GOOD_QUERY, sqlConn);
			sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = ID;
			sqlCmd.Parameters.Add("@inputString", SqlDbType.VarChar).Value = inputString;
			// Выполнение запроса осуществляется вот так:
			//sqlCmd.ExecuteScalar();
			InsertInto(inputString);
		}

		/// <summary>
		///     Имитирует выполнение парамтризированного запроса InsertInto
		/// </summary>
		/// <param name="text"></param>
		private void InsertInto(string text)
		{
			const string FILE_NAME = "DATA_BASE.txt";
			var fs = new FileStream(FILE_NAME, FileMode.Append, FileAccess.Write);
			var sw = new StreamWriter(fs);
			sw.WriteLine(text);
			sw.Close();
			fs.Close();
		}

		/// <summary>
		///     Выводит текущую дату в заголовок
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DateToTitle_Click(object sender, EventArgs e)
		{
			Text = Convert.ToString(DateTime.Now);
		}

		/// <summary>
		///     Закрывает форму если пользователь решил не проходить аутентификацию
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{
			if (_closeUser)
			{
				Close();
			}
		}
	}
}