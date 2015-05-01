using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Win32;

namespace Spritz_Rider
{
	public delegate void VoidDelegate();

	public partial class MainWindow
	{
		#region >>>FIELDS<<<

		/// <summary>
		///     Читает ли пользователь в данный момент
		/// </summary>
		private bool _isReading;

		/// <summary>
		///     Объект выполняющий чтение с txt файла
		/// </summary>
		private Reader _reader = new Reader(ConfigurationManager.AppSettings["LastFile"]);

		/// <summary>
		///     Повышается ли скорость со временем?
		/// </summary>
		private bool _speedUp = Convert.ToBoolean(ConfigurationManager.AppSettings["SpeedUp"]);

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
		private int _speedLetterReading = Convert.ToInt32(ConfigurationManager.AppSettings["Speed"]);

		/// <summary>
		///     Mаксимальная скорость чтения
		/// </summary>
		private readonly double _maxSpeed = Convert.ToDouble(ConfigurationManager.AppSettings["MaxSpeed"]);

		/// <summary>
		///     Время в мс перед повышением скорости чтения на один символ
		/// </summary>
		private readonly int _timeToUpSpeed = Convert.ToInt32(ConfigurationManager.AppSettings["TimeToUpSpeed"]);

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
		private readonly Configuration _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

		#endregion

		/// <summary>
		///     Конструктор главного окна
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			ShowFilename();
			_reader.Position = Convert.ToInt32(ConfigurationManager.AppSettings["LastPosition"]);
			chSpeedUp.IsChecked = _speedUp;

			// объявляем делегаты чтения и повышения скорости
			_nextReadDelegate = delegate
			{
				if (_reader.BookLength > 0)
				{
					_content = _reader.ShowNext();
					lbOut.Content = _content;
					lbRemTime.Content = "Remaining Time: "
										+ _reader.CharactersLeft/_speedLetterReading/60
										+ " hours "
										+ Math.Floor(Convert.ToDouble(_reader.CharactersLeft)
													/_speedLetterReading%60)
										+ " minutes";
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
			slSpeed.Value = Convert.ToInt32(ConfigurationManager.AppSettings["Speed"]);

			// выполним первый "кадр"
			_nextReadDelegate();

			// стартуем потоки чтения файла и повышения скорости
			_readerThread = new Thread(RefreshOutLabel);
			_upSpeedThread = new Thread(UpSpeed);
			_readerThread.Start();
			_upSpeedThread.Start();
		}

		/// <summary>
		///     Метод для потока обновления содержимого на экране
		/// </summary>
		private void RefreshOutLabel()
		{
			while (true)
			{
				Thread.Sleep(1);
				if (_speedLetterReading > 0)
				{
					try
					{
						// организовываем паузу, длина которой зависит от длины контента и скорости чтения
						for (int i = 0;
							i < Convert.ToInt32(
								(_content.Length*1000)
								/
								(Convert.ToDouble(_speedLetterReading)/10)
								);
							i++)
						{
							Thread.Sleep(6);
						}
						// если пользователь читает, то обновляем содержимое на экране
						if (_isReading && !slReading.IsMouseCaptureWithin)
						{
							Dispatcher.BeginInvoke(DispatcherPriority.Input, _nextReadDelegate);
						}
						_config.AppSettings.Settings["LastPosition"].Value =
							Convert.ToString(_reader.Position);
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
					Dispatcher.BeginInvoke(DispatcherPriority.Input, _incrementSpeedDelegate);
				}
			}
			// ReSharper disable once FunctionNeverReturns
		}


		/// <summary>
		///    Нажатие на кнопку открытия файла
		/// </summary>
		private void BtOpenFile_Click(object sender, RoutedEventArgs e)
		{
			_isReading = false;
			btStartPause.Content = "Start";
			var openFileDialog = new OpenFileDialog();
			bool? result = openFileDialog.ShowDialog();
			if (result == true)
			{
				_reader = new Reader(openFileDialog.FileName);
				_config.AppSettings.Settings["LastFile"].Value = Convert.ToString(openFileDialog.FileName);
				_config.Save();
				ShowFilename();
				_nextReadDelegate();
			}
		}

		/// <summary>
		/// Старт или пауза при нажатии нажатии на кнопку
		/// </summary>
		private void BtStartPause_Click(object sender, RoutedEventArgs e)
		{
			if (_isReading)
			{
				_isReading = false;
				btStartPause.Content = "Start";
			}
			else
			{
				_isReading = true;
				btStartPause.Content = "Pause";
			}
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
			lbSpeed.Content = "Speed: " + Convert.ToString(Convert.ToInt32(slSpeed.Value/6)) + " WPM";
			_speedLetterReading = Convert.ToInt32(slSpeed.Value);
			_config.AppSettings.Settings["Speed"].Value = Convert.ToString(Convert.ToInt32(slSpeed.Value));
			_config.Save();
		}

		/// <summary>
		///     Изменяет позицию в книге при изменении положения слайдера
		/// </summary>
		private void slReading_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			lbReadingPercent.Content =
				Convert.ToInt32(Convert.ToDouble(_reader.Position)/_reader.BookLength*100) + "%";
			// убеждаемся что позицию изменил именно пользователь
			if (slReading.IsMouseCaptureWithin)
			{
				_reader.Position = Convert.ToInt32((slReading.Value/10)*_reader.BookLength);
				_content = _reader.ShowNext();
				lbOut.Content = _content;
				lbRemTime.Content = "Remaining Time: "
									+ _reader.CharactersLeft/_speedLetterReading/60
									+ " hours "
									+ Math.Floor(Convert.ToDouble(_reader.CharactersLeft)
												/_speedLetterReading%60)
									+ " minutes";
				_config.Save();
			}
		}

		/// <summary>
		///     Отображает в строке статуса текущий загруженный файл
		/// </summary>
		private void ShowFilename()
		{
			var arr = _reader.File.Split('\\');
			var bookName = arr[arr.Count() - 1].Split('.')[0];
			lbFilename.Content = _reader.BookLength > 0 ? bookName : "Not File";
		}

		/// <summary>
		///     Включить повышение скорости
		/// </summary>
		private void chSpeedUp_Checked(object sender, RoutedEventArgs e)
		{
			_speedUp = true;
			_config.AppSettings.Settings["SpeedUp"].Value = "True";
			_config.Save();
		}

		/// <summary>
		///     Отключить повышение скорости
		/// </summary>
		private void chSpeedUp_Unchecked(object sender, RoutedEventArgs e)
		{
			_speedUp = false;
			_config.AppSettings.Settings["SpeedUp"].Value = "False";
			_config.Save();
		}
	}
}