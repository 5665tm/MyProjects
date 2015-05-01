// Last Change: 2014 11 13 7:45 PM

using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawLineCanvas
{
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		/// <summary>
		///     Мы рисуем первую линию
		/// </summary>
		private bool _firstLine = true;

		/// <summary>
		///     В настоящий момент мы рисуем линию, а не просто елозим мышкой
		/// </summary>
		private bool _drawingMode;

		/// <summary>
		///     Временная линия
		/// </summary>
		private Line _tempLine;

		/// <summary>
		///     Рисуем вертикальную линию
		/// </summary>
		private bool _isVertical = true;

		/// <summary>
		///     Предыдущая координата X
		/// </summary>
		private double _prevX;

		/// <summary>
		///     Предыдущая координата Y
		/// </summary>
		private double _prevY;

		/// <summary>
		///     Новая координата X
		/// </summary>
		private double _newX;

		/// <summary>
		///     Новая координата Y
		/// </summary>
		private double _newY;

		/// <summary>
		///     Список всех нарисованных линий
		/// </summary>
		private readonly List<LineInfo> _linesList = new List<LineInfo>();

		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		///     Повели мышкой по канвасу
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CnvDraw_MouseMove(object sender, MouseEventArgs e)
		{
			// если левая кнопка нажата, рисуем временную линию
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				// говорим флагу что мы начали рисовать линию
				_drawingMode = true;
				CnvDraw.Children.Remove(_tempLine);
				// мы рисуем первую линию
				if (_firstLine)
				{
					_prevX = e.GetPosition(CnvDraw).X;
					_prevY = 0;
					_newX = _prevX;
					_newY = e.GetPosition(CnvDraw).Y;
				}
					// мы рисуем не первую линию
				else
				{
					// рисуем вертикальную линию
					if (_isVertical)
					{
						_newX = _prevX;
						_newY = e.GetPosition(CnvDraw).Y;
					}
						// рисуем горизонтальную линию
					else
					{
						_newX = e.GetPosition(CnvDraw).X;
						_newY = _prevY;
					}
				}
				_tempLine = new Line
				{
					X1 = _prevX,
					Y1 = _prevY,
					X2 = _newX,
					Y2 = _newY,
					Stroke = Brushes.Red
				};
				CnvDraw.Children.Add(_tempLine);
			}
				// если левая кнопка не нажата, и мы были в режиме рисовашек
			else if (_drawingMode)
			{
				_drawingMode = false;

				// Йухххууу! Мы нарисовали первую линию епта!
				if (_firstLine)
				{
					_firstLine = false;
				}

				// создаем новую линию взамен временной
				var line = new Line
				{
					X1 = _prevX,
					Y1 = _prevY,
					X2 = _newX,
					Y2 = _newY,
					Stroke = Brushes.LightGreen
				};
				CnvDraw.Children.Add(line);
				CnvDraw.Children.Remove(_tempLine);
				_linesList.Add(new LineInfo(_prevX, _prevY, line));

				// запоминаем координаты
				_prevX = _newX;
				_prevY = _newY;

				// переключаем режим рисования на обратный
				_isVertical = !_isVertical;
			}
		}

		/// <summary>
		///     Удаляет последнюю линию по backspace
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Back && !_drawingMode && _linesList.Count > 0)
			{
				int lastIndex = _linesList.Count - 1;
				if (_linesList.Count >= 2)
				{
					_prevX = _linesList[lastIndex].StartX;
					_prevY = _linesList[lastIndex].StartY;
				}
				else
				{
					_prevY = 0;
					_firstLine = true;
				}
				CnvDraw.Children.Remove(_linesList[lastIndex].LineReference);
				_linesList.RemoveAt(_linesList.Count - 1);
				_isVertical = !_isVertical;
			}
		}
	}

	/// <summary>
	///     Информация о линии
	/// </summary>
	internal class LineInfo
	{
		/// <summary>
		///     X координата первой точки
		/// </summary>
		public readonly double StartX;

		/// <summary>
		///     Y координата первой точки
		/// </summary>
		public readonly double StartY;

		/// <summary>
		///     Ссылка на линию
		/// </summary>
		public readonly Line LineReference;

		/// <summary>
		///     Конструктор для создания новой записи о лини
		/// </summary>
		/// <param name="startX">X координата первой точки</param>
		/// <param name="startY">Y координата первой точки</param>
		/// <param name="lineReference">Ссылка на линию</param>
		public LineInfo(double startX, double startY, Line lineReference)
		{
			StartX = startX;
			StartY = startY;
			LineReference = lineReference;
		}
	}
}