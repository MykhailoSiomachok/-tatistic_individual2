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
    public partial class Form1 : Form
    {
        Dictionary<int, int> pairs = new Dictionary<int, int>();
        Dictionary<List<double>, int> key_value = new Dictionary<List<double>, int>();
        double standart_d;
        double average_d; 
        double standart_c;
        double average_c;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pairs = null;
            double a = Convert.ToDouble(textBox1.Text);
            double b = Convert.ToDouble(textBox2.Text);
            int n = Convert.ToInt32(textBox3.Text);
            Discrete discrete = new Discrete(a, b, n);
            discrete.Get_list_and_hash_set();
            discrete.Get_dict();
            pairs = discrete.Dict;
            dataGridView2.Rows.Clear();
            foreach(var y in discrete.Dict)
            {
                dataGridView2.Rows.Add(y.Key, y.Value);
            }
            richTextBox1.Text = $"Median: {Convert.ToString(discrete.Median())}\n" +
                $"Mode: {Convert.ToString(discrete.Mode())}\n" +
                $"Rozmach: {Convert.ToString(discrete.Rozmach())}\n" +
                $"Average: {Convert.ToString(discrete.Average())}\n" +
                $"Deviation: {Convert.ToString(discrete.Deviation())}\n" +
                $"Variansa: {Convert.ToString(discrete.Variansa())}\n" +
                $"Standart: {Convert.ToString(discrete.Standart())}\n" +
                $"Dyspersia: {Convert.ToString(discrete.Dispersion())}\n" +
                $"Variation: {Convert.ToString(discrete.Variation())}\n" +
                $"Asymetry: {Convert.ToString(discrete.Asymerty())}\n" +
                $"Access: {Convert.ToString(discrete.Access())}\n";
            foreach (var val in discrete.Interquantile_latitude())
            {
                richTextBox1.Text += val.Key + ": " + val.Value.ToString() + "\n";
            }
            average_d = discrete.Average();
            standart_d = discrete.Standart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Make_Diagram(pairs);
            form2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Make_Polygon(pairs);
            form3.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Make_Gistogram(pairs);
            form4.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Make_Empiryc(pairs);
            form5.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            key_value = null;
            int a = Convert.ToInt32(textBox6.Text);
            int b = Convert.ToInt32(textBox7.Text);
            int n = Convert.ToInt32(textBox8.Text);
            Continous continous = new Continous(a, b, n);
            continous.Get_list_and_hash_set();
            continous.Get_Dict();
            key_value = continous.Dict;
            dataGridView1.Rows.Clear();
            foreach(var z in continous.Dict)
            {
                dataGridView1.Rows.Add($"[{z.Key[0]} : {z.Key[1]}]",$"{z.Value}");
            }
            richTextBox2.Text = $"Median: {Convert.ToString(continous.Median())}\n" +
                $"Mode: {Convert.ToString(continous.Mode())}\n" +
                $"Rozmach: {Convert.ToString(continous.Swing())}\n" +
                $"Average: {Convert.ToString(continous.Average())}\n" +
                $"Deviation: {Convert.ToString(continous.Deviation())}\n" +
                $"Variansa: {Convert.ToString(continous.Variansa())}\n" +
                $"Standart: {Convert.ToString(continous.Standart())}\n" +
                $"Dyspersia: {Convert.ToString(continous.Dispresion())}\n" +
                $"Variation: {Convert.ToString(continous.Variation())}\n" +
                $"Asymerty: {Convert.ToString(continous.Asymetry())}\n" +
                $"Access:  {Convert.ToString(continous.Access())}\n";
            foreach (var val in continous.Interqantile_lattitudes())
            {
                richTextBox2.Text += val.Key + ": " + val.Value.ToString() + "\n";
            }
            average_c = continous.Average();
            standart_c = continous.Standart();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form6 form6 = new Form6();
            form6.Make_Gistogram(key_value);
            form6.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.Make_Gistogram(key_value);
            form7.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form8 form8 = new Form8();
            double alpha = Convert.ToDouble(textBox4.Text);
            form8.Edges(average_d, standart_d);
            form8.Texto(pairs, average_d, standart_d, alpha);
            form8.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form10 form10 = new Form10();
            double alpha = Convert.ToDouble(textBox4.Text);
            form10.Binom(pairs,alpha);
            form10.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Form9 form9 = new Form9();
            double alpha = Convert.ToDouble(textBox5.Text);
            form9.Edges(average_c, standart_c);
            form9.Texto(key_value, average_c, standart_c, alpha);
            form9.Show();
        }
    }
}
