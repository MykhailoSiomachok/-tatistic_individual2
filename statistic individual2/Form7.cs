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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }
        public void Make_Gistogram(Dictionary<List<double>, int> k)
        {
            List<double> z = new List<double>();
            List<double> omega = new List<double>();
            List<double> x = new List<double>();
            double Bomega = 0;
            foreach (var val in k)
            {
                z.Add((val.Key[0] + val.Key[1]) / 2);
                omega.Add(val.Value/(double)k.Values.Sum());
                x.Add(val.Key[0]);
            }
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddXY(0, 0);
            for (int i = 1; i < z.Count; i++)
            {
                double Fx = (omega[i] / (z[i] - z[i - 1])) * (x[i] - z[i - 1]) + Bomega;
                Bomega += omega[i];
                chart1.Series["Series1"].Points.AddXY(x[i], Fx);
            }
        }
    }
}
