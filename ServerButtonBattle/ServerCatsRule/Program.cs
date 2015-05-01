// Created : 2014 03 27 10:47 PM : AppForWorking : ServerCatsRule
// Changed : 2014 03 28 2:15 AM : Вадим Караваев

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace ServerCatsRule
{
	internal class Program
	{
		private static void Main()
		{
			int port = 4578;
			var srv = new Server();
			srv.Start(port);
			Console.WriteLine("Server started on port {0}, press any key to stop him...", port);
			Console.ReadKey();
			srv.Stop();
		}
	}


	internal class Server
	{
		public List<ClientConnection> ListClient = new List<ClientConnection>();
		private Socket _sock;
		private SocketAsyncEventArgs _acceptAsyncArgs;

		public Server()
		{
			_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_acceptAsyncArgs = new SocketAsyncEventArgs();
			_acceptAsyncArgs.Completed += AcceptCompleted;
		}

		public void Start(int port)
		{
			_sock.Bind(new IPEndPoint(IPAddress.Any, port));
			_sock.Listen(50);
			AcceptAsync(_acceptAsyncArgs);
		}

		private void AcceptAsync(SocketAsyncEventArgs e)
		{
			bool willRaiseEvent = _sock.AcceptAsync(e);
			if (!willRaiseEvent)
			{
				AcceptCompleted(_sock, e);
			}
		}

		private void AcceptCompleted(object sender, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				ListClient.Add(new ClientConnection(e.AcceptSocket));
			}
			e.AcceptSocket = null;
			AcceptAsync(_acceptAsyncArgs);
		}

		public void Stop()
		{
			_sock.Close();
		}
	}


	internal class ClientConnection
	{
		private int _clientNumber;
		private static int _numberOfClients;
		private Socket _sock;
		private SocketAsyncEventArgs _sockAsyncEventArgs;
		private static Dictionary<int, string> _thereMustBeJson = new Dictionary<int, string>();

		public ClientConnection(Socket acceptedSocket)
		{
			_clientNumber = ++_numberOfClients;
			var buff = new byte[1024];
			_sock = acceptedSocket;
			_sockAsyncEventArgs = new SocketAsyncEventArgs();
			_sockAsyncEventArgs.Completed += SockAsyncEventArgs_Completed;
			_sockAsyncEventArgs.SetBuffer(buff, 0, buff.Length);

			ReceiveAsync(_sockAsyncEventArgs);
		}

		public void SockAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
		{
			switch (e.LastOperation)
			{
				case SocketAsyncOperation.Receive:
					ProcessReceive(e);
					break;
				case SocketAsyncOperation.Send:
					ProcessSend(e);
					break;
			}
		}

		public void ReceiveAsync(SocketAsyncEventArgs e)
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
				string strSend = Convert.ToString(_clientNumber) + "|";
				if (!str.Contains("Joined") && !str.Contains("Disconnect"))
				{
					str = _clientNumber + " " + str;
					if (_thereMustBeJson.ContainsKey(Convert.ToInt32(str.Split(' ')[0])))
					{
						_thereMustBeJson[Convert.ToInt32(str.Split(' ')[0])] = str;
					}
					else
					{
						_thereMustBeJson.Add(Convert.ToInt32(str.Split(' ')[0]), str);
					}
					foreach (var s in _thereMustBeJson.Values)
					{
						strSend += s + ":";
					}
				}
				else if (str.Contains("Disconnect"))
				{
					_thereMustBeJson.Remove(_clientNumber);
				}
				Console.WriteLine(strSend);
				SendAsync(strSend);
			}
		}

		private void SendAsync(string data)
		{
			byte[] buff = Encoding.UTF8.GetBytes(data);
			SocketAsyncEventArgs e = new SocketAsyncEventArgs();
			e.Completed += SockAsyncEventArgs_Completed;
			e.SetBuffer(buff, 0, buff.Length);
			SendAsync(e);
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
				ReceiveAsync(_sockAsyncEventArgs);
			}
		}
	}
}