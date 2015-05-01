using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// создаем входной массив
		int[] input = { 1, -2, 3, 4, -4, 5, -4, 2, -3};

		// длина текущей последовательности
		int len = 1;
		// максимальная длина последовательности
		int maxlen = 1;
		// бежим по массиву от начала до конца
		for (int i = 1 ; i < input.Length ; i++) {
			// ого, наше число больше предыдущего!
			if (input[ i ] > input[ i - 1 ]) {
				len++;
			}
			// или не больше =( сброс
			else {
				len = 1;
			}
			// если текущая длина последовательности наибольшая
			if (len > maxlen) {
				maxlen = len;
			}
		}
		
		Console.WriteLine(maxlen);
		Console.ReadLine();
	}
}