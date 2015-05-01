using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GraphProject1000
{
	public delegate void VoidDelegate();

	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		/// Словарь -=ключ, значение=- в котором хранятся значения движения фигур
		/// </summary>
		private readonly Dictionary<string, bool?> _transformMap;

		/// <summary>
		/// Делегат который будет выполняться в фоновом потоке и двигать фигуры
		/// </summary>
		private readonly VoidDelegate _transformDelegate;

		/// <summary>
		/// Лист в котором хранятся все фигуры
		/// </summary>
		private readonly List<Shape> _shapesList;

		/// <summary>
		/// Фоновый поток ответсвенный за движение фигур
		/// </summary>
		private readonly Thread _threadTransform;

		/// <summary>
		/// Угол поворота
		/// </summary>
		private int _rotate;

		/// <summary>
		/// Конструктор главного окна
		/// </summary>
		public MainWindow()
		{
			// инициализация формы
			InitializeComponent();

			//заполняем лист фигурами
			_shapesList = new List<Shape>
			{
				EllipseFigure,
				EllipseFigure2,
				EllipseFigure3,
				EllipseFigure4,
				PolyFigure,
				PolyFigure2,
				PolyFigure3,
				PolyFigure4,
				RecFigure,
				RecFigure2,
				RecFigure3,
				RecFigure4
			};

			// раскрашиваем кнопочки
			btRight.Background = Brushes.White;
			btUp.Background = Brushes.White;
			btRotateR.Background = Brushes.White;
			btLeft.Background = Brushes.Green;
			btDown.Background = Brushes.Green;
			btRotateL.Background = Brushes.Green;

			// заполняем словарь парами -=ключ(тип движения), значение(направление)=-
			_transformMap = new Dictionary<string, bool?>
			{
				// Движение по горизонтали. True - направо, null - без движения, False - налево.
				{"horizontal", false},
				// Движение по вертикали. True - вверх, null - без движения, False - вниз.
				{"vertical", false},
				// Поворот. True - направо, null - без поворота, False - налево.
				{"rotate", false}
			};

			// делегат в котором и будет выполняться движение фигур
			_transformDelegate = delegate
			{
				// проверка содержит ли ключ "horizontal" значение отличное от null
				// то есть движемся ли мы по горизонтали?
				if (_transformMap["horizontal"].HasValue)
				{
					// если false то движемся налево, true - направо
					int i = _transformMap["horizontal"].Value ? 3 : -3;
					// двигаем все фигуры в листе
					foreach (var shape in _shapesList)
					{
						Canvas.SetLeft(shape, Canvas.GetLeft(shape) + i);
						// если одна из фигур вышла за границy влево
						if (Canvas.GetLeft(shape) < -600)
						{
							// показываем ее справа
							Canvas.SetLeft(shape, Canvas.GetLeft(shape) + 1200);
						}
						// если одна из фигур вышла за границy вправо
						if (Canvas.GetLeft(shape) > 600)
						{
							// показываем ее слева
							Canvas.SetLeft(shape, Canvas.GetLeft(shape) - 1200);
						}
					}
				}
				// проверка содержит ли ключ "vertical" значение отличное от null
				// то есть движемся ли мы по вертикали?
				if (_transformMap["vertical"].HasValue)
				{
					// если false то движемся вниз, true - вверх
					int i = _transformMap["vertical"].Value ? -3 : 3;
					// двигаем все фигуры в листе
					foreach (var shape in _shapesList)
					{
						Canvas.SetTop(shape, Canvas.GetTop(shape) + i);
						// если одна из фигур вышла за границy вниз
						if (Canvas.GetTop(shape) < -369)
						{
							// показываем ее сверху
							Canvas.SetTop(shape, Canvas.GetTop(shape) + 738);
						}
							// если одна из фигур вышла за границy вверх
						else if (Canvas.GetTop(shape) > 369)
						{
							// показываем ее снизу
							Canvas.SetTop(shape, Canvas.GetTop(shape) - 738);
						}
					}
				}
				// проверка содержит ли ключ "rotate" значение отличное от null
				// то есть вращаемся ли мы?
				if (_transformMap["rotate"].HasValue)
				{
					// если мы вращаемся вправо
					if (_transformMap["rotate"].Value)
					{
						_rotate += 1;
					}
						// если мы вращаемся влево
					else
					{
						_rotate -= 1;
					}
					// вращаем все фигуры в листе
					foreach (var shape in _shapesList)
					{
						shape.RenderTransform = new RotateTransform(_rotate);
					}
				}
			};

			// создаем новый поток, который будет двигать фигуры
			_threadTransform = new Thread(Transform);
			// запускаем поток на исполнение
			_threadTransform.Start();
		}

		/// <summary>
		/// Метод который будет выполняться в фоновом потоке
		/// </summary>
		private void Transform()
		{
			// метод выполняется бесконечно, до тех пор пока не будет закрыто приложение
			while (true)
			{
				// перед тем как снова сдвинуть фигуры, организовываем паузу в 40мс
				Thread.Sleep(40);
				// делаем запрос потоку вызвавшему текущий поток на передвижение фигур
				Dispatcher.BeginInvoke(DispatcherPriority.Input, _transformDelegate);
			}
			// ReSharper disable once FunctionNeverReturns
		}

		#region >>>BUTTON COMMANDS<<<

		/// <summary>
		/// Событие при нажатии на кнопку "Вправо"
		/// </summary>
		private void btRight_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - движемся направо
			SayCommand("horizontal", true, btRight, btLeft);
		}

		/// <summary>
		/// Событие при нажатии на кнопку "Налево"
		/// </summary>
		private void btLeft_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - движемся налево
			SayCommand("horizontal", false, btLeft, btRight);
		}

		/// <summary>
		/// Событие при нажатии на кнопку "Вверх"
		/// </summary>
		private void btUp_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - движемся вверх
			SayCommand("vertical", true, btUp, btDown);
		}

		/// <summary>
		/// Событие при нажатии на кнопку "Вниз"
		/// </summary>
		private void btDown_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - движемся вниз
			SayCommand("vertical", false, btDown, btUp);
		}


		/// <summary>
		/// Событие при нажатии на кнопку "Поворот налево"
		/// </summary>
		private void btRotateL_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - вращаемся налево
			SayCommand("rotate", false, btRotateL, btRotateR);
		}

		/// <summary>
		/// Событие при нажатии на кнопку "Поворот направо"
		/// </summary>
		private void btRotateR_Click(object sender, RoutedEventArgs e)
		{
			// отдаем команду - вращаемся направо
			SayCommand("rotate", true, btRotateR, btRotateL);
		}

		/// <summary>
		/// Отдает новую команду движения/вращения
		/// </summary>
		/// <param name="keyTransform">Ключ типа движения</param>
		/// <param name="typeTransform">Тип движения (в указанную сторону, или в обратную)</param>
		/// <param name="positiveButton">Кнопка от которой пришла команда</param>
		/// <param name="negativeButton">Кнопка ответственная за противоположную команду</param>
		private void SayCommand(string keyTransform, bool typeTransform, Control positiveButton, Control negativeButton)
		{
			// проверка содержит ли ключ значение отличное от null
			// то есть совешаем ли мы движение/вращение?
			if (_transformMap[keyTransform].HasValue)
			{
				// если до этого двигались - останавливаемся
				_transformMap[keyTransform] = null;
				// раскрашиваем кнопки ответственные за перемещение/вращение в белый цвет
				positiveButton.Background = Brushes.White;
				negativeButton.Background = Brushes.White;
			}
				// если мы не двигались/не вращались
			else
			{
				// начинаем двигаться/вращаться в указанную сторону
				_transformMap[keyTransform] = typeTransform;
				// раскрашиваем кнопку от которой пришла команда, в зеленый цвет
				//positiveButton.Background = Brushes.Green;
			}
		}

		#endregion

		/// <summary>
		/// Событие при закрытии главного окна
		/// </summary>
		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// останавливаем поток ответственный за передвижение фигур
			_threadTransform.Abort();
		}
	}
}