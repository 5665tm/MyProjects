// Created : 2014 03 27 8:01 PM : AppForWorking : AppForWorking
// Changed : 2014 03 28 3:58 AM : Вадим Караваев

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace AppForWorking
{
	public delegate void SendDelegate();


	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Client Cl;

		private SendDelegate _refreshDelegate;
		private Dictionary<int, Button> _mapPlayers = new Dictionary<int, Button>();

		public MainWindow()
		{
			InitializeComponent();
			_refreshDelegate =
				delegate
				{
					string position = Me.Margin.Left + " " + Me.Margin.Top;
					Cl.SendAsync(position);
					// OH MY GOD! ! ! ! 
					if (Info.Content.ToString().Contains('|') && Info.Content.ToString().Contains(':'))
					{
						string[] playersInfo = Info.Content.ToString().Split('|')[1].Split(':');
						var playersId = new List<int>();
						foreach (var s in playersInfo)
						{
							try
							{
								playersId.Add(Convert.ToInt32(s.Split(' ')[0]));
								if (_mapPlayers.ContainsKey(Convert.ToInt32(s.Split(' ')[0])))
								{
									_mapPlayers[Convert.ToInt32(s.Split(' ')[0])].Margin =
										new Thickness(Convert.ToInt32(s.Split(' ')[1]), Convert.ToInt32(s.Split(' ')[2]), 0, 0);
								}
								else if (s.Split(' ')[0] != Info.Content.ToString().Split('|')[0])
								{
									Button playerButton = new Button
									{
										Width = 40,
										Height = 40,
										HorizontalAlignment = HorizontalAlignment.Left,
										VerticalAlignment = VerticalAlignment.Top,
										Background = Brushes.Red,
										Margin =
											new Thickness(Convert.ToInt32(s.Split(' ')[1]), Convert.ToInt32(s.Split(' ')[2]), 0, 0),
										Content = "#" + Convert.ToInt32(s.Split(' ')[0])
									};
									playerButton.IsEnabled = true;
									_mapPlayers.Add(Convert.ToInt32(s.Split(' ')[0]), playerButton);
									MainGrid.Children.Add(_mapPlayers[Convert.ToInt32(s.Split(' ')[0])]);
								}
							}
							catch
							{
							}
						}
						List<int> blablabla = new List<int>();
						foreach (var mapPlayer in _mapPlayers.Keys)
						{
							blablabla.Add(mapPlayer);
						}
						foreach (var bla in blablabla)
						{
							if (!playersId.Contains(bla))
							{
								_mapPlayers[bla].Width = 0;
								_mapPlayers.Remove(bla);
							}
						}
					}
				};
			Cl = new Client(this, Info);
			Cl.ConnectAsync("127.0.0.1", 4578);
			Thread m = new Thread(Refresh);
			m.Start();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			Thickness margin = Me.Margin;
			switch (e.Key)
			{
				case Key.W:
					margin.Top -= 5;
					if (margin.Top < 0)
					{
						margin.Top = 0;
					}
					break;
				case Key.A:
					margin.Left -= 5;
					if (margin.Left < 0)
					{
						margin.Left = 0;
					}
					break;
				case Key.S:
					margin.Top += 5;
					if (margin.Top > 350)
					{
						margin.Top = 350;
					}
					break;
				case Key.D:
					margin.Left += 5;
					if (margin.Left > 550)
					{
						margin.Left = 550;
					}
					break;
			}
			Me.Margin = margin;
			string position = margin.Left + " " + margin.Top;
			Title = position;
		}

		private void Refresh()
		{
			Thread.Sleep(500);
			Cl.SendAsync("Joined");
			Thread.Sleep(500);
			while (true)
			{
				Thread.Sleep(50);
				Dispatcher.BeginInvoke(DispatcherPriority.Input,
					_refreshDelegate);
			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			Cl.SendAsync("Disconnect");
		}
	}


	internal class Client
	{
		private Socket _sock;
		private SocketAsyncEventArgs _sockAsyncArgs;
		private byte[] _buff;
		private Label _info;
		private MainWindow _mainWindow;

		public Client(MainWindow mainWindow, Label info)
		{
			_mainWindow = mainWindow;
			_info = info;
			_buff = new byte[1024];
			_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_sockAsyncArgs = new SocketAsyncEventArgs();
			_sockAsyncArgs.Completed += SockAsyncArgs_Completed;
		}

		private void SockAsyncArgs_Completed(object sender, SocketAsyncEventArgs e)
		{
			switch (e.LastOperation)
			{
				case SocketAsyncOperation.Connect:
					ProcessConnect(e);
					break;
				case SocketAsyncOperation.Receive:
					ProcessReceive(e);
					break;
				case SocketAsyncOperation.Send:
					ProcessSend(e);
					break;
			}
		}

		public void ConnectAsync(string address, int port)
		{
			_sockAsyncArgs.RemoteEndPoint = new DnsEndPoint(address, port);
			ConnectAsync(_sockAsyncArgs);
		}

		private void ConnectAsync(SocketAsyncEventArgs e)
		{
			bool willRaiseEvent = _sock.ConnectAsync(e);
			if (!willRaiseEvent)
			{
				ProcessConnect(e);
			}
		}

		private void ProcessConnect(SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				_mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new ThreadStart(() => { _info.Content = ("Connected to " + e.RemoteEndPoint); }));
				_sockAsyncArgs.SetBuffer(_buff, 0, _buff.Length);
			}
			else
			{
				_mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new ThreadStart(() => { _info.Content = "Dont connect to " + e.RemoteEndPoint; }));
			}
		}

		public void SendAsync(string data)
		{
			if (_sock.Connected && data.Length > 0)
			{
				byte[] buff = Encoding.UTF8.GetBytes(data);
				SocketAsyncEventArgs e = new SocketAsyncEventArgs();
				e.SetBuffer(buff, 0, buff.Length);
				e.Completed += SockAsyncArgs_Completed;
				SendAsync(e);
			}
		}

		private void SendAsync(SocketAsyncEventArgs e)
		{
			bool willRaiseEvent = _sock.SendAsync(e);
			if (!willRaiseEvent)
			{
				ProcessSend(e);
			}
		}

		private void ProcessSend(SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				ReceiveAsync(_sockAsyncArgs);
			}
			else
			{
				_mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new ThreadStart(() => { _info.Content = ("Dont send"); }));
			}
		}

		private void ReceiveAsync(SocketAsyncEventArgs e)
		{
			bool willRaiseEvent = _sock.ReceiveAsync(e);
			if (!willRaiseEvent)
			{
				ProcessReceive(e);
			}
		}

		private void ProcessReceive(SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				string str = Encoding.UTF8.GetString(e.Buffer, 0, e.BytesTransferred);
				_mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new ThreadStart(() => { _info.Content = (str); }));
			}
			else
			{
				_mainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Input,
					new ThreadStart(() => { _info.Content = ("Dont recieve"); }));
			}
		}
	}
}