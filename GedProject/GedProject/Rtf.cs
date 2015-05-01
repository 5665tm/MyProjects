using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

static class Rtf
{
	// что поделать, идиотский формат RTF не поддерживает из коробки
	// нормальной кодировки, будем извращаться.
	// Да здравствует китайский код!
	static public string Encoding(string x)
	{
		x = x.Replace("а","\\'e0");
		x = x.Replace("б","\\'e1");
		x = x.Replace("в","\\'e2");
		x = x.Replace("г","\\'e3");
		x = x.Replace("д","\\'e4");
		x = x.Replace("е","\\'e5");
		x = x.Replace("ё","\\'b8");
		x = x.Replace("ж","\\'e6");
		x = x.Replace("з","\\'e7");
		x = x.Replace("и","\\'e8");
		x = x.Replace("й","\\'e9");
		x = x.Replace("к","\\'ea");
		x = x.Replace("л","\\'eb");
		x = x.Replace("м","\\'ec");
		x = x.Replace("н","\\'ed");
		x = x.Replace("о","\\'ee");
		x = x.Replace("п","\\'ef");
		x = x.Replace("р","\\'f0");
		x = x.Replace("с","\\'f1");
		x = x.Replace("т","\\'f2");
		x = x.Replace("у","\\'f3");
		x = x.Replace("х","\\'f5");
		x = x.Replace("ф","\\'f4");
		x = x.Replace("ц","\\'f6");
		x = x.Replace("ч","\\'f7");
		x = x.Replace("ш","\\'f8");
		x = x.Replace("щ","\\'f9");
		x = x.Replace("ь","\\'fc");
		x = x.Replace("ъ","\\'fa");
		x = x.Replace("ы","\\'fb");
		x = x.Replace("э","\\'fd");
		x = x.Replace("ю","\\'fe");
		x = x.Replace("я","\\'ff");
		x = x.Replace("А","\\'c0");
		x = x.Replace("Б","\\'c1");
		x = x.Replace("В","\\'c2");
		x = x.Replace("Г","\\'c3");
		x = x.Replace("Д","\\'c4");
		x = x.Replace("Е","\\'c5");
		x = x.Replace("Ё","\\'a8");
		x = x.Replace("Ж","\\'c6");
		x = x.Replace("З","\\'c7");
		x = x.Replace("И","\\'c8");
		x = x.Replace("Й","\\'c9");
		x = x.Replace("К","\\'ca");
		x = x.Replace("Л","\\'cb");
		x = x.Replace("М","\\'cc");
		x = x.Replace("Н","\\'cd");
		x = x.Replace("О","\\'ce");
		x = x.Replace("П","\\'cf");
		x = x.Replace("Р","\\'d0");
		x = x.Replace("С","\\'d1");
		x = x.Replace("Т","\\'d2");
		x = x.Replace("У","\\'d3");
		x = x.Replace("Х","\\'d5");
		x = x.Replace("Ф","\\'d4");
		x = x.Replace("Ц","\\'d6");
		x = x.Replace("Ч","\\'d7");
		x = x.Replace("Ш","\\'d8");
		x = x.Replace("Щ","\\'d9");
		x = x.Replace("Ь","\\'dc");
		x = x.Replace("Ъ","\\'da");
		x = x.Replace("Ы","\\'db");
		x = x.Replace("Э","\\'dd");
		x = x.Replace("Ю","\\'de");
		x = x.Replace("Я","\\'df");
		return x;
	}
}