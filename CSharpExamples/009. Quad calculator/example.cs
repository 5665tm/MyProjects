using System;

class Program
{
	static double a, b, c;
	static double res1, res2;

	static void Main()
	{
		string command;
		Console.WriteLine("Please, enter the initial vales");
		Console.Write("a = ");
		a = Double.Parse(Console.ReadLine());
		Console.Write("b = ");
		b = Double.Parse(Console.ReadLine());
		Console.Write("c = ");
		c = Double.Parse(Console.ReadLine());
		while (true)
		{
			Equa();
			Console.WriteLine(res1);
			Console.WriteLine(res2);
			Console.WriteLine("Input the next command");
			command = Console.ReadLine();
			if (command == "x") break;
			if (command == "a")
			{
				a = Double.Parse(Console.ReadLine());
				continue;
			}
			if (command == "b")
			{
				b = Double.Parse(Console.ReadLine());
				continue;
			}
			if (command == "c")
			{
				c = Double.Parse(Console.ReadLine());
				continue;
			}
		}
	}

	static void Equa()
	{
		res1 = (-b + Math.Sqrt((b*b) - 4d*a*c)) / (2d*a);
		res2 = (-b - Math.Sqrt((b*b) - 4d*a*c)) / (2d*a);
	}
}