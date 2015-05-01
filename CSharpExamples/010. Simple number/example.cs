using System;

class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Ввод числа:");
		{
			int x = int.Parse(Console.ReadLine());
			if(Simple(x)) Console.WriteLine("Число является простым");
			else Console.WriteLine("Число не является простым");
			Console.ReadKey();
		}
	}

	private static bool Simple(int x)
	{
		if (x == 2) return true;
		if (x % 2 == 0) return false;
		for (int i = 3; i * i <= x; i+=2)
		{
			if (x % i == 0)
			{
				return false;
			}
		}
		return true;
	}
}