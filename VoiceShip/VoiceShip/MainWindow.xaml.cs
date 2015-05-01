// Changed: 2014 10 24 3:06 PM : 5665tm

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VoiceShip
{
	/// <summary>
	///     Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly List<Rectangle> _allRectangles = new List<Rectangle>();
		private readonly bool[,,] _ships = new bool[2, 10, 10];
		private static bool _liter = true;
		private int _column;
		private Label _lastLiter;
		private string _com = "";

		private enum Thing
		{
			Unknown,
			Sea,
			DestroySea,
			Ship,
			DestroyShip,
			Cursor
		}

		public MainWindow()
		{
			InitializeComponent();
			NewGame();
			try
			{
				var sre = new SpeechRecognitionEngine(new CultureInfo("en-US"));
			}
			catch (Exception ex)
			{
				MessageBox.Show("Печаль, печаль, печаль.\nНе хочу тебя расстраивать дружище, но кажется на твоем компе не установлен Speech Recognition для английского языка.\nУстанови его, и может быть удача улыбнется тебе.");
				Close();
			}
			allCommand.Text = @"Aa [ ei ] [эй]
Bb [ bi: ] [би]
Cc [ si: ] [си]
Dd [ di: ] [ди]
Ee [ i: ] [и]
Ff [ ef ] [эф]
Gg [ dʒi: ] [джи]
Hh [ eitʃ ] [эйч]
Ii [ ai ] [ай]
Jj [ dʒei ] [джей]";
			Task.Factory.StartNew(Run);
		}

		private bool _completed;

		private void Run()
		{
			while (true)
			{
				_completed = false;
				// Create a new SpeechRecognitionEngine instance.
				var sre = new SpeechRecognitionEngine(new CultureInfo("en-US"));

				// sre.SetInputToWaveFile("D:\\Desktop\\stop.wav");
				sre.SetInputToDefaultAudioDevice();

				var choices = new Choices();
				if (_liter)
				{
					choices.Add(new[] {"a", "b", "c", "d", "e", "f", "g", "i", "h", "j"});
				}
				else
				{
					choices.Add(new[] {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"});
				}
				var gb = new GrammarBuilder();
				gb.Append(choices);
				var g = new Grammar(gb);

				sre.LoadGrammar(g);
				sre.RecognizeCompleted +=
					RecognizeCompletedHandler;

				sre.RecognizeAsync(RecognizeMode.Single);
				while (!_completed)
				{
					Thread.Sleep(400);
				}
				(sre).RecognizeAsyncCancel();
			}
		}

		private void RecognizeCompletedHandler(
			object sender, RecognizeCompletedEventArgs e)
		{
			_completed = true;
			if (e.Result != null)
			{
				_com = e.Result.Text;
				Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action) (NextStep));
			}
		}

		private void PutObject(int row, int column, Thing type, bool enemy = true)
		{
			Brush color = BorderBrush;
			if (type == Thing.Unknown)
			{
				color = Brushes.WhiteSmoke;
			}
			else if (type == Thing.Sea)
			{
				color = Brushes.LightSeaGreen;
			}
			else if (type == Thing.DestroySea)
			{
				color = Brushes.LightSlateGray;
			}
			else if (type == Thing.Ship)
			{
				color = Brushes.Brown;
			}
			else if (type == Thing.DestroyShip)
			{
				color = Brushes.Black;
			}
			else if (type == Thing.Cursor)
			{
				var myColor = new Color {A = 125, R = 125, G = 125, B = 125};
				color = new SolidColorBrush(myColor);
			}
			var rec = new Rectangle {Fill = color, Height = 40, Width = 40, StrokeThickness = 1, Stroke = Brushes.Black};
			if (enemy)
			{
				FieldEnemy.Children.Add(rec);
			}
			else
			{
				FieldPlayer.Children.Add(rec);
			}
			Grid.SetRow(rec, row);
			Grid.SetColumn(rec, column);
			_allRectangles.Add(rec);
		}

		private void NewGame()
		{
			SetShips(0);
			Thread.Sleep(10);
			SetShips(1);
			FieldEnemy.Children.RemoveRange(0, FieldEnemy.Children.Count);
			_allRectangles.Clear();
			for (int i = 0; i < 10; i++)
			{
				for (int k = 0; k < 10; k++)
				{
					PutObject(i, k, _ships[1, i, k] ? Thing.Ship : Thing.Sea, false);
					PutObject(i, k, Thing.Unknown);
				}
			}
		}

		private void SetShips(int num)
		{
			var rnd = new Random();
			bool ok = false;
			loop:
			while (!ok)
			{
				ok = true;
				for (int i = 0; i < 10; i++)
				{
					for (int j = 0; j < 10; j++)
					{
						_ships[num, i, j] = false;
					}
				}
				for (int p = 4; p >= 1; p--)
				{
					for (int j = 0; j < 5 - p; j++)
					{
						bool d = rnd.Next(0, 2) == 0;
						int x = rnd.Next(10 - (d ? p : 0));
						int y = rnd.Next(10 - (d ? 0 : p));
						if (d)
						{
							for (int i = 0; i < p; i++)
							{
								ok &= CanPut(x + i, y, i, d, num);
								_ships[num, x + i, y] = true;
								if (!ok)
								{
									goto loop;
								}
							}
						}
						else
						{
							for (int i = 0; i < p; i++)
							{
								ok &= CanPut(x, y + i, i, d, num);
								_ships[num, x, y + i] = true;
								if (!ok)
								{
									goto loop;
								}
							}
						}
					}
				}
			}
		}

		private bool CanPut(int x, int y, int p, bool d, int num)
		{
			bool ok = !_ships[num, x, y];
			ok &= (x == 0 || (d && p != 0) || !_ships[num, x - 1, y]);
			ok &= (y == 0 || !(d || p == 0) || !_ships[num, x, y - 1]);
			ok &= (x == 9 || !_ships[num, x + 1, y]);
			ok &= (y == 9 || !_ships[num, x, y + 1]);
			ok &= (x == 0 || y == 0 || !_ships[num, x - 1, y - 1]);
			ok &= (x == 9 || y == 9 || !_ships[num, x + 1, y + 1]);
			ok &= (x == 0 || y == 9 || !_ships[num, x - 1, y + 1]);
			ok &= (x == 9 || y == 0 || !_ships[num, x + 1, y - 1]);
			return ok;
		}

		private void NextStep()
		{
			command.Text = _com;
			if (_liter)
			{
				switch (command.Text)
				{
					case "a":
						_lastLiter = cA;
						_column = 0;
						break;
					case "b":
						_lastLiter = cB;
						_column = 1;
						break;
					case "c":
						_lastLiter = cC;
						_column = 2;
						break;
					case "d":
						_lastLiter = cD;
						_column = 3;
						break;
					case "e":
						_lastLiter = cE;
						_column = 4;
						break;
					case "f":
						_lastLiter = cF;
						_column = 5;
						break;
					case "g":
						_lastLiter = cG;
						_column = 6;
						break;
					case "h":
						_lastLiter = cH;
						_column = 7;
						break;
					case "i":
						_lastLiter = cI;
						_column = 8;
						break;
					case "j":
						_lastLiter = cJ;
						_column = 9;
						break;
					default:
						command.Text = "";
						return;
				}
				_lastLiter.Foreground = Brushes.Red;
				_lastLiter.FontWeight = FontWeights.Bold;
				allCommand.Text = @"1	one [wʌn]
2	two [tuː]
3	three [θriː]
4	four [fɔː]
5	five [faɪv]
6	six [sɪks]
7	seven ['sev(ə)n]
8	eight [eɪt]
9	nine [naɪn]
10	ten [ten]";
			}
			else
			{
				int row = Convert.ToInt32(command.Text) - 1;
				if (row > 9 || row < 0)
				{
					return;
				}
				_lastLiter.Foreground = Brushes.Black;
				_lastLiter.FontWeight = FontWeights.Normal;
				PutObject(row, _column, _ships[0, row, _column] ? Thing.DestroyShip : Thing.DestroySea);
				var rnd = new Random();
				int r = rnd.Next(0, 10);
				int c = rnd.Next(0, 10);
				Title = r + " " + c;
				PutObject(r,
					c,
					_ships[1, r, c] ? Thing.DestroyShip : Thing.DestroySea,
					false);
				allCommand.Text = @"Aa [ ei ] [эй]
Bb [ bi: ] [би]
Cc [ si: ] [си]
Dd [ di: ] [ди]
Ee [ i: ] [и]
Ff [ ef ] [эф]
Gg [ dʒi: ] [джи]
Hh [ eitʃ ] [эйч]
Ii [ ai ] [ай]
Jj [ dʒei ] [джей]";
			}
			_liter = !_liter;
		}
	}
}