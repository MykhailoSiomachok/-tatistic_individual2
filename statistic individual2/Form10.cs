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
    public partial class Form10 : Form
    {

        public Form10()
        {
            InitializeComponent();
        }
        public double Factorial(double val)
        {
            double sum = 1.0;
            for (int i = 1; i <= val; i++)
            {
                sum *= i;
            }
            return sum;
        }
        public double C(double N, int i)
        {
            double res = Factorial(N) / (Factorial(i) * Factorial(N - i));
            return res;
        }
        public void Binom(Dictionary<int, int> k, double alpha)
        {
            double x_empiric = 0.0;
            List<string> value = new List<string>();
            List<double> amount = new List<double>();
            List<double> pi = new List<double>();
            List<double> nipi = new List<double>();
            double N = k.Last().Key;
            int suma = 0;
            foreach (var sum in k)
            {
                suma += (sum.Value * sum.Key);
            }
            double prop = suma / (k.Values.Sum() * N);
            int i = 1;
            var summa = 0.0;
            foreach (var sum in k)
            {
                double prob = C(N, i) * Math.Pow(prop, i) * Math.Pow(1 - prop, N - i);
                summa += prob;
                double Pimi = k.Values.Sum() * prob;
                value.Add(sum.Key.ToString());
                amount.Add(sum.Value);
                pi.Add(prob);
                nipi.Add(Pimi);
                i++;
            }

            int count = 0;
            foreach (var val in nipi)
            {
                if (val < 10)
                {
                    count += 1;
                }
            }
            for (int q = 0; q < count; q++)
            {
                for (int j = 0; j < amount.Count - 1; j++)
                {
                    if (j == nipi.Count - 1 && nipi[j] < 10)
                    {
                        value[j - 1] += value[j];
                        value.RemoveAt(j);
                        amount[j - 1] += amount[j];
                        amount.RemoveAt(j);
                        pi[j - 1] += pi[j];
                        pi.RemoveAt(j);
                        nipi[j - 1] += nipi[j];
                        nipi.RemoveAt(j);
                        break;
                    }
                    if (nipi[j] < 10 && j == 0)
                    {
                        value[j + 1] += value[j];
                        value.RemoveAt(j);
                        amount[j + 1] += amount[j];
                        amount.RemoveAt(j);
                        pi[j + 1] += pi[j];
                        pi.RemoveAt(j);
                        nipi[j + 1] += nipi[j];
                        nipi.RemoveAt(j);
                        break;
                    }
                    if (nipi[j] < 10 && nipi[j + 1] < 10)
                    {
                        value[j] += value[j + 1];
                        value.RemoveAt(j + 1);
                        amount[j] += amount[j + 1];
                        amount.RemoveAt(j + 1);
                        pi[j] += pi[j + 1];
                        pi.RemoveAt(j + 1);
                        nipi[j] += nipi[j + 1];
                        nipi.RemoveAt(j + 1);
                        break;
                    }
                    if (nipi[j] < 10 && nipi[j + 1] >= 10)
                    {
                        value[j - 1] += value[j];
                        value.RemoveAt(j);
                        amount[j - 1] += amount[j];
                        amount.RemoveAt(j);
                        pi[j - 1] += pi[j];
                        pi.RemoveAt(j);
                        nipi[j - 1] += nipi[j];
                        nipi.RemoveAt(j);
                        break;
                    }
                }

            }
            value[value.Count - 2] += value[value.Count - 1];
            value.RemoveAt(value.Count - 1);
            amount[amount.Count - 2] += amount[amount.Count - 1];
            amount.RemoveAt(amount.Count - 1);
            pi[pi.Count - 2] += pi[pi.Count - 1];
            pi.RemoveAt(pi.Count - 1);
            nipi[pi.Count - 2] += nipi[nipi.Count - 1];
            nipi.RemoveAt(nipi.Count - 1);
            if (value[0].Length > 2)
            {
                string x = $"{value[0][0]} : {value[0].Last()}";
                value[0] = x;
            }
            for (int j = 0; j < value.Count; j++)
            {
                if (value[j].Length > 2)
                {
                    value[j] = $"{value[j][1]}{value[j][0]} : {value[j][value[j].Length - 2]}{value[j][value[j].Length - 1]}";
                }
            }

            for (int j = 0; j < amount.Count; j++)
            {
                x_empiric += (double)Math.Pow(amount[j] - Math.Round(nipi[j], 4), 2) / Math.Round(nipi[j], 4);
                dataGridView1.Rows.Add(value[j], amount[j], pi[j], nipi[j]);
            }
            int df = amount.Count - 1;
            double x_crit = table_value(df, alpha);
            if (x_crit != 0.0)
            {
                textBox4.Text = x_crit.ToString();
            }
            else
            {
                textBox4.Text = "Program is not suited for such numbers";
            }
            if (x_empiric > x_crit)
            {
                textBox5.Text = "Hypothesis is Wrong";
            }
            else
            {
                textBox5.Text = "Hipothesis is true";
            }
            textBox1.Text = summa.ToString();
            textBox2.Text = prop.ToString();
            textBox3.Text = x_empiric.ToString();
         
        }
        public double table_value(int x, double alpha)
        {
            double res;
            if (x == 1 && alpha == 0.01)
            {
                res = 6.6;
                return res;
            }
            if (x == 1 && alpha == 0.05)
            {
                res = 3.8;
                return res;
            }
            if (x == 2 && alpha == 0.01)
            {
                res = 9.2;
                return res;
            }
            if (x == 2 && alpha == 0.05)
            {
                res = 6.0;
                return res;
            }
            if (x == 3 && alpha == 0.01)
            {
                res = 11.3;
                return res;
            }
            if (x == 3 && alpha == 0.05)
            {
                res = 7.8;
                return res;
            }
            if (x == 4 && alpha == 0.01)
            {
                res = 13.3;
                return res;
            }
            if (x == 4 && alpha == 0.05)
            {
                res = 9.5;
                return res;
            }
            if (x == 5 && alpha == 0.01)
            {
                res = 15.1;
                return res;
            }
            if (x == 5 && alpha == 0.05)
            {
                res = 11.1;
                return res;
            }
            if (x == 6 && alpha == 0.01)
            {
                res = 16.8;
                return res;
            }
            if (x == 6 && alpha == 0.05)
            {
                res = 12.6;
                return res;
            }
            if (x == 7 && alpha == 0.01)
            {
                res = 18.5;
                return res;
            }
            if (x == 7 && alpha == 0.05)
            {
                res = 14.1;
                return res;
            }
            if (x == 8 && alpha == 0.01)
            {
                res = 20.1;
                return res;
            }
            if (x == 8 && alpha == 0.05)
            {
                res = 15.5;
                return res;
            }
            if (x == 9 && alpha == 0.01)
            {
                res = 21.7;
                return res;
            }
            if (x == 9 && alpha == 0.05)
            {
                res = 16.9;
                return res;
            }
            if (x == 10 && alpha == 0.01)
            {
                res = 23.2;
                return res;
            }
            if (x == 10 && alpha == 0.05)
            {
                res = 18.3;
                return res;
            }
            else
            {
                return 0.0;
            }
        }
    }
}
