using System;

class Program
{
	static void Main()
	{
		Console.WriteLine("Введите размер массива");
		// создаем массив через метод Arr.CreateArray()
		int[] a = Arr.CreateArray(Convert.ToInt32(Console.ReadLine()));
		// выводим значения элементов массива
		foreach (int x in a) {
			Console.Write("\t {0}", x);
		}
		// узнаем минимальный элемент массива через метод Arr.FindMin()
		Console.WriteLine("\nМинимальный элемент массива {0}", Arr.FindMin(a));
		// узнаем сумму элементов между первым и последним положительным через Arr.GetSumm()
		Console.WriteLine("Cуммa элементов между первым и последним положительным {0}"
			, Arr.GetSumm(a));
		// сортируем массив и выводим результаты
		a = Arr.SortArray(a);
		foreach (int x in a) {
			Console.Write("\t {0}", x);
		}
		Console.ReadLine();
	}
}

// класс для работы с массивами
// static - означает что невозможно создавать объекты этого класса
static class Arr
{
	// метод для создания массива
	static public int[] CreateArray(int size)
	{
		int[] a = new int[ size ];
		Random rand = new Random();
		// заполняем массив случайными значениями
		for (int i = 0 ; i < size ; i++) {
			a[ i ] = rand.Next(-2, 2);
		}
		return a;
	}

	// метод для поиска минимального элемента в массиве
	// a.Length - возвращает длинну массива
	static public int FindMin(int[] a)
	{
		int min = a[ 0 ];
		for (int i=1 ; i < a.Length ; i++) {
			if (a[ i ]<min) {
				min=a[ i ];
			}
		}
		return min;
	}

	// метод для поиска индекcа первого положительного элемента в массиве
	// private - вызывать можно только из этого класса
	static private int FindFirstPositive(int[] a)
	{
		int index_first_positive = 0;
		for (int i = 0 ; i < a.Length ; i++) {
			if (a[ i ] > 0) {
				index_first_positive = i;
				break;
			}
		}
		return index_first_positive;
	}

	// метод для поиска индекcа последнего положительного элемента в массиве
	// private - вызывать можно только из этого класса
	static private int FindEndPositive(int[] a)
	{
		int index_end_positive = 0;
		for (int i = a.Length - 1 ; i >= 0 ; i--) {
			if (a[ i ] > 0) {
				index_end_positive = i;
				break;
			}
		}
		return index_end_positive;
	}

	// метод для получения суммы элементов массива между первым и последним
	// .. положительным элементом
	static public int GetSumm(int[] a)
	{
		int sum = 0;
		int first = FindFirstPositive(a);
		int end = FindEndPositive(a);
		for (int i = first + 1 ; i < end ; i++)
			sum = sum + a[ i ];
		return sum;
	}

	// метод для перестановки элементов массива что бы вначале были нули
	static public int[] SortArray(int[] a)
	{
		for (int i = 0 ; i < a.Length ; i++) {
			if (a[ i ] != 0) {
				for (int j = i ; j < a.Length ; j++) {
					if (a[ j ] == 0) {
						a[ j ] = a[ i ];
						a[ i ] = 0;
					}
				}
			}
		}
		return a;
	}
}