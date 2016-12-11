using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using System.Threading;

namespace Herbata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ReadBase();
        }
        public void ReadBase()
        {
            var reader = new StreamReader(File.OpenRead(@"../../herbaty.csv"));
            teaList = new List<Tea>();
            int records = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                if(records == 0)
                {
                    ++records;
                    continue;
                }
                teaList.Add(new Tea(values[0], Double.Parse(values[1], CultureInfo.InvariantCulture), Int32.Parse(values[2])));
                treeView1.Nodes.Add(new TreeNode(values[0]));
            }
            reader.Close();
            
        }
        private class Tea
        {
            public Tea(string teaname, double teatemp, int teatime)
            {
                name = teaname;
                temp = teatemp;
                time = teatime;
            }
            public string name;
            public double temp;
            public int time;
        }
        private List<Tea> teaList;
        private int timeLeft;
        private Tea selected;
        private void button1_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                timer1.Start();
                timeLeft = selected.time;
                labelTimeLeft.Text = "Time left: " + timeLeft.ToString() + "s";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                labelTimeLeft.Text = "Time left: " + timeLeft.ToString() + "s";
                --timeLeft;
            }
            else
            {
                timer1.Stop();
                labelTimeLeft.Text = "Time elapsed!";
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            for (int i = 0; i < teaList.Count;++i)
            {
                if(teaList.ElementAt(i).name == e.Node.Text)
                {
                    selected = teaList.ElementAt(i);
                    labelName.Text = selected.name;
                    labelTemp.Text = "Temperatura parzenia: " + selected.temp.ToString() + "C";
                    labelTime.Text = "Czas parzenia: " + selected.time.ToString() + "s";
                    break;
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            newTea tea = new newTea();
            tea.ShowDialog();
            if (tea.read)
            {
                teaList.Add(new Tea(tea.name, tea.temp, tea.time));
                treeView1.Nodes.Add(tea.name);
            }
            var writer = new StreamWriter(@"../../herbaty.csv");
            writer.WriteLine("nazwa;temperatura;czas(s)");
            for (int i = 0; i < teaList.Count; ++i)
            {
                writer.WriteLine(teaList.ElementAt(i).name + ";" + teaList.ElementAt(i).temp.ToString("F", CultureInfo.InvariantCulture) + ";" + teaList.ElementAt(i).time.ToString());
            }
            writer.Flush();
            writer.Close();
        }
    }
}
