using System.Linq;

namespace Spritz_Rider
{
	/// <summary>
	///     ����� ������
	/// </summary>
	internal class Reader
	{
		/// <summary>
		///     ����������� ����� ����������������� � ������
		/// </summary>
		private string _book;

		/// <summary>
		///     ����� ����� �����
		/// </summary>
		private readonly string _file;

		/// <summary>
		///     ����� ����� ����� (������ ������)
		/// </summary>
		public string File
		{
			get { return _file; }
		}

		/// <summary>
		///     ������� ������� � �����
		/// </summary>
		private int _position;

		/// <summary>
		///     ������� ������� � �����
		/// </summary>
		public int Position
		{
			get { return _position; }
			set { _position = value > BookLength ? BookLength : value; }
		}

		/// <summary>
		///     ����� ����� � ��������
		/// </summary>
		public int BookLength
		{
			get { return _book.Length; }
		}

		/// <summary>
		///     �������� �������� �� ����� �����
		/// </summary>
		public int CharactersLeft
		{
			get { return _book.Length - _position; }
		}

		/// <summary>
		///     ����������� ������� ������
		/// </summary>
		/// <param name="file">����� �����</param>
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
		///     ��������� ���� � ����������� ��������� �������
		/// </summary>
		/// <param name="file">����� *.txt-��������� �����</param>
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
		///     ��������� ���� � FB2 �������
		/// </summary>
		/// <param name="file">����� fb2 �����</param>
		// ReSharper disable once UnusedParameter.Local
		private void ReaderFb2(string file)
		{
			// TODO ������� ��� ��� ��������� ������ FB2
			_book = "";
		}

		/// <summary>
		///     �������� ��������� �������
		/// </summary>
		/// <returns>������� �� ����� �� ����� 18 ��������</returns>
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