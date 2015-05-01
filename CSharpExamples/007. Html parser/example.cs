using System;
using System.Net;
using System.Text;
using System.IO;

class Example
{
	static void Main()
	{
		// адрес книги
		string url_book;
		// путь к входному файлу
		string in_txt =
		  Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) 
		  + "\\ImhoWork\\in.txt";
		// количество книг = количество строк во входном файле
		int number_of_books = System.IO.File.ReadAllLines(in_txt).Length;
		// filein = поток чтения входного файла
		System.IO.StreamReader filein = 
		  new System.IO.StreamReader(@"D:\\Desktop\\ImhoWork\\in.txt");
		// запускаем цикл записи html в выходной файл
		for (int i=0 ; i<number_of_books;)
		{
			// узнаем адрес книги из файла
			url_book = filein.ReadLine();
			// дальше какая то магия
			HttpWebRequest myHttpWebRequest =
			  (HttpWebRequest) HttpWebRequest.Create(url_book);
			HttpWebResponse myHttpWebResponse =
			  (HttpWebResponse)myHttpWebRequest.GetResponse();
			StreamReader myStreamReader =
			  new StreamReader(myHttpWebResponse.GetResponseStream(), Encoding.GetEncoding(65001));
			// filein = поток записи в выходной файл
			using (System.IO.StreamWriter file =
			  new System.IO.StreamWriter(@"D:\Desktop\ImhoWork\out.txt", true))
			{
				// записываем исходный код html в выходной файл out.txt
				file.WriteLine(myStreamReader.ReadToEnd());
				// выводим адрес книги
				Console.WriteLine(url_book);
				// выводим номер книги
				Console.WriteLine("{0}", ++i);
			}
		}
		Console.WriteLine("{0} {1}", in_txt, number_of_books);
		Console.ReadKey();
	}

}
