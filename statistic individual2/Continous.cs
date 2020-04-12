using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statistic_individual2
{
    class Continous
    {
        public int A { get; set; }
        public int B { get; set; }
        public int N { get; set; }
        public List<int> List_ = new List<int>();
        public HashSet<int> Hash = new HashSet<int>();
        public Dictionary<List<double>, int> Dict = new Dictionary<List<double>, int>();
        public Continous() { }
        public Continous(int a, int b, int n)
        {
            A = a;
            B = b;
            N = n;
        }
        public void Get_list_and_hash_set()
        {
            Random rnd = new Random();
            for (int i = 1; i <= N; i++)
            {
                List_.Add(rnd.Next(Convert.ToInt32(A), Convert.ToInt32(B)));
            }
            List_ = List_.OrderBy(x => x).ToList();
            Hash = List_.ToHashSet();
        }
        public void Get_Dict()
        {
            var intervals = Math.Floor(Math.Log(N, 2)) + 1;
            var step = (B - A) / intervals;
            List<double> first_pair = new List<double> { A - step, A };
            Dict.Add(first_pair, 0);
            for (double i = A; i < B; i += step)
            {
                int count = 0;
                List<double> pair = new List<double>
                {
                    i,
                    i + step
                };
                foreach (var k in List_)
                {
                    if (i == A)
                    {
                        if (k >= i && k <= i + step)
                        {
                            count += 1;
                        }
                    }
                    else
                    {
                        if (k > i && k <= i + step)
                        {
                            count += 1;
                        }
                    }

                }
                Dict.Add(pair, count);
            }
            
        }
        public List<double> Modal_interval()
        {
            int max = Dict.Values.Max();
            foreach(var val in Dict)
            {
                if(val.Value == max)
                {
                    return val.Key;
                }
            }
            return Dict.First().Key; // if got this, something is wrong
        }
        public double Mode()
        {
            List<int> values = new List<int>(Dict.Values);
            var list = Modal_interval();
            int max = Dict.Values.Max();
            double h = list[1] - list[0];
            double x = list[0];
            int previous = 0;
            int next = 0;
            for(int i = 0; i<values.Count; i++)
            {
                if( values[i] == max)
                {
                    previous = values[i - 1];
                    next = values[i + 1];
                }
            }
            double mode = x + h * ((max - previous) / (max - previous + max - next));
            return mode;
        }
        public Dictionary<List<double>,int> Median_interval()
        {
            int count = 0;
            Dictionary<List<double>, int> res = new Dictionary<List<double>, int>();
            int center = (int)Math.Ceiling((double)Dict.Values.Sum()/2);
            foreach (var pair in Dict)
            {
                count += pair.Value;
                if (count >= center)
                {
                    res.Add(pair.Key, pair.Value);
                    return res;
                } 
            }
            
            return Dict; // if got this, something is wrong
        }
        public double Median()
        {
            int count = 0;
            var interval = Median_interval();
            double x = 0;
            double y = 0;
            double value = 0;
            foreach(var k in interval)
            {
                x = k.Key[0];
                y = k.Key[1];
                value = k.Value;
            }
            
            double median = 0;
            int center = (int)Math.Ceiling((double)Dict.Values.Sum() / 2);
            double cent = Dict.Values.Sum() / 2;
            foreach (var pair in Dict)
            {
                count += pair.Value;
                if (count >= center)
                {
                    break;
                }
            }
            median = x + (y - x) * ((cent - count - value) / value);
            return median;
        }
        public double Average()
        {
            double sum = 0;
            int amount = Dict.Values.Sum();
            foreach(var val in Dict)
            {
                sum += val.Value * ((val.Key[0] + val.Key[1]) / 2);
            }
            double average = sum / amount;
            return average;
        }
        public double Swing()
        {
            List<List<double>> list = new List<List<double>>(Dict.Keys);
            return list[list.Count - 2][1] - list[1][0];
        }
        public double Deviation()
        {
            double average = Average();
            double deviation = 0; 
            foreach(var val in Dict)
            {
                deviation += val.Value * Math.Pow(((val.Key[0] + val.Key[1]) / 2) - average, 2);
            }
            return deviation;
        }
        public double Variansa()
        {
            return Deviation() / (Dict.Values.Sum() - 1);
        }
        public double Standart()
        {
            return Math.Sqrt(Variansa());
        }
        public double Variation()
        {
            return Standart() / Average();
        }
        public double Dispresion()
        {
            return Deviation() / Dict.Values.Sum();
        }
        public List<double> Moments()
        {
            List<double> moments = new List<double>();
            double average = Average();
            double sum = Dict.Values.Sum();
            for (int i = 1; i <= 4; i++)
            {
                double moment = 0;
                foreach (var val in Dict)
                {
                    moment = (val.Value - Math.Pow((((val.Key[0] + val.Key[1]) / 2) - average), i))/ sum;
                    moments.Add(moment);
                }
            }
            return moments;
        }
        public double Asymetry()
        {
            var moments = Moments();
            return (moments[3] / Math.Pow(moments[1], 2) - 3); 
        }
        public double Access()
        {
            var moments = Moments();
            return moments[2] / Math.Pow(moments[1], 1.5);
        }
        public Dictionary<string, int> Interqantile_lattitudes()
        {
            Dictionary<string, int> interquantile_latitudes = new Dictionary<string, int>();
            if (List_.Count % 4 == 0)
            {
                var x = List_[List_.Count / 4];
                var y = List_[List_.Count * 3 / 4];
                interquantile_latitudes.Add("Interquartiles", y - x);
            }
            if (List_.Count % 10 == 0)
            {
                var x = List_[List_.Count / 10];
                var y = List_[List_.Count * 9 / 10];
                interquantile_latitudes.Add("Interdeciles", y - x);
            }
            if (List_.Count % 100 == 0 && List_.Count > 100)
            {
                var x = List_[List_.Count / 100];
                var y = List_[List_.Count * 99 / 100];
                interquantile_latitudes.Add("Intercentiles", y - x);
            }
            if (List_.Count % 1000 == 0 && List_.Count > 1000)
            {
                var x = List_[List_.Count / 1000];
                var y = List_[List_.Count * 999 / 1000];
                interquantile_latitudes.Add("Intermililes", y - x);
            }
            return interquantile_latitudes;
        }
    }
}
