using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Program
{
	static void Main()
	{
		// Матрица А
		int[,] A = {{1, 4, 3, 8, 3}
		           ,{2, 9, 3, 9, 3}
		           ,{3, 9, 9, 3, 2}
		           ,{2, 9, 5, 9, 1}
		           ,{3, 7, 2, 3, 3}};
		// узнаем сторону матрицы
		int l = Convert.ToInt32(Math.Sqrt(A.Length));
		// индекс
		int index = 0;
		// а вот и наш один цикл
		for ( ; index < A.Length ; index++) { 
			// верхняя строка
			if ((index<=l) 
				// нижняя строка
			    || (index>=A.Length-l)
				// левый столбец
			    || (index%l) == 0
				// правая строка
				|| (index+1)%l == 0) {
				A[ (index/l), index%l ] = 0;
			}
		}
		// выводим результат
		for (int i = 0 ; i < l ; i++) {
			for (int p = 0 ; p < l ; p++) {
				Console.Write(A[i, p]);
			}
			Console.WriteLine();
		}
		Console.ReadLine();
	}
}