using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Кортежи
{
    public partial class Form1 : Form
    {
        public RoadMap map;
        public Form1()
        {
            InitializeComponent();
            map = new RoadMap();
            List<(string, string, int)> towns=map.print_towns();
            foreach((string, string, int) x in towns)
            listView1.Items.Add(x.ToString());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            map.fill(textBox1.Text);
            List<(string, int, string, Boolean)> roads = map.print_roads();
            foreach ((string, int, string, Boolean) x in roads)
            listView2.Items.Add("Из "+ textBox1.Text+" в "+x.Item1+" стоит "+x.Item2+". Путь: "+x.Item3);
        }
    }
}
