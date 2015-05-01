using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// входной массив 5x7
		int[,] a = {{1, -4, 8, 2, 8}
		           ,{8, 9, 5, -3, 0}
		           ,{8, 9, 5, 3, 0}
		           ,{8, -9, 5, 3, 0}
		           ,{8, 9, 5, 3, 0}
		           ,{8, 9, -5, -3, 0}
		           ,{3, -7, 8, 3, 3}};
		// узнаем количество отрицательных чисел
		int size = 0;
		foreach (int x in a) {
			if (x<0) { 
				size++;
			}
		}
		// создаем одномерный массив хранящий отрицательные числа
		int[] output = new int[ size ];
		// индекс для массива
		int index = 0;
		foreach (int x in a) {
			if (x<0) {
				output[ index ] = x;
				index++;
			}
		}
		foreach (int x in output) {
			Console.WriteLine(x);
		}
		Console.ReadLine();
	}
}