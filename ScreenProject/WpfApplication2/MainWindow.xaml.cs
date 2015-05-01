using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		// кнопка #1
		private void my_button_Click(object sender, RoutedEventArgs e)
		{
			my_button.Content = Screenice.Screenice.Shot();
		}

		// кнопка #2
		private void my_button_2_Click(object sender, RoutedEventArgs e)
		{
			my_button_2.Content = Screenice.Screenice.Shot(@"C:\");
		}

		// кнопка #3
		private void my_button_3_Click(object sender, RoutedEventArgs e)
		{
			my_button_3.Content = Screenice.Screenice.Shot(@"C:\", "myfile", "png");
		}
	}
}