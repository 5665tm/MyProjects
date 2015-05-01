using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
	static void Main()
	{
		// изначальный массив
		int[] input = { 1, 2, 3, 4, 5, 4, 2, 2 };
		// среднее = конвертация в дробное число суммы элементов входного массива / длина входного массива
		double mean = Convert.ToDouble(input.Sum()) / input.Length;
		// переменная длины выходного массива
		int size_output = 0;
		// пробегаемся по входному массиву, считаем количество элементов ниже среднего
		foreach (int x in input) {
			if (x<mean)
				size_output++;
		}
		// создаем выходной массив длина которого = количество элементов ниже среднего
		int[] output = new int[ size_output ];
		// назначаем выходному массиву индексы входного массива
		for (int i = 0, k = 0 ; i<input.Length ; i++) {
			if (input[ i ] < mean) {
				output[ k ] = i;
				k++;
			}
		}
		// вывод результатов
		foreach (int x in output) {
			Console.WriteLine(x);
		}
		Console.ReadLine();
	}
}