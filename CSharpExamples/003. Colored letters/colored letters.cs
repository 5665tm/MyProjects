using System;

class Example
{
	static void Main(string[] args)
	{
		Console.Title = "My Rocking App";
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.BackgroundColor = ConsoleColor.Blue;
		Console.WriteLine("*************************************");
		Console.WriteLine("*****WELCOME TO MY ROCKING APP*******");
		Console.WriteLine("*************************************");
		Console.BackgroundColor = ConsoleColor.Red;
		Console.ReadKey();
	}
}
