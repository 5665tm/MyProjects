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
	public partial class Input : Form
	{
		public Input()
		{
			InitializeComponent();
		}

		// заносим все значения по нажатии кнопки ОК
		private void button1_Click(object sender, EventArgs e)
		{
			// заносим значение number1
			try {
				MyData.num1 = Convert.ToInt32(number1box.Text);
				MyData.num1show.Text = Convert.ToString(MyData.num1);
			}
			// обработчик ошибочного ввода
			catch { 
				MyData.num1show.Text = Convert.ToString(MyData.num1)
					+ " (взято предыдущее значение из за ошибки ввода)";
			}
			// заносим значение number2
			try {
				MyData.num2 = Convert.ToInt32(number2box.Text);
				MyData.num2show.Text = Convert.ToString(MyData.num2);
			}
			// обработчик ошибочного ввода
			catch { 
				MyData.num2show.Text = Convert.ToString(MyData.num2)
					+ " (взято предыдущее значение из за ошибки ввода)";
			}
			// проверяем установлены ли галочки, инфу заносим в структуру MyData
			MyData.max_divisor = max_divisor_check.Checked;
			MyData.multiply = multiply_check.Checked;
			MyData.summa = summa_check.Checked;
			// закрываем текущую форму
			this.Close();
			// пишем в главной форме включены ли были галочки
			MyData.lab_max_divisor.Text = "Max divisor: "
				+ (max_divisor_check.Checked ? "включено" : "отключено");
			MyData.lab_multiply.Text = "Multiply: "
				+ (multiply_check.Checked ? "включено" : "отключено");
			MyData.lab_summ.Text = "Summa: "
				+ (summa_check.Checked ? "включено" : "отключено");
		}
	}
}