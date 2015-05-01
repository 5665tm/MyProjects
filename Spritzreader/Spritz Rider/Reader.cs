using System.Linq;

namespace Spritz_Rider
{
	/// <summary>
	///     Класс чтения
	/// </summary>
	internal class Reader
	{
		/// <summary>
		///     Загруженная книга сконвертированная в строку
		/// </summary>
		private string _book;

		/// <summary>
		///     Адрес файла книги
		/// </summary>
		private readonly string _file;

		/// <summary>
		///     Адрес файла книги (только чтение)
		/// </summary>
		public string File
		{
			get { return _file; }
		}

		/// <summary>
		///     Позиция курсора в книге
		/// </summary>
		private int _position;

		/// <summary>
		///     Позиция курсора в книге
		/// </summary>
		public int Position
		{
			get { return _position; }
			set { _position = value > BookLength ? BookLength : value; }
		}

		/// <summary>
		///     Длина книги в символах
		/// </summary>
		public int BookLength
		{
			get { return _book.Length; }
		}

		/// <summary>
		///     Осталось символов до конца книги
		/// </summary>
		public int CharactersLeft
		{
			get { return _book.Length - _position; }
		}

		/// <summary>
		///     Конструктор объекта чтения
		/// </summary>
		/// <param name="file">Адрес файла</param>
		public Reader(string file)
		{
			_file = file;
			string fileExtension = file.Split('.')[file.Split('.').Count() - 1].ToLower();
			if (fileExtension == "fb2")
			{
				ReaderFb2(file);
			}
			else
			{
				ReaderDefault(file);
			}
		}


		/// <summary>
		///     Загрузчик книг в стандартном текстовом формате
		/// </summary>
		/// <param name="file">Адрес *.txt-подобного файла</param>
		private void ReaderDefault(string file)
		{
			try
			{
				_book = System.IO.File.ReadAllText(file);
				_book = _book.Replace('\n', ' ');
				_book = _book.Replace('\r', ' ');
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
				_book = "";
			}
		}

		/// <summary>
		///     Загрузчик книг в FB2 формате
		/// </summary>
		/// <param name="file">Адрес fb2 файла</param>
		// ReSharper disable once UnusedParameter.Local
		private void ReaderFb2(string file)
		{
			// TODO Вписать код для обработки файлов FB2
			_book = "";
		}

		/// <summary>
		///     Показать следующий отрывок
		/// </summary>
		/// <returns>Отрывок из книги не более 18 символов</returns>
		public string ShowNext()
		{
			try
			{
				int previousPosition = _position;
				int i = 0;
				int r = 0;
				for (; i < 18; i++)
				{
					if (_book[_position + i] == ' ')
					{
						r = i;
					}
				}
				if (r == 0)
				{
					r = 18;
				}
				_position += r;
				string result = _book.Substring(previousPosition, _position - previousPosition);
				result = result.Trim();
				return result;
			}
			catch
			{
				return "";
			}
		}
	}
}