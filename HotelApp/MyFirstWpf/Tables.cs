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
using System.Collections.ObjectModel;

namespace MyFirstWpf
{
/*************************************************
TABLES
*************************************************/
class Tables
{
	static private string path_app = System.IO.Path.Combine(System.IO.Path.GetDirectoryName
		(typeof(MainWindow).Assembly.Location), "AutoLot.mdf");
	static private string connection_string = String.Format
		(@"Data Source=(LocalDB)\v11.0;AttachDbFilename={0};Integrated Security=True", path_app);
	static protected SqlConnection sql_conn = new SqlConnection(connection_string);
	static protected SqlDataAdapter data_adapter;
	static protected DataSet data_set = new DataSet();
	static public Label lab_info;
	static public DataGrid main_grid;
	static protected string current_table = "Rooms";
	static public bool show_grid = true;

	protected static SqlCommand DataBaseToGrid(string table_name)
	{
		sql_conn.Open();
		data_adapter = new SqlDataAdapter("SELECT * FROM " + table_name, sql_conn);
		data_set = new DataSet();
		data_adapter.Fill(data_set, table_name);
		main_grid.AutoGenerateColumns = true;
		main_grid.ItemsSource = data_set.Tables[ table_name ].DefaultView;
		current_table = table_name;
		return new SqlCommand("Select * From " + table_name, sql_conn);
	}

	static public void SaveCurrentTable()
	{
		try {
			SqlCommandBuilder sql_builder = new SqlCommandBuilder(data_adapter);
			sql_builder.QuotePrefix = "[";
			sql_builder.QuoteSuffix = "]";
			data_adapter.Update(data_set, current_table);
			switch (current_table) {
				case "Rooms":
					Rooms.DataBaseTableToList();
					Msg.Ready("Таблица \"Комнаты\" успешно сохранена");
					break;
				case "Posts":
					Posts.DataBaseTableToList();
					Msg.Ready("Таблица \"Должности\" успешно сохранена");
					break;
				case "AccountingServices":
					AccountingServices.DataBaseTableToList();
					Msg.Ready("Таблица \"Учет услуг\" успешно сохранена");
					break;
				case "Session":
					Session.DataBaseTableToList();
					Msg.Ready("Таблица \"Сессии\" успешно сохранена");
					break;
				case "Workers":
					Workers.DataBaseTableToList();
					Msg.Ready("Таблица \"Персонал\" успешно сохранена");
					break;
				case "Services":
					Services.DataBaseTableToList();
					Msg.Ready("Таблица \"Услуги\" успешно сохранена");
					break;
				default:
					break;
			}
		}
		catch (Exception ex) {
			if (ex.Message.Contains("PRIMARY")) {
				Msg.Wrong("Ошибка сохранения таблицы. Одинаковые главные поля недопустимо!");
			}
			else {
				Msg.Wrong("Ошибка сохранения таблицы. Не все поля заполнены!");
			}
		}
	}

	static public void RefreshToEdit()
	{
		switch(current_table) {
			case "Rooms":
				Rooms.RefreshDefaultValue();
				break;
			case "Posts":
				Posts.RefreshDefaultValue();
				break;
			case "AccountingServices":
				AccountingServices.RefreshDefaultValue();
				break;
			case "Session":
				Session.RefreshDefaultValue();
				break;
			case "Workers":
				Workers.RefreshDefaultValue();
				break;
			case "Services":
				Services.RefreshDefaultValue();
				break;
			default:
				break;
		}
	}
}

/*************************************************
ACCOUNTING SERVICES
*************************************************/
class AccountingServices : Tables
{
	public static List<AccountingServices> acc_list =
    new List<AccountingServices>();
	public int acc_id { get; set; }
	public string services_name { get; set; }
	public DateTime execution_date { get; set; }
	public int session_id { get; set; }
	public int room_id { get; set; }
	public int worker_id { get; set; }

	public AccountingServices(int acc_id, string services_name, DateTime execution_date
		, int session_id, int room_id, int worker_id)
	{
		this.acc_id = acc_id;
		this.services_name = services_name;
		this.execution_date = execution_date;
		this.session_id = session_id;
		this.room_id = room_id;
		this.worker_id = worker_id;
	}

	static public void DataBaseTableToList()
	{
		acc_list.Clear();
		SqlCommand cmd = DataBaseToGrid("AccountingServices");
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				DateTime mydata;
				try {
					mydata = DateTime.Parse(Convert.ToString(dr.GetValue(2)));
				}
				catch {
					mydata = DateTime.Parse("1/1/0");
				}
				acc_list.Add(new AccountingServices(Convert.ToInt32(dr.GetValue(0))
					, Convert.ToString(dr.GetValue(1))
					, mydata
					, Convert.ToInt32(dr.GetValue(3))
					, Convert.ToInt32(dr.GetValue(4))
					, Convert.ToInt32(dr.GetValue(5))
				));
			}
		}
		Msg.Norm("Таблица \"Учет услуг\" открыта для просмотра");
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "AccountingServices" ].Columns[ 0 ].DefaultValue
			= (DateTime.Now - new DateTime(2013, 12, 12)).TotalSeconds;
		data_set.Tables[ "AccountingServices" ].Columns[ 1 ].DefaultValue
			= Services.services_list[ Services.services_list.Count - 1 ].service;
		data_set.Tables[ "AccountingServices" ].Columns[ 2 ].DefaultValue
			= DateTime.Now;
		data_set.Tables[ "AccountingServices" ].Columns[ 3 ].DefaultValue
			= Session.session_list[ Session.session_list.Count - 1 ].session_id;
		data_set.Tables[ "AccountingServices" ].Columns[ 4 ].DefaultValue
			= Session.session_list[ Session.session_list.Count - 1 ].room;
		data_set.Tables[ "AccountingServices" ].Columns[ 5 ].DefaultValue
			= AccountingServices.acc_list[ AccountingServices.acc_list.Count - 1 ].worker_id;
	}
}

/*************************************************
POSTS
*************************************************/
class Posts : Tables
{
	public static List<Posts> posts_list = new List<Posts>();
	public string posts { get; set; }
	public string category_of_services { get; set; }

	public Posts(string posts, string category_of_services)
	{
		this.posts = posts;
		this.category_of_services = category_of_services;
	}

	static public void DataBaseTableToList()
	{
		SqlCommand cmd = DataBaseToGrid("Posts");
		posts_list.Clear();
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				posts_list.Add(new Posts(Convert.ToString(dr.GetValue(0))
					, Convert.ToString(dr.GetValue(1))
				));
			}
		}
		Msg.Norm("Таблица \"Должности\" открыта для просмотра");
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "Posts" ].Columns[ 1 ].DefaultValue
			= Services.services_list[ Services.services_list.Count - 1 ].category_of_services;
	}
}

/*************************************************
ROOMS
*************************************************/
class Rooms : Tables
{
	public static List<Rooms> rooms_list = new List<Rooms>();
	public int room_id { get; set; }
	public int level { get; set; }
	public int capacity { get; set; }
	public DateTime busy { get; set; }
	public int cost_in_day { get; set; }

	public Rooms(int room_id, int level, int capacity, DateTime busy, int cost_in_day)
	{
		this.room_id = room_id;
		this.level = level;
		this.capacity = capacity;
		this.busy = busy;
		this.cost_in_day = cost_in_day;
	}

	static public void DataBaseTableToList()
	{
		SqlCommand cmd = DataBaseToGrid("Rooms");
		rooms_list.Clear();
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				DateTime mydata;
				try {
					mydata = DateTime.Parse(Convert.ToString(dr.GetValue(3)));
				}
				catch {
					mydata = DateTime.Parse("1/1/0");
				}
				rooms_list.Add(new Rooms(Convert.ToInt32(dr.GetValue(0))
					, Convert.ToInt32(dr.GetValue(1))
					, Convert.ToInt32(dr.GetValue(2))
					, mydata
					, Convert.ToInt32(dr.GetValue(4))
				));
			}
		}
		Msg.Norm("Таблица \"Комнаты\" открыта для просмотра");
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "Rooms" ].Columns[ 1 ].DefaultValue = 5;
		data_set.Tables[ "Rooms" ].Columns[ 2 ].DefaultValue = 2;
		data_set.Tables[ "Rooms" ].Columns[ 3 ].DefaultValue = null;
		data_set.Tables[ "Rooms" ].Columns[ 4 ].DefaultValue = 800;
	}
}

/*************************************************
SERVICES
*************************************************/
class Services : Tables
{
	public static List<Services> services_list = new List<Services>();
	public string service { get; set; }
	public string category_of_services { get; set; }
	public int service_cost { get; set; }

	public Services(string service, string category_of_services, int service_cost)
	{
		this.service = service;
		this.category_of_services = category_of_services;
		this.service_cost = service_cost;
	}

	static public void DataBaseTableToList()
	{
		SqlCommand cmd = DataBaseToGrid("Services");
		services_list.Clear();
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				services_list.Add(new Services(Convert.ToString(dr.GetValue(0))
					, Convert.ToString(dr.GetValue(1))
					, Convert.ToInt32(dr.GetValue(2))
				));
			}
		}
		Msg.Norm("Таблица \"Услуги\" открыта для просмотра");
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "Services" ].Columns[ 1 ].DefaultValue
			= Posts.posts_list[ Posts.posts_list.Count - 1 ].category_of_services;
		data_set.Tables[ "Services" ].Columns[ 2 ].DefaultValue = 200;
	}
}

/*************************************************
SESSION
*************************************************/
class Session : Tables
{
	public static List<Session> session_list = new List<Session>();
	public int session_id { get; set; }
	public int pasport_num { get; set; }
	public string first_name { get; set; }
	public string last_name { get; set; }
	public int room { get; set; }
	public DateTime start_session { get; set; }
	public DateTime finish_session { get; set; }
	public int cost_session { get; set; }
	public string sex { get; set; }

	public Session(int session_id, int pasport_num, string first_name, string last_name
		, int room, DateTime start_session, DateTime finish_sessison, int cost_session, string sex)
	{
		this.session_id = session_id;
		this.pasport_num = pasport_num;
		this.first_name = first_name;
		this.last_name = last_name;
		this.room = room;
		this.start_session = start_session;
		this.finish_session = finish_session;
		this.cost_session = cost_session;
		this.sex = sex;
	}

	static public void DataBaseTableToList()
	{
		SqlCommand cmd = DataBaseToGrid("Session");
		session_list.Clear();
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				DateTime mydata1;
				DateTime mydata2;
				try {
					mydata1 = DateTime.Parse(Convert.ToString(dr.GetValue(5)));
				}
				catch {
					mydata1 = DateTime.Parse("1/1/0");
				}
				try {
					mydata2 = DateTime.Parse(Convert.ToString(dr.GetValue(6)));
				}
				catch {
					mydata2 = DateTime.Parse("1/1/0");
				}
				session_list.Add(new Session(Convert.ToInt32(dr.GetValue(0))
					, Convert.ToInt32(dr.GetValue(1))
					, Convert.ToString(dr.GetValue(2))
					, Convert.ToString(dr.GetValue(3))
					, Convert.ToInt32(dr.GetValue(4))
					, mydata1
					, mydata2
					, Convert.ToInt32(dr.GetValue(7))
					, Convert.ToString(dr.GetValue(8))
				));
			}
		}
		Msg.Norm("Таблица \"Сессии\" открыта для просмотра");
	}

	static public void SearchID(string id)
	{
		var linq_id = from x in session_list
			where x.session_id == Convert.ToInt32(id)
			select x;
		try {
			if (linq_id.Count() != 0) {
				main_grid.ItemsSource = linq_id;
				Msg.Norm("Информация по ID " + id);
			}
			else {
				Msg.Norm("Информации по ID " + id + " не обнаружено");
			}
		}
		catch {
			Msg.Wrong("Неверный ID для поиска");
		}
	}

	static public void SearchPass(string passport)
	{
		var linq_pass = from x in session_list
			where x.pasport_num == Convert.ToInt32(passport)
			select x;
		try {
			if (linq_pass.Count() != 0) {
				main_grid.ItemsSource = linq_pass;
				Msg.Norm("Информация по номеру паспорта " + passport);
			}
			else {
				Msg.Norm("Информации по номеру паспорта " + passport + " не обнаружено");
			}
		}
		catch {
			Msg.Wrong("Неверный номер паспорта для поиска");
		}
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "Session" ].Columns[ 0 ].DefaultValue
			= (DateTime.Now - new DateTime(2013, 12, 12)).TotalSeconds;
		data_set.Tables[ "Session" ].Columns[ 4 ].DefaultValue
			= Session.session_list[ Session.session_list.Count - 1 ].room;
		data_set.Tables[ "Session" ].Columns[ 5 ].DefaultValue
			= DateTime.Now;
		data_set.Tables[ "Session" ].Columns[ 6 ].DefaultValue
			= DateTime.Now.AddDays(1);
		data_set.Tables[ "Session" ].Columns[ 7 ].DefaultValue = 0;
		data_set.Tables[ "Session" ].Columns[ 8 ].DefaultValue = "М";
	}
}

/*************************************************
WORKERS
*************************************************/
class Workers : Tables
{
	public static List<Workers> workers_list = new List<Workers>();
	public int worker_id { get; set; }
	public string first_name { get; set; }
	public string last_name { get; set; }
	public DateTime date_birth { get; set; }
	public string sex { get; set; }
	public string post { get; set; }

	public Workers(int worker_id, string first_name, string last_name
		, DateTime date_birth, string sex, string post)
	{
		this.worker_id = worker_id;
		this.first_name = first_name;
		this.last_name = last_name;
		this.date_birth = date_birth;
		this.sex = sex;
		this.post = post;
	}

	static public void DataBaseTableToList()
	{
		SqlCommand cmd = DataBaseToGrid("Workers");
		workers_list.Clear();
		using (SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)) {
			while (dr.Read()) {
				DateTime mydata;
				try {
					mydata = DateTime.Parse(Convert.ToString(dr.GetValue(3)));
				}
				catch {
					mydata = DateTime.Parse("1/1/0");
				}
				workers_list.Add(new Workers(Convert.ToInt32(dr.GetValue(0))
					, Convert.ToString(dr.GetValue(1))
					, Convert.ToString(dr.GetValue(2))
					, mydata
					, Convert.ToString(dr.GetValue(4))
					, Convert.ToString(dr.GetValue(5))
				));
			}
		}
		Msg.Norm("Таблица \"Персонал\" открыта для просмотра");
	}

	static public void RefreshDefaultValue()
	{
		data_set.Tables[ "Workers" ].Columns[ 4 ].DefaultValue = "M";
		data_set.Tables[ "Workers" ].Columns[ 5 ].DefaultValue
			= Posts.posts_list[Posts.posts_list.Count - 1].posts;
	}
}
}