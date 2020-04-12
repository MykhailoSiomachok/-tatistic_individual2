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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        public Dictionary<List<double>,int> Make_Gistogram(Dictionary<int, int> k)
        {
            Dictionary<List<double>, int> pairs = new Dictionary<List<double>, int>();
            int amount = 0;
            double swing = k.Last().Key - k.First().Key;
            foreach (var y in k.Values)
            {
                amount += y;
            }
            double interval_amount = Math.Floor(Math.Log(amount, 2)) + 1;
            var step = swing / interval_amount;
            for (double i = k.First().Key; i < k.Last().Key; i += step)
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
            return pairs;
        }
        public Dictionary<List<double>, int> Group_check(Dictionary<int, int> k)
        {
            Dictionary<List<double>, int> Checked_dict = new Dictionary<List<double>, int>();
            var x = Make_Gistogram(k);
            List <List<double>> Keys = new List<List<double>>(x.Keys);
            List<int> Values = new List<int>(x.Values);
            if(Keys.Count == Values.Count)
            {
                for (int i = 0; i < Keys.Count; i++)
                {
                    if(i == Values.Count - 1 && Values[i] < 5)
                    {
                        Values[i - 1] += Values[i];
                        Values.RemoveAt(i);
                        Keys[i - 1][1] = Keys[i][1];
                        Keys.RemoveAt(i);
                        break;
                    }    
                    if(Values[i] < 5 && i ==1)
                    {
                        Values[i] += Values[i + 1];
                        Values.RemoveAt(i + 1);
                        Keys[i][1] = Keys[i + 1][1];
                        Keys.RemoveAt(i + 1);
                        break;
                    }
                    if(Values[i] < 5 && Values[i+1] < 5)
                    {
                        Values[i] += Values[i + 1];
                        Values.RemoveAt(i + 1);
                        Keys[i][1] = Keys[i + 1][1];
                        Keys.RemoveAt(i + 1);
                        break;
                    }
                    if(Values[i]<5 && Values[i+1] >= 5)
                    {
                        Values[i - 1] += Values[i];
                        Values.RemoveAt(i);
                        Keys[i - 1][1] = Keys[i][1];
                        Keys.RemoveAt(i);
                        break;
                    }
                    
                }
            }
            for (int i = 0; i < Keys.Count; i++)
            {
                Checked_dict.Add(Keys[i], Values[i]);
            }
            return Checked_dict;
        }
        public void Texto(Dictionary<int, int> k, double average, double standart,double alpha)
        {
            var x_empiric = 0.0;
            int i = 0;
            var y = Prob(average,standart,k);
            dataGridView1.Rows.Clear();
            foreach(var x in Group_check(k))
            {
                dataGridView1.Rows.Add($"{x.Key[0]} : {x.Key[1]}", x.Value, Math.Round(y[i],4), k.Values.Sum()*Math.Round(y[i], 4));
                x_empiric += Math.Pow(x.Value - (k.Values.Sum() * Math.Round(y[i], 4)),2)/ (k.Values.Sum() * Math.Round(y[i], 4));
                i++;
            }
            textBox3.Text = x_empiric.ToString();
            int df = dataGridView1.Rows.Count - 3;
            var crit = table_value(df, alpha);
            if (crit != 0.0)
            {              textBox4.Text = crit.ToString();
            }
            else
            {
                textBox4.Text = "Program is not suited for such numbers";
            }
            if(x_empiric > crit)
            {
                textBox5.Text = "Hypothesis is Wrong";
            }
            else
            {
                textBox5.Text = "Hipothesis is true";
            }
        }
        public List<double> Edges(double average, double standart)
        {
            List<double> ab = new List<double>();
            double a_star = Math.Round(average - Math.Sqrt(3) * standart,3);
            double b_star = Math.Round(average + Math.Sqrt(3) * standart,3);
            ab.Add(a_star);
            ab.Add(b_star);
            textBox1.Text = a_star.ToString();
            textBox2.Text = b_star.ToString();
            return ab;
        }
        public List<double> Prob(double average, double standart, Dictionary<int,int> k)
        {
            List<double> prob = new List<double>();
            var dict = Group_check(k);
            var edge = Edges(average, standart);
            foreach(var d in dict)
            {
                double res = (d.Key[1] - d.Key[0]) / (edge[1] - edge[0]);
                prob.Add(res);
            }
            return prob;
        }
        public double table_value(int x,double alpha)
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
            if (x == 2 && alpha == 0.01 )
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
