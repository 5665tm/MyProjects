// Changed: 2014 10 06 8:57 PM : 5665tm

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

public partial class Form1 : Form
{
	/// <summary>
	///     Начальная инициализация
	/// </summary>
	public Form1()
	{
		InitializeComponent();
		// путь к папке с программой
		string currentPath = Environment.CurrentDirectory;
		// параметры по умолчанию
		tbConfig.Text = @"D:\Desktop\FIDSKY1\CMS\config.php";
		tbAcc.Text = @"D:\Desktop\FIDSKY1\CMS_extras\free.txt";
		tbKeys.Text = @"D:\Desktop\FIDSKY1\CMS_extras\keys";
		tbTexts.Text = @"D:\Desktop\FIDSKY1\CMS_extras\text";
		tbSavePath.Text = @"D:\Desktop\FIDSKY1\out";
		uploadUnZip.Checked = true;
	}

	/// <summary>
	///     Выбор файла конфигурации
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btConfig_Click(object sender, EventArgs e)
	{
		var openDialog = new OpenFileDialog {Filter = @"Файл конфигурации|*.php"};
		DialogResult result = openDialog.ShowDialog();
		if (result == DialogResult.OK)
		{
			tbConfig.Text = openDialog.FileName;
		}
	}

	/// <summary>
	///     Выбор файла с аккаунтами
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btAcc_Click(object sender, EventArgs e)
	{
		var openDialog = new OpenFileDialog {Filter = @"Файл c аккаунтами|*.txt"};
		DialogResult result = openDialog.ShowDialog();
		if (result == DialogResult.OK)
		{
			tbAcc.Text = openDialog.FileName;
		}
	}

	/// <summary>
	///     Выбор папки с ключами
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btKeys_Click(object sender, EventArgs e)
	{
		OpenFolder(tbKeys);
	}

	/// <summary>
	///     Выбор папки с текстами
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btTexts_Click(object sender, EventArgs e)
	{
		OpenFolder(tbTexts);
	}

	/// <summary>
	///     Выбор папки где будут храниться результаты
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btSavePath_Click(object sender, EventArgs e)
	{
		OpenFolder(tbSavePath);
	}

	/// <summary>
	///     Занесение в TextBox выбранной папки
	/// </summary>
	/// <param name="tb"></param>
	private void OpenFolder(MaskedTextBox tb)
	{
		var openDialog = new FolderBrowserDialog();
		DialogResult result = openDialog.ShowDialog();
		if (result == DialogResult.OK)
		{
			tb.Text = openDialog.SelectedPath;
		}
	}

	/// <summary>
	///     Нажатие кнопки пуск
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void btRun_Click(object sender, EventArgs e)
	{
		//блокируем форму на время выполнения программы
		Enabled = false;
		// вся логика выполнения будет в отдельном потоке
		var backgroundThread = new Thread(() =>
		{
			var timer = new Stopwatch();
			timer.Start();
			try
			{
				#region Выполняем все проверки

				if (tbConfig.Text.Length == 0
					|| tbAcc.Text.Length == 0
					|| tbKeys.Text.Length == 0
					|| tbTexts.Text.Length == 0
					|| tbSavePath.Text.Length == 0)
				{
					MessageBox.Show(@"Не все поля заполнены");
					return;
				}
				if (!File.Exists(tbConfig.Text))
				{
					MessageBox.Show(@"Неверно указан файл конфигурации!");
					return;
				}
				if (!File.Exists(tbAcc.Text))
				{
					MessageBox.Show(@"Неверно указан файл c аккаунтами!");
					return;
				}

				// здесь будут храниться все аккаунты
				List<string> freeHosts = File.ReadAllLines(tbAcc.Text).ToList();
				// пустые строки игнорируем
				for (int i = 0; i < freeHosts.Count;)
				{
					if (freeHosts[i] == "")
					{
						freeHosts.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
				int countFreeHost = freeHosts.Count;
				// список файлов с ключами и их количество
				FileInfo[] filesKey = new DirectoryInfo(tbKeys.Text).GetFiles();
				int countFilesKey = filesKey.Length;
				// список файлов с текстами и их количество
				FileInfo[] filesTexts = new DirectoryInfo(tbTexts.Text).GetFiles();
				int countFilesTexts = filesTexts.Length;
				// проверяем что всего поровну
				if (countFreeHost != countFilesKey
					|| countFreeHost != countFilesTexts
					|| countFilesTexts != countFilesKey)
				{
					MessageBox.Show(@"Число аккаунтов, файлов в папке с ключами и в папке с текстами должно совпадать");
					return;
				}

				#endregion

				#region Если проверки прошли удачно, начинаем работу

				// запишем config.php в память
				string configPhp = File.ReadAllText(tbConfig.Text, Encoding.UTF8);

				int success = 0;
				var uploadList = new ConcurrentBag<string>();
				// распаралеливаем выполнение на два потока
				// судя по тестам, больше потоков нет смысла создавать
				Parallel.For(0, 2, k =>
				{
					int start = k == 0 ? 0 : countFreeHost/2;
					int end = k == 0 ? countFreeHost/2 : countFreeHost;
					// каждый из потоков обрабатывает свою половину аккаунтов
					for (int i = start; i < end; i++)
					{
						// показываем прогресс
						int count1 = success++;
						progressBar.BeginInvoke(new Action(() =>
						{
							progressBar.Value = Convert.ToInt32((count1*1000f)/countFreeHost);
						}));

						// создаем новую папку и копуруем в нее CMS
						string hostName = freeHosts[i].Split(';')[1];
						string newDirectory = tbSavePath.Text + "\\" + "_temp_" + hostName;
						Directory.CreateDirectory(newDirectory);
						string sourceFolder = tbConfig.Text.Substring(0, tbConfig.Text.LastIndexOf('\\'));
						CopyFolder(sourceFolder, newDirectory);

						// генерируем задание для uploadUnZip
						if (uploadUnZip.Checked)
						{
							string[] param = freeHosts[i].Split(';');
							string template = @"ftp://" + param[2] + ":" + param[3] + "@" + param[1] + "/"+ param[4] +
								@" http://" + param[1] + @"/ "
								+ tbSavePath.Text + @"\" + param[1] + ".zip";
							uploadList.Add(template);
						}

						// записываем config.php
						var fs = new FileStream(newDirectory + "\\config.php", FileMode.Create);
						var sw = new StreamWriter(fs, Encoding.UTF8);
						string newConfig = configPhp.Replace("$url = \"http://\";", "$url = \"http://" + hostName + "\";");
						sw.Write(newConfig, Encoding.UTF8);
						sw.Close();
						fs.Close();

						// копируем ключ и текстовик
						File.Copy(filesKey[i].FullName, newDirectory + @"\keys.txt", true);
						File.Copy(filesTexts[i].FullName, newDirectory + @"\text.txt", true);

						// зипуем и удаляем временную папку
						string zipFileName = tbSavePath.Text + "\\" + hostName + ".zip";
						if (File.Exists(zipFileName))
						{
							File.Delete(zipFileName);
						}
						var zf = new ZipFile(zipFileName);
						zf.AddDirectory(newDirectory);
						zf.Save();
						Directory.Delete(newDirectory, true);
					}
				});
				if (uploadUnZip.Checked)
				{
					var fs = new FileStream("zip.txt", FileMode.Create);
					var sw = new StreamWriter(fs);
					bool first = true;
					foreach (var note in uploadList)
					{
						if (first)
						{
							first = false;
						}
						else
						{
							sw.WriteLine();
						}
						sw.Write(note);
					}
					sw.Close();
					fs.Close();
				}
				timer.Stop();
				MessageBox.Show(@"Время выполнения " + Math.Round(timer.ElapsedMilliseconds/1000f, 1) + @" c. Количество архивов " + countFreeHost);
				progressBar.BeginInvoke(new Action(() =>
				{
					progressBar.Value = 0;
				}));

				#endregion
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			BeginInvoke(new Action(() =>
			{
				Enabled = true;
				TopMost = true;
				TopMost = false;
			}));
		}) {Priority = ThreadPriority.Highest};
		backgroundThread.Start();
	}

	private void CopyFolder(string sourceFolder, string destFolder)
	{
		if (!Directory.Exists(destFolder))
		{
			Directory.CreateDirectory(destFolder);
		}

		string[] files = Directory.GetFiles(sourceFolder);

		foreach (string file in files)
		{
			if (file != null)
			{
				File.Copy(file, Path.Combine(destFolder, Path.GetFileName(file)), true);
			}
		}

		string[] folders = Directory.GetDirectories(sourceFolder);

		foreach (string folder in folders)
		{
			if (folder != null)
			{
				CopyFolder(folder, Path.Combine(destFolder, Path.GetFileName(folder)));
			}
		}
	}
}