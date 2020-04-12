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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public void Make_Gistogram(Dictionary<int, int> k)
        {
            Dictionary<List<double>, int> pairs = new Dictionary<List<double>, int>();
            int amount = 0;
            double swing = k.Last().Key - k.First().Key;
            foreach(var y in k.Values)
            {
                amount += y;
            }
            double interval_amount = Math.Floor(Math.Log(amount, 2)) + 1;
            var step = swing / interval_amount;
            for(double i = k.First().Key; i <k.Last().Key; i += step)
            {
                int count = 0;
                List<double> frequency = new List<double>
                {
                    i,
                    i + step
                };
                foreach (var y in k)
                {
                    if (i == k.First().Key)
                    {
                        if (y.Key >= i && y.Key <= i + step)
                        {
                            count += y.Value;
                        }
                    }
                    else
                    {
                        if (y.Key > i && y.Key <= i + step)
                        {
                            count += y.Value;
                        }
                    }
                }
                pairs.Add(frequency, count);
            }
            chart1.Series["Series1"].Points.Clear();
            foreach (var z in pairs)
            {
                chart1.Series["Series1"].Points.AddXY($"[{z.Key[0]} : {z.Key[1]}]", z.Value);
            }

        }
    }
}
