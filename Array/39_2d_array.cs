using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// создаем входные массивы
		int[,] a = {{1, 4, 8}
		           ,{8, 9, 5}
		           ,{3, 7, 8}};
		int[,] b = {{0, 1, 2}
		           ,{3, 4, 5}
		           ,{6, 7, 8}};
		int[,] c = {{0, 0, 0}
		           ,{1, 1, 0}
		           ,{1, 1, 1}};
		// определяем n
		int n = Convert.ToInt32(Math.Sqrt(a.Length));
		Console.WriteLine(n);
		// создаем выходной массив
		int[,] output = new int[ n, 3*n ];
		// заполняем значениями из массива a
		for (int i = 0; i < n ; i++ ) {
			for (int k = 0 ; k < n ; k++) {
				output[ i, k ] = a[ i, k ];
			}
		}
		// заполняем значениями из массива b
		for (int i = 0; i < n ; i++ ) {
			for (int k = 0 ; k < n ; k++) {
				output[ i, k + n ] = b[ i, k ];
			}
		}
		// заполняем значениями из массива c
		for (int i = 0; i < n ; i++ ) {
			for (int k = 0 ; k < n ; k++) {
				output[ i, k + (n*2)] = c[ i, k ];
			}
		}
		// выводим результаты
		for (int i = 0; i < n ; i++ ) {
			for (int k = 0 ; k < n*3 ; k++) {
				Console.Write(output[ i, k ]);
			}
			Console.WriteLine();
		}
		Console.ReadLine();
	}
}