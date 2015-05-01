using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Data;
using System.Data.SqlClient;
using System.Data.ProviderBase;

namespace MyFirstWpf
{
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
		Tables.lab_info = lab_info;
		Tables.main_grid = main_grid;
		Msg.Norm("Приложение запущено");

		// show all tables and load data in list
		AccountingServices.DataBaseTableToList();
		Posts.DataBaseTableToList();
		Services.DataBaseTableToList();
		Session.DataBaseTableToList();
		Workers.DataBaseTableToList();
		Rooms.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_AccountingServices_Click(object sender, RoutedEventArgs e)
	{
		AccountingServices.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_Posts_Click(object sender, RoutedEventArgs e)
	{
		Posts.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_Rooms_Click(object sender, RoutedEventArgs e)
	{
		Rooms.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_Services_Click(object sender, RoutedEventArgs e)
	{
		Services.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_Session_Click(object sender, RoutedEventArgs e)
	{
		Session.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void but_Workers_Click(object sender, RoutedEventArgs e)
	{
		Workers.DataBaseTableToList();
		Tables.RefreshToEdit();
	}

	private void save_Click(object sender, RoutedEventArgs e)
	{
		Tables.SaveCurrentTable();
	}

	private void but_search_id_click(object sender, RoutedEventArgs e)
	{
		Session.SearchID(textbox_searhid.Text);
	}

	private void but_search_pass_click(object sender, RoutedEventArgs e)
	{
		Session.SearchPass(textbox_searchpass.Text);
	}

	private void Rowing(object sender, DataGridRowEditEndingEventArgs e)
	{
		Tables.RefreshToEdit();
	}
}
}