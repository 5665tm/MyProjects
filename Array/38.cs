using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// создаем входной массив
		int[] input = { 1, -2, 3, -4, 5, -4, 2, -3 };

		// число положительных элементов
		int positive = 0;
		for (int i = 0 ; i < input.Length ; i++) { 
			// узнаем сколько положительных элементов
			if (input[ i ]>0) { 
				positive++;
			}
		}

		// число отрицательных элементов
		int negative = input.Length - positive;

		// создаем массив который хранит неотрицательные значения
		// из входного массива
		int[] positive_arr = new int[ positive ];
		for (int i = 0, p = 0; i < input.Length ; i++) {
			if (input[ i ]>=0) {
				positive_arr[ p ] = input[i];
				p++;
			}
		}

		// создаем массив который хранит отрицательные значения
		// из входного массива
		int[] negative_arr = new int[ negative ];
		for (int i = 0, p = 0; i < input.Length ; i++) {
			if (input[ i ]<0) {
				negative_arr[ p ] = input[i];
				p++;
			}
		}

		// заполняем входной массив положительными элементами
		for (int i = 0; i < positive_arr.Length; i++) {
			input[ i ] = positive_arr[ i ];
		}
		// дописываем в конец входного массива отрицательные элементы
		for (int i = 0; i < negative_arr.Length; i++) {
			input[ i + positive_arr.Length ] = negative_arr[ i ];
		}

		// вывод результатов
		foreach (int x in input) {
			Console.WriteLine(x);
		}
		Console.ReadLine();
	}
}