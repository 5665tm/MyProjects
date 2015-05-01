using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// создаем входной массив
		int[] input = { 1, -2, 3, -4, 5, -4, 2, -2 };

		// число положительных элементов
		int positive = 0;
		for (int i = 0 ; i < input.Length ; i++) { 
			// узнаем сколько положительных элементов
			if (input[ i ]>0) { 
				positive++;
			}
		}

		// создаем массив который хранит положительные значения
		// из входного массива
		int[] positive_arr = new int[ positive ];
		for (int i = 0, p = 0; i < input.Length ; i++) { 
			if (input[ i ]>0) { 
				positive_arr[ p ] = input[i];
				p++;
			}
		}
		// сортируем положительные значения
		Array.Sort(positive_arr);

		// изменяем массив
		for (int i = input.Length - 1, p = 0 ; i >= 0; i--) {
			if (input[ i ] > 0) {
				input[ i ] = positive_arr[ p ];
				p++;
			}
		}

		// вывод результатов
		foreach (int x in input) {
			Console.WriteLine(x);
		}
		Console.ReadLine();
	}
}