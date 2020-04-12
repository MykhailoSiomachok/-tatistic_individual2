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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        public void Make_Gistogram(Dictionary<List<double>, int> k)
        {
            chart1.Series["Series1"].Points.Clear();
            foreach (var z in k)
            {
                chart1.Series["Series1"].Points.AddXY($"[{z.Key[0]} : {z.Key[1]}]", z.Value);
            }
        }
}
}
