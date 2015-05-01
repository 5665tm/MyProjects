using System;





// класс реализующий стек
class Stack
{
	// массивы для хранения строк, чисел и булевых значений
	private string[] stArr_String;
	private int[] stArr_Int;
	private bool[] stArr_Bool;

	// счетчик массива, его размер, и режим стека
	// (строковый, числовой, булевый)
	private int cur = 0;
	private int size;
	private byte mode;

	/*====================================================
			   КОНСТРУКТОР для создания стека
	         public Stack (размер стека, режим)
	=====================================================*/

	public Stack(int size, byte mode)
	{
		this.size = size;
		this.mode = mode;
		Console.WriteLine();
		Console.WriteLine("Стек со следующими параметрами:");
		Console.WriteLine(" количество хранимых значений: {0}", size);

		// в зависимости от режима инициализируем необходимый массив
		// нужного размера
		switch (mode)
		{
			case 1:
				stArr_String = new string[size];
				Console.WriteLine(" тип хранимых значений: строки");
				break;
			case 2:
				stArr_Int = new int[size];
				Console.WriteLine(" тип хранимых значений: целые числа");
				break;
			case 3:
				stArr_Bool = new bool[size];
				Console.WriteLine(" тип хранимых значений: булев тип");
				break;
		}

		Console.WriteLine("успешно создан!", size);
		Console.WriteLine();
	}

	/*====================================================
	          метод для ЗАПОЛНЕНИЯ данными стека
	  public void Push (строка которую нужно обработать)
	=====================================================*/

	public void Push(string value)
	{

		// проверка на наличие свободных мест
		if (cur == size)
		{
			Console.WriteLine("   WARNING: Стек заполнен!");
			return;
		}

		// в зависимости от режима заполняем массив стека значениями
		// перед этим сконвертировав строки в нужный тип

		// обратить внимание на [cur++] - вначале используем индекс,
		// а затем инкрементируем!
		switch (mode)
		{
			case 1:
				stArr_String[cur++] = value;
				break;
			case 2:
				stArr_Int[cur++] = Convert.ToInt32(value);
				break;
			case 3:
				stArr_Bool[cur++] = Convert.ToBoolean(value);
				break;
		}
	}

	/*====================================================
		      метод для ИЗВЛЕЧЕНИЯ данных из стека
	=====================================================*/

	public string Up()
	{
		// проверка на наличие хотя бы одного элемента в стеке
		if (cur == 0)
		{
			string err = "   WARNING: В стеке нет ни одного элемента!";
			return err;
		}

		// извлекаем значение из стека, конвертируем в строку,
		// возвращаем значение
		// от режима зависит в какую ветку case мы пойдем

		// обратить внимание на [--cur] - вначале дикрементируем индекс
		// массива/стека, затем только используем!
		switch (mode)
		{
			case 1:
				return stArr_String[--cur];
			case 2:
				return Convert.ToString(stArr_Int[--cur]);
			default:
				return Convert.ToString(stArr_Bool[--cur]);
		}
	}
}





// класс реализующий управление стеком
class Program
{
	static void Main()
	{
		Console.WriteLine(" 1 для строкового стека");
		Console.WriteLine(" 2 для целочисленного стека");
		Console.WriteLine(" 3 для булевого стека");
		Console.Write("Выберите тип требуемого стека: ");
		byte mode = Convert.ToByte(Console.ReadLine());

		Console.Write("Введите размер требуемого стека: ");
		int size = Convert.ToInt32(Console.ReadLine());

		Stack TestStack = new Stack(size, mode);
		for (; ; )
		{
			Console.Write("1 - для добавления в стек, 2 для извлечения из стека: ");
			byte operation = Convert.ToByte(Console.ReadLine());
			switch (operation)
			{
				case 1:
					TestStack.Push(Console.ReadLine());
					break;
				case 2:
					Console.WriteLine(TestStack.Up());
					break;
			}
		}
	}
}

