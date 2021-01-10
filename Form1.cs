using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using IntXLib;

namespace RSA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string path = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + "Текст.txt";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] text = File.ReadAllText(path).Split(',');
            char[] kir = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я' };
            char[] kir1 = { ' ', 'О', 'Е', 'И', 'А', 'Н', 'Т', 'С', 'В', 'Р', 'Л', 'К', 'М', 'Д', 'Ы', 'П', 'У', 'З', 'Б', 'Г', 'Я', 'Ь', 'Ч', 'Й', 'Х', 'Ж', 'Ю', 'Э', 'Ц', 'Ф', 'Щ', 'Ш', 'Ъ', 'Ё' };
            int N = 445081;
            double E = 180233;
            double p = 469;
            double q = 949;
            double F = (p - 1) * (q - 1);
            uint d = 0;
            do
            {
                d++;
            } while ((d * E) % F != 1);



            List<Tuple<int, int>> chast = new List<Tuple<int, int>>();
            List<int> c = new List<int>();
            string st = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Convert.ToInt32(text[i]) == 0)
                {
                    c.Add(0);
                    st += "0,";
                }
                else
                {
                    var s = IntX.Pow(Convert.ToInt32(text[i]), d) % N;
                    st += s.ToString() + ",";
                    c.Add((int)s);
                }
            }

            st.Remove(st.Length - 1, 1);
            var itog = st.Split(',');



            foreach (var s in c)
            {
                int ch = c.Select(x => x).Where(x => x == s).Count();
                if (!chast.Contains(Tuple.Create<int, int>(ch, s)))
                {
                    chast.Add(Tuple.Create<int, int>(ch, s));
                }
            }
            chast = chast.Select(x => x).OrderByDescending(x => x.Item1).ToList();

            foreach (var s in chast)
            {
                char s1 = '\0';
                if (s.Item2 != 0)
                {
                    s1 = kir[s.Item2 - 10];
                }
                else
                {
                    s1 = '0';
                }
                listBox1.Items.Add(s.Item1 + "\t" + s1);
            }

            int k = 0;
            string otvet = "";
            for (int i = 0; i < itog.Length - 1; i++)
            {
                if (chast.Select(x => x.Item2).Where(x => x == Convert.ToInt32(itog[i])).Count() > 0)
                {
                    var s = chast.IndexOf(chast.Select(x => x).Where(x => x.Item2 == Convert.ToInt32(itog[i])).First()).ToString();
                    if (Convert.ToInt32(s) != 0)
                        otvet += kir1[Convert.ToInt32(s)];
                    else
                        otvet += " ";
                }
            }

            textBox1.Text += otvet;
        }
    }
}
