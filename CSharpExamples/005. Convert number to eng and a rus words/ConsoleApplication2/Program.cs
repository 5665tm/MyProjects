using System;

class Program
{
	static void Main()
	{
		Random rnd = new Random();
		int i;
		string mode;
		Console.WriteLine("Ru: Вас приветствует программа для изучения чисел");
		Console.WriteLine("    на английском языке. Пожалуйста, выберите");
		Console.WriteLine("    подходящий для вас режим обучения");

		Console.WriteLine();

		Console.WriteLine("En: You are welcomed program for learning numbers");
		Console.WriteLine("    in English. Please select");
		Console.WriteLine("    the appropriate mode of training for you");

		Console.WriteLine();

		Console.WriteLine("1. A Hundred => 100");
		Console.WriteLine("2. A Hundred => Сто");
		Console.WriteLine("3. 100       => A Hundred");
		Console.WriteLine("4. Сто       => A Hundred");

		Console.WriteLine("\nВведите номер режима: ");
		mode = Console.ReadLine();
		Console.WriteLine();

		switch (mode)
		{
			case "1":
				for (; ; )
				{
					Console.WriteLine();
					i = rnd.Next(-9999, 10000);
					Console.WriteLine(GetWord.Go(i));
					Console.ReadKey();
					Console.WriteLine();
					Console.WriteLine(i);
					Console.WriteLine();
					Console.ReadKey();
				}
			case "2":
				for (; ; )
				{
					Console.WriteLine();
					i = rnd.Next(-9999, 10000);
					Console.WriteLine(GetWord.Go(i));
					Console.ReadKey();
					Console.WriteLine();
					Console.WriteLine(GetWord.Go(i, true));
					Console.WriteLine();
					Console.ReadKey();
				}
			case "3":
				for (; ; )
				{
					Console.WriteLine();
					i = rnd.Next(-9999, 10000);
					Console.WriteLine(i);
					Console.ReadKey();
					Console.WriteLine();
					Console.WriteLine(GetWord.Go(i));
					Console.WriteLine();
					Console.ReadKey();
				}
			case "4":
				for (; ; )
				{
					Console.WriteLine();
					i = rnd.Next(-9999, 10000);
					Console.WriteLine(GetWord.Go(i, true));
					Console.ReadKey();
					Console.WriteLine();
					Console.WriteLine(GetWord.Go(i));
					Console.WriteLine();
					Console.ReadKey();
				}
			default:
				Console.WriteLine("Ooops! Кажется вы ввели неверное значение!");
				Console.WriteLine("Своими необдуманными действиями вы сдвинули");
				Console.WriteLine("центр нашей планеты. Мы все умрем!");
				Console.ReadKey();
				Console.Write("\n\nТы грязный ублюдок! Это все из за тебя!");
				Console.ReadKey();
				Console.Write("\rВСТРЕТИМСЯ В АДУ !!!!!!!11111          ");
				Console.ReadKey();
				break;
		}
	}
}