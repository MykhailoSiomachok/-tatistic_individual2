using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace statistic_individual2
{
    class Discrete
    {
        public double A { get; set; }
        public double B { get; set; }
        public int N { get; set; }
        public List<int> List_ = new List<int>();
        public HashSet<int> Hash = new HashSet<int>();
        public Dictionary<int, int> Dict = new Dictionary<int, int>();
        public Discrete() { }
        public Discrete(double a, double b, int n)
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
        public void Get_dict()
        {
            foreach (var val_hash in Hash)
            {
                int counter = 0;
                foreach (var val_list in List_)
                {
                    if (val_hash == val_list)
                    {
                        counter += 1;
                    }
                }
                Dict.Add(val_hash, counter);
            }
        }
        public double Median()
        {

            if (List_.Count % 2 == 0)
            {
                int x = List_.Count / 2;
                return (List_[x] + List_[x + 1]) / 2;
            }
            else
            {
                int x = (int)Math.Ceiling((double)List_.Count / 2);
                return List_[x];
            }
        }
        public int Mode()
        {
            int mode = 0;
            int max = Dict.Values.Max();
            foreach (var mat in Dict)
            {
                if (mat.Value == max)
                {
                    mode = mat.Key;
                }
            }
            return mode;
        }
        public double Average()
        {
           
            double sum = List_.Sum();
            double average = sum / List_.Count;
            return Math.Round(average,2);
        }
        public int Rozmach()
        {
            int first = List_.First();
            int last = List_.Last();
            int rozmach = last - first;
            return rozmach;
        }
        public double Deviation()
        {
            double res = 0.0;
            double average = Average();
            foreach(var val in Dict)
            {
                res += val.Value * Math.Pow((val.Key - average),2);
            }
            return res;
        }
        public double Variansa()
        {
            return Math.Round(Deviation() / (List_.Count - 1), 3);
        }
        public double Standart()
        {
            return Math.Round(Math.Sqrt(Variansa()), 3);
        }
        public double Dispersion()
        {
            return Math.Round(Deviation() / List_.Count, 3);
        }
        public double Variation()
        {
            return Math.Round(Standart() / Average(), 3);
        }
        public Dictionary<string, int> Interquantile_latitude()
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
        public List<double> Moments()
        {
            List<double> moments = new List<double>();
            double average = Average();
            double sum = Dict.Values.Sum();
            for (int i = 1; i <= 4; i++)
            {
                foreach (var val in Dict)
                {
                    double x = val.Key;
                    double moment = (val.Value - Math.Pow(x - average, i) / sum);
                    moments.Add(moment);
                }
            }
            return moments;
        }
        public double Asymerty()
        {
            var m = Moments();
            double moment2 = m[2] - Math.Pow(m[1], 2);
            double moment3 = m[3] - 3 * m[2] * m[1] - 2 * Math.Pow(m[1], 2);
            double asymetry = (moment3 / Math.Pow(moment2, 1.5));
            return asymetry;
        }
        public double Access()
        {
            var m = Moments();
            double moment3 = m[3] - 3 * m[2] * m[1] + 2 * Math.Pow(m[1], 2);
            double moment4 = m[4] - 4 * m[3] * m[1] + 6 * m[2] * Math.Pow(m[1], 2) - 3 * Math.Pow(m[1], 4);
            double access = (moment4 / Math.Pow(moment3, 2)) - 3;
            return access;
        }

    }
}
