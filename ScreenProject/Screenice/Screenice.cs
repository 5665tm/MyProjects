using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Screenice
{
	static public class Screenice
	{
		// перегрузка метода Shot() #1
		// сохраняет изображение из буфера обмена в C:\shot.jpg
		// возвращает true в случае успешного сохранения
		// возвращает false при возникновении ошибки сохранения
		static public bool Shot()
		{
			try
			{
				Clipboard.GetImage().Save(@"C:\shot.jpg"
					, System.Drawing.Imaging.ImageFormat.Jpeg);
				return true;
			}
			catch
			{
				return false;
			}
		}

		// перегрузка метода Shot() #2
		// сохраняет изображение из буфера обмена в указанную папку в формате jpg
		//         в качестве имени выступает текущее время
		// возвращает true в случае успешного сохранения
		// возвращает false при возникновении ошибки сохранения
		static public bool Shot(string path)
		{
			try
			{
				string filename = Convert.ToString(DateTime.Now);
				filename = filename.Replace(":", "-");
				filename = filename.Replace(" ", "-");
				filename = filename.Replace(".", "-");
				Clipboard.GetImage().Save(path + "/" + filename + ".jpg"
					, System.Drawing.Imaging.ImageFormat.Jpeg);
				return true;
			}
			catch
			{
				return false;
			}
		}

		// перегрузка метода Shot() #3
		// сохраняет изображение из буфера обмена в указанную папку c
		//          указанным именем в указанном формате (jpg, png, bmp)
		// возвращает true в случае успешного сохранения
		// возвращает false при возникновении ошибки сохранения
		static public bool Shot(string path, string filename, string format)
		{
			try
			{
				switch (format)
				{
					case "jpg":
						Clipboard.GetImage().Save(path + "/" + filename + ".jpg"
							, System.Drawing.Imaging.ImageFormat.Jpeg);
						return true;

					case "png":
						Clipboard.GetImage().Save(path + "/" + filename + ".png"
							, System.Drawing.Imaging.ImageFormat.Png);
						return true;

					case "bmp":
						Clipboard.GetImage().Save(path + "/" + filename + ".bmp"
							, System.Drawing.Imaging.ImageFormat.Bmp);
						return true;

					default:
						return false;
				}
			}
			catch
			{
				return false;
			}
		}
	}
}