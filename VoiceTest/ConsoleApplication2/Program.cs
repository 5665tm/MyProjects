// Changed: 2014 10 24 2:33 PM : 5665tm

using System;
using System.Globalization;
using System.Speech.Recognition;
using System.Threading;

namespace VoiceTest
{
	internal class Program
	{
		private static bool _completed;
		private static bool _liter = true;

		private static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("-----");
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
					Console.WriteLine(".");
				}
				(sre).RecognizeAsyncCancel();
			}
		}

		private static void RecognizeCompletedHandler(
			object sender, RecognizeCompletedEventArgs e)
		{
			_completed = true;
			if (e.Result != null)
			{
				Console.WriteLine("  Recognition completed.{0}", e.Result.Text);
				_liter = !_liter;
			}
		}
	}
}