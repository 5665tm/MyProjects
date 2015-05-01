using System;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;

namespace Spritz_Rider_WP8
{
	public delegate void VoidDelegate();

	// ReSharper disable once UnusedMember.Global
	public partial class MainPage
	{
		#region >>>FIELDS<<<

		/// <summary>
		///     Читает ли пользователь в данный момент
		/// </summary>
		private bool _isReading;

		/// <summary>
		///     Объект выполняющий чтение с txt файла
		/// </summary>
		private readonly Reader _reader;

		/// <summary>
		///     Повышается ли скорость со временем?
		/// </summary>
		private bool _speedUp;

		/// <summary>
		///     Текущей текст выводимый на экран
		/// </summary>
		private string _content = "Hi!";

		/// <summary>
		///     Cколько мс прошло с последнего увеличения скорости
		/// </summary>
		private int _msToUpSpeedDiv10;

		/// <summary>
		///     Cкорость символов в минуту
		/// </summary>
		private int _speedLetterReading;

		/// <summary>
		///     Mаксимальная скорость чтения
		/// </summary>
		private readonly double _maxSpeed;

		/// <summary>
		///     Время в мс перед повышением скорости чтения на один символ
		/// </summary>
		private readonly int _timeToUpSpeed;

		/// <summary>
		///     Поток в котором обновляется содержимое на экране
		/// </summary>
		private readonly Thread _readerThread;

		/// <summary>
		///     Делегат для обновления содержимого на экране
		/// </summary>
		private readonly VoidDelegate _nextReadDelegate;

		/// <summary>
		///     Поток в котором увеличивается скорость чтения
		/// </summary>
		private readonly Thread _upSpeedThread;

		/// <summary>
		///     Делегат для увеличения скорости
		/// </summary>
		private readonly VoidDelegate _incrementSpeedDelegate;

		/// <summary>
		///     Объект для работы с файлом конфигурации
		/// </summary>
		private readonly IsolatedStorageSettings _config = IsolatedStorageSettings.ApplicationSettings;

		/// <summary>
		///     Листает ли пользователь в данный момент книгу
		/// </summary>
		private bool _slReadingFocused;

		#endregion

		// Конструктор
		public MainPage()
		{
			InitializeSettings();
			_speedUp = Convert.ToBoolean(_config["SpeedUp"] as string);
			_reader = new Reader(_config["LastFile"] as string);
			_timeToUpSpeed = Convert.ToInt32(_config["TimeToUpSpeed"] as string);
			_maxSpeed = Convert.ToDouble(_config["MaxSpeed"] as string);
			_speedLetterReading = Convert.ToInt32(_config["Speed"] as string);
			_reader.Position = Convert.ToInt32(_config["LastPosition"] as string);
			InitializeComponent();
			ShowFilename();
			chSpeedUp.IsChecked = _speedUp;

			// объявляем делегаты чтения и повышения скорости
			_nextReadDelegate = delegate
			{
				if (_reader.BookLength > 0)
				{
					_content = _reader.ShowNext();
					lbOut.Text = _content;
					lbRemTime.Text = "Time Left: "
									+ _reader.CharactersLeft/_speedLetterReading/60
									+ "h "
									+ Math.Floor(Convert.ToDouble(_reader.CharactersLeft)
												/_speedLetterReading%60)
									+ "m";
					slReading.Value = 10*Convert.ToDouble(_reader.Position)/_reader.BookLength;
				}
			};
			_incrementSpeedDelegate = delegate
			{
				if (_speedUp && _reader.Position < _reader.BookLength)
				{
					slSpeed.Value++;
				}
			};

			// настраиваем параметры слайдера ответственного за скорость чтения
			slSpeed.Maximum = _maxSpeed;
			slSpeed.Value = Convert.ToInt32(_config["Speed"]);

			// выполним первый "кадр"
			_nextReadDelegate();

			// стартуем потоки чтения файла и повышения скорости
			_readerThread = new Thread(RefreshOutLabel);
			_upSpeedThread = new Thread(UpSpeed);
			_readerThread.Start();
			_upSpeedThread.Start();

			// Пример кода для локализации ApplicationBar
			//BuildLocalizedApplicationBar();
		}

		private void InitializeSettings()
		{
			if (!_config.Contains("SpeedUp"))
			{
				_config.Add("SpeedUp", "True");
			}
			if (!_config.Contains("LastFile"))
			{
				_config.Add("LastFile", "Not File");
			}
			if (!_config.Contains("TimeToUpSpeed"))
			{
				_config.Add("TimeToUpSpeed", "120000");
			}
			if (!_config.Contains("MaxSpeed"))
			{
				_config.Add("MaxSpeed", "6000");
			}
			if (!_config.Contains("Speed"))
			{
				_config.Add("Speed", "1200");
			}
			if (!_config.Contains("LastPosition"))
			{
				_config.Add("LastPosition", "1");
			}
			_config.Save();
		}

		// Пример кода для сборки локализованной панели ApplicationBar
		//private void BuildLocalizedApplicationBar()
		//{
		//    // Установка в качестве ApplicationBar страницы нового экземпляра ApplicationBar.
		//    ApplicationBar = new ApplicationBar();

		//    // Создание новой кнопки и установка текстового значения равным локализованной строке из AppResources.
		//    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
		//    appBarButton.Text = AppResources.AppBarButtonText;
		//    ApplicationBar.Buttons.Add(appBarButton);

		//    // Создание нового пункта меню с локализованной строкой из AppResources.
		//    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
		//    ApplicationBar.MenuItems.Add(appBarMenuItem);
		//}

		/// <summary>
		///     Метод для потока обновления содержимого на экране
		/// </summary>
		private void RefreshOutLabel()
		{
			while (true)
			{
				Thread.Sleep(10);
				if (_speedLetterReading > 0)
				{
					try
					{
						// организовываем паузу, длина которой зависит от длины контента и скорости чтения
						for (int i = 1;
							i < Convert.ToInt32(
								(_content.Length*1000)
								/
								(Convert.ToDouble(_speedLetterReading)/6)
								);
							i++)
						{
							Thread.Sleep(10);
						}
						// если пользователь читает, то обновляем содержимое на экране
						if (_isReading && !_slReadingFocused)
						{
							Dispatcher.BeginInvoke(_nextReadDelegate);
							_config["LastPosition"] =
								Convert.ToString(_reader.Position);
						}
					}
						// ReSharper disable once EmptyGeneralCatchClause
					catch
					{
					}
				}
			}
			// ReSharper disable once FunctionNeverReturns
		}

		/// <summary>
		///     Метод для потока повышения скорости
		/// </summary>
		private void UpSpeed()
		{
			// ReSharper disable once FunctionNeverReturns
			while (true)
			{
				LABEL__STOP_PROGRESS_IF_NOT_READ:
				Thread.Sleep(10);
				if (_isReading && _speedLetterReading < _maxSpeed)
				{
					// организовываем паузу перед очередным повышением скорости
					while (_msToUpSpeedDiv10*10 < _timeToUpSpeed)
					{
						_msToUpSpeedDiv10++;
						Thread.Sleep(10);
						// если вдруг обнаружили что пользователь перестал читать выходим из цикла
						if (!_isReading)
						{
							goto LABEL__STOP_PROGRESS_IF_NOT_READ;
						}
					}
					_msToUpSpeedDiv10 = 0;
					// повышаем скорость чтения на один символ
					Dispatcher.BeginInvoke(_incrementSpeedDelegate);
				}
			}
			// ReSharper disable once FunctionNeverReturns
		}


		/// <summary>
		///    Нажатие на кнопку открытия файла
		/// </summary>
		private void BtOpenFile_Click(object sender, RoutedEventArgs e)
		{
			//TODO Сюда вписать открытие файла
			_isReading = false;
			lbOut.FontSize = 30;
			// признак паузы
			//var openFileDialog = new OpenFileDialog();
			//bool? result = openFileDialog.ShowDialog();
			//if (result == true)
			//{
			//	_reader = new Reader(openFileDialog.FileName); // создаем объект чтения, передаем в качестве параметра адрес файла который надо прочесть
			//	_config["LastFile"] = Convert.ToString(openFileDialog.FileName); // пишем в конфиг имя последнего файла
			//	_config.Save(); // сохраняем конфиг
			//	_nextReadDelegate(); // показываем первый кадр
			//}
		}

		/// <summary>
		///     Останавливаем потоки обновления содержимого на экране и увеличения скорости чтения
		///     при закрытии окна
		/// </summary>
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			_config.Save();
			_readerThread.Abort();
			_upSpeedThread.Abort();
		}

		/// <summary>
		///     Изменяет скорость чтения при изменении положения слайдера
		/// </summary>
		private void slSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				lbSpeed.Text = "Speed: " + Convert.ToString(Convert.ToInt32(slSpeed.Value/6)) + " WPM";
				_speedLetterReading = Convert.ToInt32(slSpeed.Value);
				_config["Speed"] = Convert.ToString(Convert.ToInt32(slSpeed.Value));
				_config.Save();
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}

		/// <summary>
		///     Изменяет позицию в книге при изменении положения слайдера
		/// </summary>
		private void slReading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lbReadingPercent.Text =
				Convert.ToInt32(Convert.ToDouble(_reader.Position)/_reader.BookLength*100) + "%";
			// убеждаемся что позицию изменил именно пользователь
			if (_slReadingFocused)
			{
				_reader.Position = Convert.ToInt32((slReading.Value/10)*_reader.BookLength);
				_content = _reader.ShowNext();
				lbOut.Text = _content;
				lbRemTime.Text = "Time Left: "
								+ _reader.CharactersLeft/_speedLetterReading/60
								+ "h "
								+ Math.Floor(Convert.ToDouble(_reader.CharactersLeft)
											/_speedLetterReading%60)
								+ "m";
				try
				{
					_config.Save();
				}
					// ReSharper disable once EmptyGeneralCatchClause
				catch
				{
				}
			}
		}

		/// <summary>
		///     Отображает в строке статуса текущий загруженный файл
		/// </summary>
		private void ShowFilename()
		{
			var arr = _reader.File.Split('\\');
			var bookName = arr[arr.Count() - 1].Split('.')[0];
			lbFilename.Text = _reader.BookLength > 0 ? bookName : "Not File";
		}

		/// <summary>
		///     Включить повышение скорости
		/// </summary>
		private void chSpeedUp_Checked(object sender, RoutedEventArgs e)
		{
			_speedUp = true;
			_config["SpeedUp"] = "True";
			try
			{
				_config.Save();
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}

		/// <summary>
		///     Отключить повышение скорости
		/// </summary>
		private void chSpeedUp_Unchecked(object sender, RoutedEventArgs e)
		{
			_speedUp = false;
			_config["SpeedUp"] = "False";
			try
			{
				_config.Save();
			}
				// ReSharper disable once EmptyGeneralCatchClause
			catch
			{
			}
		}

		private void slReading_GotFocus(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
		{
			_slReadingFocused = true;
		}

		private void slReading_LostFocus(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
		{
			_slReadingFocused = false;
		}

		private void brdStartPause_Click(object sender, System.Windows.Input.GestureEventArgs e)
		{
			if (_isReading)
			{
				lbOut.FontSize = 50;
				_isReading = false;
			}
			else
			{
				lbOut.FontSize = 70;
				_isReading = true;
			}
		}
	}
}