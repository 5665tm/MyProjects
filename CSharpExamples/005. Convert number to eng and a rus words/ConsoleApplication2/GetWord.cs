using System;

static class GetWord
{
	static public string Go(int Number, bool mode_ru = false)
	{
		if (Number == 0)
		{
			if (mode_ru) return "ноль";
			return "zero";
		}

		if (mode_ru) return Ru(Number);
		return En(Number);
	}

	static private string Ru(int num)
	{
		int Digit = 0;
		string Word = "";
		if (num < 0) // число меньше нуля? 
		{
			Word += "минус "; // если меньше, то припишем спереди минус... 
			num = Math.Abs(num); // ...и сделаем его положительным для дальнейшей работы 
		}

		// выводим на экран значение 4 разряда (тысячи)
		// для чисел 1000...9999, если число меньше, то блок пропускается, и переходим к сотням
		if (num >= 1000)
		{
			Digit = num / 1000; // узнаем значение 4 разряда (для 4196 это 4)
			num %= Digit * 1000; // вычисляем значения 3,2,1 разрядов (для 4196 это 196)

			switch (Digit)
			{
				case 1:
					Word += "одна тысяча ";
					break;
				case 2:
					Word += "две тысячи ";
					break;
				case 3:
				case 4:
					Word += (DigToWord(Digit, true) + " тысячи ") ;
					break;
				default:
					Word += (DigToWord(Digit, true) + " тысяч ");
					break;
			}
		}

		// выводим на экран значение 3 разряда (сотни)
		// для чисел 100...999 (если изначально было введено 4196, то сейчас оно равно 196)
		if (num >= 100)
		{
			Digit = num / 100; // узнаем значение 3 разряда (для 196 это 1)
			num %= Digit * 100; // вычисляем значения 2,1 разрядов (для 196 это 96)

			switch (Digit)
			{
				case 1:
					Word += "сто ";
					break;
				case 2:
					Word += "двести ";
					break;
				case 3:
				case 4:
					Word += (DigToWord(Digit, true) + "ста ");
					break;
				default:
					Word += (DigToWord(Digit, true) + "сот ");
					break;
			}
		}

		// выводим на экран значение 2 и 1 разряда (десятки и единицы)
		//для чисел 20...99 и 1...9 (если изначально было введено 4196, то сейчас оно равно 96)
		if (num >= 20 || ((num <= 9) && (num >= 1)))
		{
			if (num >= 20) // выводим на экран значение 2 разряда (десятки) для 20...99
			{
				Digit = num / 10; // узнаем значение 2 разряда (для 96 это 9)
				num %= Digit * 10; // вычисляем значения 1 разряда (для 96 это 6)

				switch (Digit)
				{
					case 2:
					case 3:
						Word += (DigToWord(Digit, true) + "дцать ");
						break;
					case 4:
						Word += "сорок ";
						break;
					case 9:
						Word += "девяносто ";
						break;
					default:
						Word += (DigToWord(Digit, true) + "десят ");
						break;
				}
			}
			if (num >= 1) // выводим на экран значение 1 разряда (единицы) для 1...9
			{
				Digit = num; // узнаем значение 1 разряда (для 6 это 6)
				Word += DigToWord(Digit, true);
			}

		}

		// выводим на экран значение 2 и 1 разряда (десятки и единицы) для чисел 10...19
		// если изначально было введено число 8516, то сейчас это значение равно 16
		if (num >= 10) // исключаем вход в данный блок из предыдущего блока
		{
			Digit = num % 10; // вычисляем значения 1 разряда (для 16 это 6). 2ой разряд = 1		

			switch (Digit)
			{
				case 0:
					Word +="десять";
					break;
				case 2:
					Word +="двенадцать";
					break;
				case 1:
				case 3:
					Word += (DigToWord(Digit, true) + "надцать");
					break;
				default:
					Word += (DigToWord(Digit, true) + "\bнадцать");
					break;
			}
		}
		return Word;
	}

	static private string En(int num)
	{
		int Digit = 0;
		string Word = "";
		if (num < 0) // число меньше нуля? 
		{
			Word += "minus "; // если меньше, то припишем спереди минус... 
			num = Math.Abs(num); // ...и сделаем его положительным для дальнейшей работы 
		}

		// выводим на экран значение 4 разряда (тысячи)
		// для чисел 1000...9999, если число меньше, то блок пропускается, и переходим к сотням
		if (num >= 1000)
		{
			Digit = num / 1000; // узнаем значение 4 разряда (для 4196 это 4)
			num %= Digit * 1000; // вычисляем значения 3,2,1 разрядов (для 4196 это 196)

			switch (Digit)
			{
				case 1:
					Word += "a thousand ";
					break;
				default:
					Word += (DigToWord(Digit) + " thousand ");
					break;
			}
			if ((num != 0) && (num < 100)) Word += "and ";
		}

		// выводим на экран значение 3 разряда (сотни)
		// для чисел 100...999 (если изначально было введено 4196, то сейчас оно равно 196)
		if (num >= 100)
		{
			Digit = num / 100; // узнаем значение 3 разряда (для 196 это 1)
			num %= Digit * 100; // вычисляем значения 2,1 разрядов (для 196 это 96)
			switch (Digit)
			{
				case 1:
					Word += "a hundred ";
					break;
				default:
					Word += (DigToWord(Digit) + " hundred ");
					break;
			}
			if (num != 0) Word += "and ";
		}

		// выводим на экран значение 2 и 1 разряда (десятки и единицы)
		//для чисел 20...99 и 1...9 (если изначально было введено 4196, то сейчас оно равно 96)
		if (num >= 20 || ((num <= 9) && (num >= 1)))
		{
			if (num >= 20) // выводим на экран значение 2 разряда (десятки) для 20...99
			{
				Digit = num / 10; // узнаем значение 2 разряда (для 96 это 9)
				num %= Digit * 10; // вычисляем значения 1 разряда (для 96 это 6)

				switch (Digit)
				{
					case 2:
						Word += "twenty";
						break;
					case 3:
						Word += (DigToWord(Digit) + "thirty");
						break;
					case 4:
						Word += "forty";
						break;
					case 5:
						Word += "fifty";
						break;
					case 8:
						Word += "eighty";
						break;
					default:
						Word += (DigToWord(Digit) + "ty");
						break;
				}
				if (num != 0) Word += "-";
			}

			if (num >= 1) // выводим на экран значение 1 разряда (единицы) для 1...9
			{
				Digit = num; // узнаем значение 1 разряда (для 6 это 6)
				Word += DigToWord(Digit);
			}

		}

		// выводим на экран значение 2 и 1 разряда (десятки и единицы) для чисел 10...19
		// если изначально было введено число 8516, то сейчас это значение равно 16
		if (num >= 10) // исключаем вход в данный блок из предыдущего блока
		{
			Digit = num % 10; // вычисляем значения 1 разряда (для 16 это 6). 2ой разряд = 1		

			switch (Digit)
			{
				case 0:
					Word += "ten";
					break;
				case 1:
					Word += "eleven";
					break;
				case 2:
					Word += "twelve";
					break;
				case 3:
					Word += "thirteen";
					break;
				case 5:
					Word += "fourteen";
					break;
				case 8:
					Word += "eighteen";
					break;
				default:
					Word += (DigToWord(Digit) + "teen");
					break;
			}
		}
		return Word;
	}
	
	static private string DigToWord(int Digit, bool ru = false)
	{
		switch (Digit)
		{
			case 1:
				if (ru) return "один";
				return "one";
			case 2:
				if (ru) return "два";
				return "two";
			case 3:
				if (ru) return "три";
				return "three";
			case 4:
				if (ru) return "четыре";
				return "four";
			case 5:
				if (ru) return "пять";
				return "five";
			case 6:
				if (ru) return "шесть";
				return "six";
			case 7:
				if (ru) return "семь";
				return "seven";
			case 8:
				if (ru) return "восемь";
				return "eight";
			default:
				if (ru) return "девять";
				return "nine";
		}
	}
}