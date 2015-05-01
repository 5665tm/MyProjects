using System;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

static class Sort
{
	//Insertion Sort Normal (1, 2, 3)
	static public double[] Ins(double[] a)
	{
		for(int i = 1, j; i < a.Length; i++)
		{
			j = i;
			for (double tos; (j != 0) && (a[j] < a[j-1]); j--)
			{
				tos = a[j];
				a[j] = a[j - 1];
				a[j - 1] = tos;
			}
		}
		return a;
	}

	//Insertion Sort Reverse (3, 2, 1)
	static public double[] InsR(double[] a)
	{
		for(int i = 1, j; i < a.Length; i++)
		{
			j = i;
			for (double tos; (j != 0) && (a[j] > a[j-1]); j--)
			{
				tos = a[j];
				a[j] = a[j - 1];
				a[j - 1] = tos;
			}
		}
		return a;
	}
}

static class ArrayFunctions
{
	static public void ArrayToFile(double[] a, string outfile = "out.txt")
	{
		using (System.IO.StreamWriter fileout =
		  new System.IO.StreamWriter(outfile, true))
		{
			foreach (double x in a)
			{
				fileout.WriteLine(x);
			}
		}
	}

	// InFile To Array (double type)
	static public double[] FileToArray(string infile, int number_of_values)
	{
		// opens a stream for reading
		System.IO.StreamReader filein = new System.IO.StreamReader(infile);

		// create output array
		double[] a;
		a = new double[number_of_values];

		// reading strings and writing to array values
		for(int i = 0; i < number_of_values; i++)
		{
			a[i] = Convert.ToDouble(filein.ReadLine());
		}

		return a;
	}

	// Show Array Elements
	static public void Show(double[] a, bool num = false)
	{
		// show elements
		if (!num) foreach (double x in a) Console.WriteLine(x);
		
		// show number and elements
		else
		{
			int i = 0;
			foreach (double x in a) Console.WriteLine("{0}: {1}", i++, x);
		}
	}
}

class DemoSort
{
	static void Main()
	{
		Console.WriteLine("Please, enter the input filename");

		string infile = Console.ReadLine();
		// number of values = number of strings
		int number_of_values = System.IO.File.ReadAllLines(infile).Length;
		// timer
		var stopwatch = new Stopwatch();
		// array
		double[] ArraySort;
		double[] ArrayOut;
		ArraySort = ArrayFunctions.FileToArray (infile, number_of_values);

		Console.WriteLine("Number of values: {0}", number_of_values);

		stopwatch.Start();
		ArrayOut = Sort.Ins(ArraySort);
		stopwatch.Stop();
		ArrayFunctions.ArrayToFile(ArrayOut);
		Console.WriteLine();
		Console.WriteLine("Sort info: Insertion");
		Console.WriteLine();
		Console.WriteLine("Cycles:\t{0:###,###,###}", stopwatch.ElapsedTicks       );
		Console.WriteLine("Ms:    \t{0:###,###}"    , stopwatch.ElapsedMilliseconds);
		stopwatch.Reset();

		stopwatch.Start();
		ArrayOut = Sort.InsR(ArraySort);
		stopwatch.Stop();
		ArrayFunctions.ArrayToFile(ArrayOut, "out_r.txt");
		Console.WriteLine();
		Console.WriteLine("Sort info: Insertion Reverse");
		Console.WriteLine();
		Console.WriteLine("Cycles:\t{0:###,###,###}", stopwatch.ElapsedTicks       );
		Console.WriteLine("Ms:    \t{0:###,###}"    , stopwatch.ElapsedMilliseconds);
		stopwatch.Reset();

		Console.ReadKey();
	}
}
