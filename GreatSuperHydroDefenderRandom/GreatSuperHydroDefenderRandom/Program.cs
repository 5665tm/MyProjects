// Changed: 2014 10 19 9:10 PM : 5665tm

using System;
using System.Threading;

namespace GreatSuperHydroDefenderRandom
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var rdm = new Random();
			while (true)
			{
				for (int i = 0; i < 20; i++)
				{
					Console.WriteLine("Loading: " + i*5 + "%");
					Thread.Sleep(rdm.Next(200, 2000));
				}
				Console.WriteLine(rdm.Next(1, 101));
				Console.ReadKey();
			}
		}
	}
}