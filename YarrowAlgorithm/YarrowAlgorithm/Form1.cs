// Changed: 2014 09 18 11:17 : 5665tm

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace YarrowAlgorithm
{
	public partial class Form1 : Form
	{
		// ReSharper disable InconsistentNaming
		/// <summary>
		///     Список n-битного счетчика
		/// </summary>
		private readonly List<BigInteger> Ci = new List<BigInteger>();

		/// <summary>
		///     Список Si
		/// </summary>
		private readonly List<int> Si = new List<int>();

		/// <summary>
		///     Сгерерированная случайная последовательность из нулей и единиц
		/// </summary>
		private static readonly List<byte> OutputRandom = new List<byte>();

		/// <summary>
		///     Список Vi
		/// </summary>
		private readonly List<byte[]> Vi = new List<byte[]>();

		/// <summary>
		///     Список блоков выходной последовательности
		/// </summary>
		private readonly List<byte[]> Xi = new List<byte[]>();

		/// <summary>
		///     Константа используемая при прохождении тестов
		/// </summary>
		private const float limitTest = 1.82138636f;

		/// <summary>
		///     Ключ используемый при шифровании
		/// </summary>
		private byte[] Key = {42, 42, 42, 42, 42, 42, 42, 42};

		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Генерация случайной последовательности нулей и единиц с помощью
		///     алгоритма Yarrow-160 при нажатии на кнопку
		/// </summary>
		private void GenerateYarrow160_Click(object sender, EventArgs e)
		{
			// очищаем все данные на случай повторной генерации
			Xi.Clear();
			Vi.Clear();
			Ci.Clear();
			Si.Clear();
			OutputRandom.Clear();
			// размер сообщения, всегда 64 т.к. для шифрования используется алгоритм DES
			const int n = 64;
			// количество бит, после генерации которых нужно обновить значение k
			// 0 < Pg < 2^(n/3), some Pg = 10
			const int Pg = 10;
			// количество бит, после генерации которых нужно запустить механизм обновления ключа k
			// и счетчика Ci, используя накопитель энтропии
			// Pt > Pg
			const int Pt = 20;
			// количество запусков механизма обновления ключа и счетчика
			int t = 0;
			// в качестве "случайных" данных будем использовать значение текущей секунды
			int v = DateTime.Now.Second;
			// n-битный счетчик - начальное значение берется из numeric бара
			Ci.Add((int) bitCounter.Value);
			int curPg = Pg;
			int curPt = Pt;
			// количество слов (берется из numeric бара в интерфейсе программы)
			var m = (int) mNumeric.Value;
			// цикл описанный в шаге 2
			for (int i = 1; i <= m; i++)
			{
				// шаг 2.1
				if (curPg == 0)
				{
					// генерируем k бит для ключа
					Key = G(i, n);
					// сброс счетчика curPg
					curPg = Pg;
				}
				// шаг 2.2
				if (curPt == 0)
				{
					// вычисляем Vo
					if (Vi.Count == 0)
					{
						Vi.Add(h(Convert.ToString(v | t)));
					}
					else
					{
						Vi[0] = (h(Convert.ToString(v | t)));
					}
					// вычисляем Vi
					for (int r = 1; r <= t; r++)
					{
						Vi.Add(h(Convert.ToString(Vi[r - 1].Sum(x => x) | Vi[0].Sum(x => x) | r)));
					}
					// вычисляем Key
					Key = H(h(Convert.ToString(Vi[t].Sum(x => x) | Key.Sum(x => x))).Sum(x => x));
					// вычисляем Ci
					Ci[i] = Ek(Convert.ToString(0), Key).Sum(x => x);
					// шаг д
					curPt = Pt;
					curPg = Pg;
					t++;
				}
				// шаг 2.3 - вычисляем следующий блок выходной последовательности
				Xi.Add(G(i, n));
				// шаг 2.4 - уменьшаем счетчики на единицу
				curPg -= 1;
				curPt -= 1;
			}
			// запишем все данные в выходной файл
			var fs = new FileStream("out.txt", FileMode.Create);
			var sw = new StreamWriter(fs);
			// цикл по словам в выходной последовательности
			foreach (var bytese in Xi)
			{
				// цикл по байтам в каждом слове
				foreach (var b in bytese)
				{
					// конвертируем каждый байт в нули и единицы и пишем его в OutputRandom
					ConvertByteAndWriteToOutput(b);
				}
			}

			// записываем все нули и единицы в выходной файл
			foreach (var b in OutputRandom)
			{
				sw.Write(b);
			}
			sw.Close();
			fs.Close();
			// открываем выхдодной файл
			Process.Start("out.txt");
			// делаем доступными кнопки тестов
			button2.Enabled = true;
			button3.Enabled = true;
			button4.Enabled = true;
		}

		/// <summary>
		///     Конвертирует один байт в последовательность из 8 нулей и единиц
		///     и записывает эту последовательность в выходной лист
		/// </summary>
		/// <param name="inputByte">Байт для конвертации</param>
		private static void ConvertByteAndWriteToOutput(byte inputByte)
		{
			for (long ControlNumber = 128; ControlNumber > 0; ControlNumber /= 2) /* в цикле 
		идет поразрядное сравнение контрольного числа и inputByte, каждый раз, когда
		контрольное число делится на 2, единственная единица сдвигается вправо (0010000 => 0001000)
		*/
			{
				if (((ControlNumber & inputByte) == 0)) /* Если результат
					поразрядного сравнения контрольного числа и inputByte
					равен 0 (например 1000 & 00011, в итоге 0000) то результат"0"*/
				{
					OutputRandom.Add(0);
				}

				else if ((ControlNumber & inputByte) != 0) /* Если результат
						поразрядного сравнения конрольного числа и inputByte
						НЕ равен 0 (например 0010 & 0011, в итоге 0010), то результат "1"*/
				{
					OutputRandom.Add(1);
				}
			}
		}

		/// <summary>
		///     Генерирует новый ключ
		/// </summary>
		/// <param name="i">Счетчик главного цикла</param>
		/// <param name="n">Размер шифруемого сообщения</param>
		/// <returns>Новый ключ</returns>
		private byte[] G(int i, int n)
		{
			var result = new byte[64/8];
			BigInteger a = (Ci[i - 1] + 1);
			var b = new BigInteger(Math.Pow(2, n));
			var ci = a%b;
			Ci.Add(ci);
			var l = Ek(Convert.ToString(ci), Key);
			for (int m = 0; m < result.Length; m++)
			{
				result[m] = l[m];
			}
			return result;
		}

		/// <summary>
		///     Алгоритм шифрования, в данном случае DES
		/// </summary>
		/// <param name="input">Шифруемое сообщение</param>
		/// <param name="key">Ключ используемый при шифровании</param>
		/// <returns></returns>
		private byte[] Ek(string input, byte[] key)
		{
			DES DESalg = DES.Create();
			DESalg.Key = key;
			return EncryptTextToMemory(input, DESalg.Key, DESalg.IV);
		}

		/// <summary>
		///     Вспомогательный метод для шифрования DES
		/// </summary>
		/// <param name="input">шифруемое сообщение</param>
		/// <param name="Key">ключ</param>
		/// <param name="IV">вектор инициализации</param>
		/// <returns></returns>
		private static byte[] EncryptTextToMemory(string input, byte[] Key, byte[] IV)
		{
			try
			{
				var mStream = new MemoryStream();
				DES DESalg = DES.Create();
				var cStream = new CryptoStream(mStream,
					DESalg.CreateEncryptor(Key, IV),
					CryptoStreamMode.Write);
				byte[] toEncrypt = new ASCIIEncoding().GetBytes(input);
				cStream.Write(toEncrypt, 0, toEncrypt.Length);
				cStream.FlushFinalBlock();
				byte[] ret = mStream.ToArray();
				cStream.Close();
				mStream.Close();
				return ret;
			}
			catch (CryptographicException)
			{
				return null;
			}
		}

		/// <summary>
		///     Используется для генерации нового ключа
		/// </summary>
		private byte[] H(int s)
		{
			// вычислить So = S
			Si.Add(s);
			// вычислить Si = h (So || ... || Si-1)
			int sConcat = s;
			for (int l = 1; l < Si.Count; l++)
			{
				sConcat = sConcat | Si[l];
			}
			h(Convert.ToString(sConcat));
			// вернуть первые k бит от конкатенации двоичны слов
			for (int l = 1; l < Si.Count; l++)
			{
				sConcat = sConcat | Si[l];
			}
			byte[] toEncrypt = new ASCIIEncoding().GetBytes(Convert.ToString(sConcat));
			var result = new byte[64/8];
			for (int m = 0; m < toEncrypt.Length; m++)
			{
				result[m] = toEncrypt[m];
			}
			return result; //des.
		}

		/// <summary>
		///     Хеширует входную последовательность методом SHA1
		/// </summary>
		/// <param name="input">Входная последовательность</param>
		/// <returns>Полученный хеш</returns>
		private byte[] h(string input)
		{
			byte[] lol = Encoding.UTF8.GetBytes(input.ToString(CultureInfo.InvariantCulture));
			SHA1 sha1 = new SHA1CryptoServiceProvider();
			byte[] g = sha1.ComputeHash(lol);
			return g;
		}

		#region >>> СТАТИСТИЧЕСКИЕ ТЕСТЫ <<<

		/// <summary>
		///     Частотный тест
		/// </summary>
		private void FreqTest_Click(object sender, EventArgs e)
		{
			// входная последовательность 0 и 1 преобразовывается в - 1 и 1
			List<int> result2 = OutputRandom.Select(b => (2*b - 1)).ToList();
			// вычисляется сумма Sn = X1 + X2 + ... + Xn
			int Sn = result2.Sum(x => x);
			// вычисляется статистика S = |Sn| / sqrt(n)
			var S = (Math.Abs(Sn)/Math.Sqrt(result2.Count));
			string info = "\n\nКоличество элементов: n = " + result2.Count + "\nСумма всех элементов : Sn = " + Sn + "\nСтатистика : S = " + S;
			if (S <= limitTest)
			{
				MessageBox.Show(@"Тест пройден успешно!" + info);
			}
			else
			{
				MessageBox.Show(@"Тест провален!" + info);
			}
		}

		/// <summary>
		///     Тест на последовательность одинаковых бит
		/// </summary>
		private void SequenceTest_Click(object sender, EventArgs e)
		{
			double pi = (1f/OutputRandom.Count)*OutputRandom.Sum(x => x);
			int Vn = 1;
			for (int k = 1; k < OutputRandom.Count - 1; k++)
			{
				int Rk = OutputRandom[k] == OutputRandom[k + 1] ? 0 : 1;
				Vn += Rk;
			}
			double S = Math.Abs(Vn - 2*OutputRandom.Count*pi*(1 - pi))/(2*Math.Sqrt(2*OutputRandom.Count)*pi*(1 - pi));
			// вывод результатов
			string info = "\n\nКоличество элементов: n = " + OutputRandom.Count + "\nЧастота: Pi = "
				+ pi + "\nVn = " + Vn + "\nСтатистика : S = " + S;
			if (S <= limitTest)
			{
				MessageBox.Show(@"Тест пройден успешно!" + info);
			}
			else
			{
				MessageBox.Show(@"Тест провален!" + info);
			}
		}

		/// <summary>
		///     Расширенный тест на произвольные отклонения
		/// </summary>
		private void ExtendedTest_Click(object sender, EventArgs e)
		{
			// входная последовательность 0 и 1 преобразовывается в - 1 и 1
			List<int> result2 = OutputRandom.Select(b => (2*b - 1)).ToList();
			int n = result2.Count();
			// вычисляются суммы последовательноо удлиняющихся последовательностей
			var S_stroke = new List<int> {0};
			for (int i = 0; i < n; i++)
			{
				int sum = 0;
				for (int s = 0; s < i; s++)
				{
					sum += result2[s];
				}
				S_stroke.Add(sum);
			}
			S_stroke.Add(0);
			// лист для хранения количества встреч каждого состояния
			var mapStateCount = new Dictionary<int, int>();
			// лист для хранения статистики каждого состояния
			var Yj = new Dictionary<int, double>();
			// вычисляем L = k - 1, где k количество нулей в S_stroke
			int L = S_stroke.Where(x => x == 0).Select(x => x).Count() - 1;
			// вычисляем статистики для каждого состояния
			string allStatistic = "\nCписок всех статистик:\n";
			for (int j = -9; j <= 9; j++)
			{
				if (j != 0)
				{
					mapStateCount.Add(j, S_stroke.Count(x => x == j));
					Yj.Add(j, Math.Abs(mapStateCount[j] - L)/(Math.Sqrt(2*L*(4*(Math.Abs(j)) - 2))));
					allStatistic += "\n" + j + " : " + Yj[j] + " : Встретилось раз : " + mapStateCount[j];
				}
			}
			// худшая статистика
			double S = Yj.Values.Max();
			string info = "\n\nКоличество элементов: n = " + OutputRandom.Count + "\nL = " + L + "\nХудшая статистика : S = " + S + allStatistic;
			if (S <= limitTest)
			{
				MessageBox.Show(@"Тест пройден успешно!" + info);
			}
			else
			{
				MessageBox.Show(@"Тест провален!" + info);
			}
		}

		#endregion

		/// <summary>
		///     Обновляем информацию о числе элементов в label
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mNumeric_ValueChanged(object sender, EventArgs e)
		{
			CountRandom.Text = @"Всего элементов сгенерируется: " + mNumeric.Value*64;
		}

		// ReSharper restore InconsistentNaming
	}
}