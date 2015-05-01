using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// Матрица А
		int[,] A = {{9, 4, 3, 8, 3}
		           ,{2, 9, 3, 9, 3}
		           ,{3, 9, 9, 3, 2}
		           ,{2, 9, 5, 9, 7}
		           ,{3, 7, 2, 3, 3}};
		// узнаем сторону матрицы
		int n = Convert.ToInt32(Math.Sqrt(A.Length));
		// заполняем единицами
		for (int r = 0 ; r <  n ; r++) {
			for (int c = 0 ; c < n ; c++) {
				if (r>=n/2 && c < r)
					A[ r, c ] = 1;
			}
		}

		// вывод результатов
		for (int i = 0 ; i < n ; i++) {
			for (int p = 0 ; p < n ; p++) {
				Console.Write(A[ i, p ]);
			}
			Console.WriteLine();
		}
		Console.ReadLine();
	}
}