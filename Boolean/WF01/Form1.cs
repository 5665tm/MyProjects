// Last Change: 2014 11 07 9:46 AM

using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WF01
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void BtResult_Click(object sender, EventArgs e)
		{
			try
			{
				string s = TbInput.Text;
				s = s.ToLowerInvariant();
				char[] m = s.ToCharArray();
				// находим все аргументы в выражении
				int argCount = 0;
				for (int i = 0; i < m.Length; i++)
				{
					// возможно это аргумент?
					if (m[i] >= 'a' && m[i] <= 'z')
					{
						// если слева находится не буква
						if (i == 0 || (m[i - 1] < 'a' || m[i - 1] > 'z'))
						{
							// и справа тоже находится не буква
							if (i == m.Length - 1 || (m[i + 1] < 'a' || m[i + 1] > 'z'))
							{
								// значит это аргумент
								// помечаем аргумент буквой z
								m[i] = 'z';
								argCount++;
							}
						}
					}
				}
				var sb = new StringBuilder();
				foreach (var l in m)
				{
					sb.Append(l);
				}
				s = sb.ToString();
				for (int p = 0; p < argCount + 1; p++)
				{
					string standart = null;
					// количество перестановок
					int numberMix = Convert.ToInt32(Math.Pow(2, argCount));
					// проверяем все возможнные комбинации
					for (int i = 0; i < numberMix; i++)
					{
						var expression = s;
						// заменяем все аргументы на новые
						int counterZ = 0;
						for (int k = 0; k < expression.Length; k++)
						{
							if (expression.Substring(k, 1) == "z")
							{
								int rem = 0;
								int div = i;
								for (int r = 0; r <= counterZ; r++)
								{
									div = Math.DivRem(div, 2, out rem);
								}
								expression = expression.Remove(k, 1).Insert(k, Convert.ToString(rem));
								counterZ++;
							}
						}

						// наше выражение?
						bool thisEx = expression.Count(l => l == '0') == p;
						if (thisEx)
						{
							// выясняем значение выражения
							if (!StringToBool(ref expression))
							{
								throw new Exception();
							}
							if (standart == null)
							{
								standart = expression;
							}
							if (expression != standart)
							{
								MessageBox.Show(@"Выражение зависит от перестановок аргументов");
								return;
							}
						}
					}
				}
				MessageBox.Show(@"Выражение сохраняет свое значение при любой перестановке значений аргументов");
			}
			catch (Exception)
			{
				MessageBox.Show(@"Неверное выражение");
			}
		}

		/// <summary>
		///     Преобразует строковое представление в конечное булевое выражение
		/// </summary>
		/// <param name="s">Входное строкове представление</param>
		/// <returns>Выполнено ли преобразование?</returns>
		private bool StringToBool(ref string s)
		{
			try
			{
				// приводим строку к единому формату
				s = s.ToLower();
				s = s.Replace(" ", "");
				s = s.Replace("true", "1");
				s = s.Replace("false", "0");
				s = s.Replace("and", "&");
				s = s.Replace("not", "!");
				s = s.Replace("or", "|");
				s = s.Replace("then", ">");
				// количество левых и правых скобок
				int leftBracketsCount = 0;
				int rightBracketsCount = 0;
				// проверяем расположение скобок
				foreach (var l in s)
				{
					if (l == ')')
					{
						rightBracketsCount++;
						if (rightBracketsCount > leftBracketsCount)
						{
							return false;
						}
					}
					else if (l == '(')
					{
						leftBracketsCount++;
					}
				}
				// проверяем соответствие на количество скобок
				if (leftBracketsCount != rightBracketsCount)
				{
					return false;
				}
				// приводим выражение к виду без скобок
				while (s.Contains("("))
				{
					for (int i = 0; i < s.Length; i++)
					{
						NEWBRACKET:
						if (s.Substring(i, 1) == "(")
						{
							for (int k = i + 1; k < s.Length; k++)
							{
								if (s.Substring(k, 1) == ")")
								{
									string g = s.Substring(i + 1, k - i - 1);
									if (StringNoBracketsToBool(ref g))
									{
										s = s.Remove(i, k - i + 1).Insert(i, g);
										goto END;
									}
									return false;
								}
								if (s.Substring(k, 1) == "(")
								{
									i = k;
									goto NEWBRACKET;
								}
							}
						}
					}
					END:
					;
				}
				// считаем конечный результат
				if (StringNoBracketsToBool(ref s))
				{
					return true;
				}
				return false;
			}
				// что-то пошло не так
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		///     Преобразует строковое представление не имеющее скобок в конечное булевое выражение
		/// </summary>
		/// <param name="expression">входное выражение</param>
		/// <returns>Удачно ли произошло вычисление?</returns>
		private bool StringNoBracketsToBool(ref string expression)
		{
			try
			{
				// отрицание
				expression = expression.Replace("!1", "0");
				expression = expression.Replace("!0", "1");
				// если количество входных символов четное, значит выражение неверно
				if (expression.Length%2 == 0)
				{
					return false;
				}
				if (expression.Any(l => l != '1' && l != '0' && l != '&' && l != '>' && l != '|'))
				{
					return false;
				}
				// конъюнкция
				while (expression.Contains("&"))
				{
					int i = expression.IndexOf("&", StringComparison.Ordinal);
					string ex = expression.Substring(i - 1, 3);
					string newOp;
					switch (ex)
					{
						case "1&1":
							newOp = "1";
							break;
						case "0&1":
							newOp = "0";
							break;
						case "1&0":
							newOp = "0";
							break;
						case "0&0":
							newOp = "0";
							break;
						default:
							return false;
					}
					expression = expression.Remove(i - 1, 3).Insert(i - 1, newOp);
				}
				// дизъюнкция
				while (expression.Contains("|"))
				{
					int i = expression.IndexOf("|", StringComparison.Ordinal);
					string ex = expression.Substring(i - 1, 3);
					string newOp;
					switch (ex)
					{
						case "1|1":
							newOp = "1";
							break;
						case "0|1":
							newOp = "1";
							break;
						case "1|0":
							newOp = "1";
							break;
						case "0|0":
							newOp = "0";
							break;
						default:
							return false;
					}
					expression = expression.Remove(i - 1, 3).Insert(i - 1, newOp);
				}
				// импликация
				while (expression.Contains(">"))
				{
					int i = expression.IndexOf(">", StringComparison.Ordinal);
					string ex = expression.Substring(i - 1, 3);
					string newOp;
					switch (ex)
					{
						case "1>1":
							newOp = "1";
							break;
						case "0>1":
							newOp = "1";
							break;
						case "1>0":
							newOp = "0";
							break;
						case "0>0":
							newOp = "1";
							break;
						default:
							return false;
					}
					expression = expression.Remove(i - 1, 3).Insert(i - 1, newOp);
				}
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}