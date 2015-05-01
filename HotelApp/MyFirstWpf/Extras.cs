using System;

namespace MyFirstWpf
{
class Msg
{
	static public void Norm(string msg)
	{
		ShowText(System.Windows.Media.Brushes.White, msg);
	}

	static public void Wrong(string msg)
	{
		ShowText(System.Windows.Media.Brushes.Red, msg);
	}

	static public void Ready(string msg)
	{
		ShowText(System.Windows.Media.Brushes.LightGreen, msg);
	}

	static private void ShowText(System.Windows.Media.Brush col, string msg)
	{
		Tables.lab_info.Background = col;
		Tables.lab_info.Content = "  " + DateTime.Now.Hour + ":"
			+ DateTime.Now.Minute + ":" + DateTime.Now.Second + " : " + msg;
	}
}
}