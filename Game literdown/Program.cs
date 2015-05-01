using System;
using System.Diagnostics;
using System.Threading;

class Program
{
	static void Main()
	{
		// создаем массив хранящий буквы
		char[] ABC = new char[] {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
		               'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 
		               'V', 'W', 'X', 'Y', 'Z'};
		// создаем массив хранящий строки игрового поля в данный момент времени
		// в конкретном примере высота поля 20 строк,также нарисуем верхнюю и нижнюю границы
		string[] field = new string[20];
		field[0] = "\r\r\r\r\r\r\r--------------------------";
		field[field.Length - 1] = "\r\r\r\r\r\r\r---------------------------";
		// паузя показывающии длительность одного кадра
		int zzz;
		// падающая буква
		char liter;
		// секундомер для измерения скорости реакции
		// информация о нажатой клавише
		ConsoleKeyInfo Key = new ConsoleKeyInfo();
		// ... и рандом для нее
		Random rnd = new Random();
		// цикл для вывода каждой буквы
		// 30 - число упавших букв перед выводом очков
		// одна итерация - одна упавшая в пропасть буква
		int score = 0;
		for (int i = 0; i < 30; i++)
		{
			// рестарт секундомера
			var timekey = new Stopwatch();
			// вот так мы сделаем уменьшение паузы после каждой буквы
			zzz = 200 - (5 * i);
			// какую букву выведем? великий корейский рандом, я выбираю тебя!
			liter = ABC[rnd.Next(0, 26)];
			// этот цикл имитирует падение буквы
			// одна итерация - 1 кадр
			for (int k = 1; k < (field.Length - 1); k++)
			{
				// очищаем консоль
				Console.Clear();
				Console.WriteLine("Ваши очки: {0}", score);
				// еще один цикл - перерисовывает поле
				// одна итерация перерисовка одной строки
				for (int g = 1; g < (field.Length - 1); g++)
				{
					field[g] = "";
				}
				field[k] = Convert.ToString(liter);
				foreach (string x in field) Console.WriteLine("       {0}", x);
				// пауза
				Thread.Sleep(zzz);
				// если была нажата клавиша, то...
				if (Console.KeyAvailable == true)
				{
					// считываем клавишу
					Key = Console.ReadKey(true);
					// если правильно, то...
					if (Key.KeyChar == Char.ToLower(liter))
					{
						// останавливаем таймер
						timekey.Stop();
						// прибавляем определенно количество очков, в зависимости от 
						// времени реакции
						score += (8000 - Convert.ToInt32(timekey.ElapsedMilliseconds))/100;
						// и выходим из цикла
						break;
					}
				}
			}
		}
		Console.ReadKey();
	}
}