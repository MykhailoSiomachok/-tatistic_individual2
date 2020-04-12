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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public void Make_Diagram(Dictionary<int, int> k)
        {
            chart1.Series["Series1"].Points.Clear();
            foreach (KeyValuePair<int, int> z in k)
            {
                chart1.Series["Series1"].Points.AddXY(z.Key, z.Value);
            }
        }
    }
}
