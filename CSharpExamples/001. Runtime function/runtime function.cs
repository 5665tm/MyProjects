using System;
using System.Diagnostics; // необходимые...
using System.Threading;   // ...библиотеки

class Example
{
	static void Main()
	{
		var stopwatch = new Stopwatch(); // создаем таймер
		stopwatch.Reset();               // так делается сброс таймера
		stopwatch.Start();               // запускаем таймер
		
		double id, i = 100;                         // функции, время выполнения...
		for (id = 0;id<i;id++) Console.WriteLine(id); // ...которых мы хотим измерить
		
		stopwatch.Stop(); // остановка таймера
		Console.WriteLine(); // далее вывод результатов...
		Console.WriteLine("Тактов:\t{0:###,###,###}", stopwatch.ElapsedTicks       );
		Console.WriteLine("Мс:    \t{0:###,###}"    , stopwatch.ElapsedMilliseconds);
		
		Console.ReadKey();
	}
}