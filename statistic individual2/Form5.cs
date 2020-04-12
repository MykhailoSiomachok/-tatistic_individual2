using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace statistic_individual2
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        public void Make_Empiryc(Dictionary<int, int> k)
        {
            double amount = 0;
            double res = 0;
            foreach (int val in k.Values)
            {
                amount += val;
            }

            chart1.Series["Series1"].Points.Clear();
            for(int i = -3; i <= k.Keys.First(); i++)
            {
                chart1.Series["Series1"].Points.AddXY(i, 0);
            }
            foreach (var z in k)
            {
                res += z.Value / amount;
                chart1.Series["Series1"].Points.AddXY(z.Key, res);
            }
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }
    }
}
