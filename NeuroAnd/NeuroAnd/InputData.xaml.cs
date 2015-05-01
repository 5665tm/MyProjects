// Changed: 2014 09 30 15:43 : 5665tm

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NeuroAnd
{
	/// <summary>
	///     Логика взаимодействия для InputData.xaml
	/// </summary>
	public partial class InputData : Window
	{
		private readonly MainWindow _mw;

		public InputData(MainWindow mw)
		{
			InitializeComponent();
			_mw = mw;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Confirm();
		}

		private void TextBox_EnterConfirm(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Confirm();
			}
		}

		private void Confirm()
		{
			string w03Text = tbW03.Text.Replace(',', '.').Replace(" ", "");
			string w13Text = tbW13.Text.Replace(',', '.').Replace(" ", "");
			string w23Text = tbW23.Text.Replace(',', '.').Replace(" ", "");
			string n = tbN.Text.Replace(',', '.').Replace(" ", "");

			try
			{
				var ci = new CultureInfo("ru-Ru") {NumberFormat = {NumberDecimalSeparator = "."}};
				_mw.W03 = Double.Parse(w03Text, NumberStyles.AllowDecimalPoint, ci);
				_mw.W13 = Double.Parse(w13Text, NumberStyles.AllowDecimalPoint, ci);
				_mw.W23 = Double.Parse(w23Text, NumberStyles.AllowDecimalPoint, ci);
				_mw.N = Double.Parse(n, NumberStyles.AllowDecimalPoint, ci);
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("С вашими входными данными что-то не так!\n\n" + ex.Message);
			}
		}

		private void tb_GotMouseCapture(object sender, MouseEventArgs e)
		{
			var tb = (TextBox) sender;
			tb.SelectAll();
		}

		private void tb_GotFocus(object sender, RoutedEventArgs e)
		{
			var tb = (TextBox) sender;
			tb.SelectAll();
		}
	}
}