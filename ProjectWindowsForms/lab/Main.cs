using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lab
{
	public partial class Main : Form
	{
		// действия при запуске программы
		public Main()
		{
			InitializeComponent();
			// отключаем элемент меню calc
			calcToolStripMenuItem.Enabled = false;
			// указываем ссылки на метки в структуре с данными
			MyData.num1show = num1show;
			MyData.num2show = num2show;
			MyData.lab_summ = lab_sum;
			MyData.lab_multiply = lab_multiply;
			MyData.lab_max_divisor = lab_max_divisor;
		}

		// событие при нажатии на кнопку "input"
		private void inputToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// создаем экземпляр второй формы и показываем ее
			Input f = new Input();
			f.ShowDialog();
			// активируем кнопку "calc"
			calcToolStripMenuItem.Enabled = true;
		}

		// событие при нажатии на кнопку "calc"
		private void calcToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Calc r = new Calc();
			// выводим произведение
			r.p_multiply.Text = MyData.multiply ? "Произведение(Multiply): "
				+ Convert.ToString(MyData.num1 * MyData.num2) : " ";
			// выводим сумму
			r.p_summ.Text = MyData.summa ? "Сумма(Summ): "
				+ Convert.ToString(MyData.num1 + MyData.num2) : " ";
			// считаем и выводим НОД
			int nod = 1;
			int m = MyData.num1;
			int n = MyData.num2;
			for (int i = 1 ; i < (n * m + 1) ; i++) {
				if (m % i == 0 && n % i == 0) {
					nod = i;
				}
			}
			r.p_max_divisor.Text = MyData.max_divisor ? "HOД(Max divisor): "
				+ Convert.ToString(nod) : " ";
			// показываем окно с результатами
			r.ShowDialog();
		}

		// выход из приложения при нажатии на кнопку "quit"
		private void quitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}

	// структура в которой мы храним все данные
	public struct MyData
	{ 
		// метки для отображения значений в главной форме
		static public Label num1show;
		static public Label num2show;
		static public Label lab_max_divisor;
		static public Label lab_summ;
		static public Label lab_multiply;
		// значения двух чисел
		static public int num1 = 0;
		static public int num2 = 0;
		// значения флагов
		static public bool summa = false;
		static public bool max_divisor = false;
		static public bool multiply = false;
	}
}