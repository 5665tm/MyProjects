// Changed 2014 08 30 2:49 PM Karavaev Vadim

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkApiDemo
{
	public delegate void IncrementProgress();

	public delegate void CloseApp();

	public delegate void SetMaximum();

	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private string _userToken;
		private string _userIdText;
		private int _max;
		private const string _APP_ID = "4529437";
		private const string _NEW_TEXT_STATUS = "VK API Demo 5.24 - Glory robots!";
		private readonly IncrementProgress _incrementProgress;
		private readonly CloseApp _closeApp;
		private readonly SetMaximum _setMaximum;

		public MainWindow()
		{
			InitializeComponent();
			_incrementProgress = delegate
			{
				Progress.Value++;
				Title = Convert.ToString(Progress.Value) + " from " + Convert.ToString(Progress.Maximum);
			};
			_setMaximum = delegate { Progress.Maximum = _max; };
			_closeApp = Close;
		}

		private void Autorize_Click(object sender, RoutedEventArgs e)
		{
			const string reqStrTemplate = "https://oauth.vk.com/authorize?client_id=" + _APP_ID
				+ "&scope=status,audio&redirect_uri=https://oauth.vk.com/blank.html&display=page&v=5.24&response_type=token";

			System.Diagnostics.Process.Start(reqStrTemplate);
			AddresLine.IsEnabled = true;
			AddressLabel.IsEnabled = true;
		}

		private void AddresLine_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (Parse())
			{
				ClickMe.IsEnabled = true;
				DemoSetStatus.IsEnabled = true;
			}
			else
			{
				ClickMe.IsEnabled = false;
				DemoSetStatus.IsEnabled = false;
			}
		}

		private void ClickMe_Click(object sender, RoutedEventArgs e)
		{
			Task.Factory.StartNew(Sort);
		}

		private void Sort()
		{
			// audio.get
			// audio.reorder
			var webClient = new WebClient();
			try
			{
				string answer = webClient.DownloadString("https://api.vk.com/method/audio.get?user_id="
					+ _userIdText + "&v=5.24&access_token=" + _userToken);
				var jObject = JObject.Parse(answer);
				var objAudios = JsonConvert.DeserializeObject<Audio[]>(jObject["response"]["items"].ToString()).ToList();
				_max = objAudios.Count;
				Dispatcher.BeginInvoke(DispatcherPriority.Input, _setMaximum);
				List<Audio> sortedObjAudios = (from v in objAudios
					orderby v.Title.Trim()
					orderby v.Artist.Trim()
					select v).ToList();


				webClient.DownloadString("https://api.vk.com/method/audio.reorder?audio_id=" + sortedObjAudios[0].Id
					+ "&before=" + objAudios[0].Id + "&v=5.24&access_token=" + _userToken);


				for (int m = 1; m < sortedObjAudios.Count; m++)
				{
					string response = String.Empty;
					while (!response.Contains("{\"response\":1}"))
					{
						Thread.Sleep(100);
						response = webClient.DownloadString("https://api.vk.com/method/audio.reorder?audio_id=" + sortedObjAudios[m].Id
							+ "&after=" + sortedObjAudios[m - 1].Id + "&v=5.24&access_token=" + _userToken);
					}
					Dispatcher.BeginInvoke(DispatcherPriority.Input, _incrementProgress);
				}
				MessageBox.Show("Ready!");
				Dispatcher.BeginInvoke(DispatcherPriority.Input, _closeApp);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void SetStatus_Click(object sender, RoutedEventArgs e)
		{
			var webClient = new WebClient();
			try
			{
				Title = webClient.DownloadString("https://api.vk.com/method/status.set?text=" + _NEW_TEXT_STATUS
					+ " " + DateTime.Now + "&user_id=" + _userIdText + "&v=5.24&access_token=" + _userToken);
			}
			catch (Exception ex)
			{
				Title = ex.Message;
			}
		}

		private bool Parse()
		{
			try
			{
				var parse = AddresLine.Text.Split('=');
				_userIdText = parse[3];
				_userToken = parse[1].Split('&')[0];
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}