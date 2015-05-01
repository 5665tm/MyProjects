using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

class Program
{
	// входной файл (без ".ged")
	static private string input_file;
	// массив в котором будут хранится все строки входного файла
	static private string[] array_file;

	static void Main()
	{
		Console.WriteLine("Please enter the name of the input file");
		input_file = Console.ReadLine();
		try {
			// открываем поток чтения из файла
			var sr = new StreamReader(input_file + ".ged", Encoding.Default);

			// узнаем количество строк в файле и создаем соотвествующий массив
			int count = File.ReadAllLines(input_file + ".ged").Length;
			array_file = new string[ count ];
			// считываем все строки в массив
			for (int i = 0 ; !sr.EndOfStream ; i++ ) {
				array_file[ i ] = sr.ReadLine();
			}
			// закрываем поток входного файла
			sr.Close();

			// пробегаемся по всем строкам массива, если видим строку нащинающуюся с "0 @"
			// то отдаем массив на растерзание парсеру указав ему с какой строки парсить
			for (int i = 0 ; i < array_file.Length; i++) {
				if(array_file[i].StartsWith("0 @")) Parse(i);
			}

			// удаляем предыдущий выходной файл
			File.Delete("family_tree.rtf");
			// открываем поток для записи
			FileStream fs = new FileStream("family_tree.rtf", FileMode.CreateNew);
			StreamWriter sw = new StreamWriter(fs, Encoding.Default);
			// пишем заголовок в rtf файл
			sw.WriteLine(@"{\rtf1\ansi\ansicpg1251\deff0\deflang1049{\fonttbl{\f0\fswiss\fprq2\fcharset204{\*\fname Arial;}Arial CYR;}{\f1\fnil\fcharset0 Calibri;}}");
			sw.WriteLine(@"{\*\generator Msftedit 5.41.21.2510;}\viewkind4\uc1\pard\nowidctlpar\hyphpar0\sa160\kerning3\f0\fs24 %\'cf\'ee\'eb\par");

			// прогоняемся по коллекции людей, заодно запишем информацию о них в rtf файл
			foreach (var x in Person.person_dictionary) {
				Console.WriteLine("person:" + x.Key);
				Console.WriteLine("  givn:" + x.Value.givn);
				Console.WriteLine("  surn:" + x.Value.surn);
				Console.WriteLine("  male:" + x.Value.sex_male);
				sw.Write(Rtf.Encoding("gender(\'" + x.Value.givn +"\'-\'" + x.Value.surn + "\'," + (x.Value.sex_male ? "m" : "f")));
				sw.WriteLine(").\\par");
			}

			sw.WriteLine("\\par");
			sw.Write(Rtf.Encoding("%Кто с кем в браке. Слева мужчина, справа женщина."));
			sw.WriteLine("\\par");
			// прогоняемся по коллекции семей, заодно запишем информацию о том кто на ком женат
			foreach (var x in Family.family_dictionary) {
				try {
					sw.Write(Rtf.Encoding("marriage(\'" + x.Value.husband.givn + "\'-\'" + x.Value.husband.surn));
				}
				catch { 
					sw.Write(Rtf.Encoding("marriage(\'" + "NOT_PEOPLE"));
				}
				try {
					sw.WriteLine(Rtf.Encoding("\',\'" + x.Value.wife.givn + "\'-\'" + x.Value.wife.surn) + "\').\\par");
				}
				catch { 
					sw.WriteLine(Rtf.Encoding("\',\'" + "NOT_PEOPLE" + "\'-\'") + "\').\\par");
				}
			}

			sw.WriteLine("\\par");
			sw.WriteLine("\\par");
			sw.Write(Rtf.Encoding("% у кого какие родители. Отец, мать, ребёнок"));
			sw.WriteLine("\\par");
			// снова прогоняемся по коллекции семей, и запишем информацию о том кто чей ребенок
			foreach (var x in Family.family_dictionary) {
				Console.WriteLine("family:" + x.Key);
				try {
					Console.WriteLine("  husb:" + x.Value.husband.givn + " " + x.Value.husband.surn);
				}
				catch { 
					Console.WriteLine("  husb: NOT_PEOPLE");
				}
				try {
					Console.WriteLine("  wife:" + x.Value.wife.givn + " " + x.Value.wife.surn);
				}
				catch { 
					Console.WriteLine("  wife: NOT_PEOPLE");
				}
				// в каждой семье есть коллекция в которой мы храним всех детей, пробегаемся по ней
				foreach (var n in x.Value.children) {
					Console.WriteLine("    chil:" + n.Key);
					Console.WriteLine("      name:" + n.Value.givn);
					try {
						sw.Write(Rtf.Encoding("parents(\'" + x.Value.husband.givn + "\'-\'" + x.Value.husband.surn));
					}
					catch { 
						sw.Write(Rtf.Encoding("parents(\'" + "NOT_PEOPLE"));
					}
					try {
						sw.Write(Rtf.Encoding("\',\'" + x.Value.wife.givn + "\'-\'" + x.Value.wife.surn));
					}
					catch { 
						sw.Write(Rtf.Encoding("\',\'" + "NOT_PEOPLE"));
					}
					sw.WriteLine(Rtf.Encoding("\',\'" + n.Value.givn + "\'-\'" + n.Value.surn) + "\').\\par");
				}
			}

			// обозначаем конец rtf и закрываем поток записи
			sw.WriteLine(@"}");
			sw.Close();

			Console.WriteLine("\n\n-----------------------\nReady");
			Console.ReadLine();
		}
		// что то пошло не так
		catch (Exception ex){ 
			Console.Write("Oooops. Error: {0}", ex.Message);
			Console.ReadLine();
		}
	}


	// мегапарсер
	static void Parse(int k)
	{
		// определяем ID записи
		string id = array_file[ k ].Split('@')[1];

		// если запись типа "человек"
		if (array_file[ k ].Contains("INDI")) {
			// имя, фамилия и пол
			string givn = null;
			string surn = null;
			bool sex_male = false;

			// пробегаемся по строкам дальше до тех пор пока не встретится идентификатор окончания записи
			for (int i = k ; !array_file[ i ].StartsWith("1 RIN MH") ; i++) {
				// если нашли строку с именем, извлекаем имя
				if (array_file[ i ].StartsWith("2 GIVN ")) {
					givn = array_file[ i ].Split(' ')[2];
				}
				// если нашли строку с фамилией, извлекаем фамилию
				else if (array_file[ i ].StartsWith("2 SURN ")) {
					surn = array_file[ i ].Split(' ')[2];
				}
				// если нашли строку с полом, узнаем пол
				else if (array_file[ i ].StartsWith("1 SEX ")) {
					if (array_file[ i ].Split(' ')[ 2 ] == "M") {
						sex_male = true;
					}
				}
			}
			// добавляем человека в список всех людей
			Person.person_dictionary.Add(id, new Person(givn, surn, sex_male));
		}

		// если запись типа "семья"
		else if (array_file[ k ].Contains("FAM")) {
			// муж, жена, список детей
			Person husband = null;
			Person wife = null;
			Dictionary<string, Person> children = new Dictionary<string,Person>();

			// пробегаемся по строкам дальше до тех пор пока не встретится идентификатор окончания записи
			for (int i = k ; !array_file[ i ].StartsWith("1 RIN MH") ; i++) {
				// если нашли строку с мужем, узнаем его по идентификатору
				if (array_file[ i ].StartsWith("1 HUSB ")) {
					husband = Person.person_dictionary[array_file[ i ].Replace("@", "").Split(' ')[2]];
				}
				// если нашли строку с женой, узнаем ее по идентификатору
				else if (array_file[ i ].StartsWith("1 WIFE ")) {
					wife = Person.person_dictionary[array_file[ i ].Replace("@", "").Split(' ')[2]];
				}
				// если нашли строку с ребенком, добавляем его в список детей
				else if (array_file[ i ].StartsWith("1 CHIL ")) {
					children.Add(array_file[ i ].Replace("@", "").Split(' ')[2],
						Person.person_dictionary[array_file[ i ].Replace("@", "").Split(' ')[2]]);
				}
			}
			// добавляем семью в список всех семей
			Family.family_dictionary.Add(id, new Family(husband, wife, children));
		}
	}
}