// Changed: 2014 09 30 7:38 PM : 5665tm

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace NeuroAnd
{
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public double W03;
		public double W13;
		public double W23;
		public double N;
		private int _counterStep = 1;
		private readonly int[,] _inputCombination = {{0, 0}, {0, 1}, {1, 0}, {1, 1}};
		private int _counterComb;
		private double _net3;
		private int _y;
		private string Step { get { return "-----   Шаг " + _counterStep++ + "   -----\n"; } }
		private readonly bool[] _arrTrue = {false, false, false, false};
		private bool _isBad;

		public MainWindow()
		{
			InitializeComponent();
			ResetButton_Click(null, null);
		}

		private void ResetButton_Click(object sender, RoutedEventArgs e)
		{
			var inputData = new InputData(this);
			inputData.ShowDialog();
			ClearLog();
			_counterStep = 1;
			AppendLog(Step + "Создана нейронная сеть со следующими параметрами:\nВеса:\nW03 = " + W03
				+ "\nW13 = " + W13 + "\nW23 = " + W23 + "\nНорма обучения:\nn = " + N);
			lbX1.Content = "";
			lbX2.Content = "";

			lbOne.Content = "";
			lbNET3Eq.Content = "";
			lbNET3.Content = "";
			lbY.Content = "";
			lbN.Content = "n = " + N;
			for (int i = 0; i < 4; i++)
			{
				_arrTrue[i] = false;
			}
			_isBad = false;
			ShowWeight();
			NextStepButton.IsEnabled = true;
		}

		private void AppendLog(string append)
		{
			string oldText = new TextRange(OutRich.Document.ContentStart, OutRich.Document.ContentEnd).Text;
			var document = new FlowDocument();
			var paragraph = new Paragraph();
			paragraph.Inlines.Add(new Bold(new Run(oldText + append)));
			document.Blocks.Add(paragraph);
			OutRich.Document = document;
			OutRich.ScrollToEnd();
		}

		private void ClearLog()
		{
			var document = new FlowDocument();
			var paragraph = new Paragraph();
			paragraph.Inlines.Add(new Bold(new Run("")));
			document.Blocks.Add(paragraph);
			OutRich.Document = document;
		}

		private void NextStepButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_isBad)
			{
				for (int i = 0; i < 4; i++)
				{
					if (!_arrTrue[i])
					{
						_counterComb = i;
						break;
					}
				}
			}
			int x1 = _inputCombination[_counterComb, 0];
			int x2 = _inputCombination[_counterComb, 1];
			AppendLog(Step + "\nПроверим комбинацию " + _counterComb
				+ " [" + x1 + " : " + x2 + "]");
			_net3 = x1*W13 + x2*W23 + 1*W03;
			_y = _net3 >= 0 ? 1 : 0;
			lbX1.Content = Convert.ToString(x1);
			lbX2.Content = Convert.ToString(x2);
			lbNET3.Content = Convert.ToString(_net3);
			lbOne.Content = "1";
			lbY.Content = "Y = " + _y;
			lbNET3Eq.Content = "net3 = " + x1 + "*" + W13 + " + " + x2 + "*" + W23 + " + " + "1*" + W03;
			AppendLog(Convert.ToString(lbNET3Eq.Content) + " = " + _net3);
			AppendLog(_net3 >= 0 ? "Т.к net3 >= 0, то y = 1" : "Т.к net3 < 0, то y = 0");
			bool result = Convert.ToBoolean(x1) && Convert.ToBoolean(x2);
			AppendLog("Ожидаемый результат : " + Convert.ToInt32(result));
			if ((Convert.ToBoolean(x1) && Convert.ToBoolean(x2)) == Convert.ToBoolean(_y))
			{
				AppendLog("ВЕРНО");
				_arrTrue[_counterComb] = true;
				lbY.Background = Brushes.DarkSeaGreen;
				bool succes = true;
				foreach (var b in _arrTrue)
				{
					if (!b)
					{
						succes = false;
						break;
					}
				}
				if (succes)
				{
					AppendLog("\n--------------------\nПоздравляем! На этот раз все выборки оказались верны!\nВы обучили нейронную сеть епта!");
					NextStepButton.IsEnabled = false;
				}
				_isBad = false;
			}
			else
			{
				AppendLog("НЕВЕРНО");
				AppendLog("Определим коэффициент ошибки");
				double error = Convert.ToInt32(result) - _y;
				AppendLog("б = t - y = " + Convert.ToInt32(result) + " - " + _y + " = " + error);
				AppendLog("Посчитаем новые веса для входов где присутствовала единица:");
				SetNewWeight(ref W03, error, 1);
				AppendLog("∆W03 = n * 1 * б1 = " + N + "*1*" + error + " = " + N*1*error);
				AppendLog("W03 = W03 + ∆W03 = " + W03);
				if (x1 == 1)
				{
					SetNewWeight(ref W13, error, x1);
					AppendLog("∆W13 = n * x1 * б1 = " + N + "*" + x1 + "*" + error + " = " + N*x1*error);
					AppendLog("W13 = W13 + ∆W13 = " + W13);
				}
				if (x2 == 1)
				{
					SetNewWeight(ref W23, error, x2);
					AppendLog("∆W23 = n * x2 * б1 = " + N + "*" + x2 + "*" + error + " = " + N*x2*error);
					AppendLog("W23 = W23 + ∆W23 = " + W23);
				}
				lbY.Background = Brushes.IndianRed;
				AppendLog("Считаем заново эту и остальные комбинации");
				_isBad = true;
				for (int i = 0; i < 4; i++)
				{
					_arrTrue[i] = false;
				}
			}
			ShowWeight();
		}

		private void SetNewWeight(ref double weight, double error, int x)
		{
			double deltaX = N*x*error;
			weight = weight + deltaX;
		}

		private void ShowWeight()
		{
			lbW03.Content = "W03 = " + W03;
			lbW13.Content = "W13 = " + W13;
			lbW23.Content = "W23 = " + W23;
		}
	}
}