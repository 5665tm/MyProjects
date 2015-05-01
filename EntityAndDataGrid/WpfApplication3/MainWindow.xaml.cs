// Created : 2014 02 17 11:02 PM : Вадим Караваев
// Changed : 2014 02 17 11:47 PM : Вадим Караваев

using System.Linq;
using System.Windows;


namespace WpfApplication3
{
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			using (CarEntities context = new CarEntities())
			{
				dataGrid.ItemsSource = context.Cars.ToList();
			}
		}
	}
}