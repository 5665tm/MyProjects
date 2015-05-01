// Changed: 2014 10 05 3:41 PM : 5665tm

using System;
using System.Windows.Forms;
using SafeProjectName;

internal static class Program
{
	/// <summary>
	///     Главная точка входа для приложения.
	/// </summary>
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new Form1());
	}
}