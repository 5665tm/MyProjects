using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// Матрица А
		int[,] A = {{1, 4, 8, 2, 8, 3, 3, 0}
		           ,{2, 9, 2, 2, 4, 3, 3, 0}
		           ,{3, 9, 1, 2, 1, 2, 9, 0}
		           ,{2, 9, 2, 2, 3, 9, 5, 0}
		           ,{3, 7, 8, 0, 3, 3, 2, 0}};
		// Вектор B
		int[] B = { 1, 1, 1, 1, 1 };
		// определим столбец с минимальным значением на 5 строке
		int min5row = A[4, 0];
		// исключить последний столбец из проверки!
		for (int i = 1 ; i < 6 ; i++) {
			if (A[ 4, i ]<min5row) { 
				min5row = i;
			}
		}
		// сдвигаем столбцы находящиеся правее столбца с минимальным 
		// значением на 5 строке вправо
		for (int column = 7 ; column > min5row + 1 ; column--) { 
			for (int row = 0 ; row < 5 ; row++) {
				A[ row, column ] = A[ row, column - 1 ];
			}
		}
		// втыкаем вектор в матрицу
		for (int i = 0 ; i < 5 ; i++) {
			A[ i, min5row + 1 ] = B[ i ];
		}
		// смотрим результат
		for (int i = 0 ; i < 5 ; i++) {
			for (int p = 0 ; p < 8 ; p++) {
				Console.Write(A[ i, p ]);
			}
			Console.WriteLine();
		}
		Console.ReadLine();
	}
}