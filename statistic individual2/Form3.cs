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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public void Make_Polygon(Dictionary<int, int> k)
        {
            chart1.Series["Series1"].Points.Clear();
            foreach (KeyValuePair<int, int> z in k)
            {
                chart1.Series["Series1"].Points.AddXY(z.Key, z.Value);
            }
        }
    }
}
