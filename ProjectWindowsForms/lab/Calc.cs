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
	public partial class Calc : Form
	{
		public Label p_multiply;
		public Label p_summ;
		public Label p_max_divisor;

		public Calc()
		{
			InitializeComponent();
			p_multiply = multiply;
			p_summ = summ;
			p_max_divisor = max_divisor;
		}
	}
}
