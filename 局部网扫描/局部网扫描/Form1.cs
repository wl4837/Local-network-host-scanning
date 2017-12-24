using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;

namespace 局部网扫描
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Truncate_IP(string IP,bool type_bool) {
            string Truncate = "";
            if (type_bool == true)
            {
                MatchCollection matchCollection = Regex.Matches(IP, "[0-9]{1,3}");
                foreach (Match item in matchCollection)
                {
                    Truncate = item.Value;
                }
            }
            else
            {
                MatchCollection matchCollection = Regex.Matches(IP, "[0-9]{1,3}");
                int Item_count = 0;
                foreach (Match item in matchCollection)
                {
                    ++Item_count;
                    if (matchCollection.Count == Item_count)
                    {

                    }
                    else
                    {

                        Truncate += (item.Value + ".");
                    }
                }
        }

            return Truncate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Thread thread = new Thread(new ThreadStart(button1Thread));
            thread.Start();
        }

        private void button1Thread() {
            listView1.Items.Clear();
            button1.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            progressBar1.Value = 0;
            if (IPAddress.TryParse(textBox1.Text, out IPAddress iP) && IPAddress.TryParse(textBox2.Text, out IPAddress ip))
            {
                progressBar1.Maximum = 1 + (int.Parse(Truncate_IP(textBox2.Text, true)) - int.Parse(Truncate_IP(textBox1.Text, true)));
                for (int i = int.Parse(Truncate_IP(textBox1.Text, true)); i <= int.Parse(Truncate_IP(textBox2.Text, true)); i++)
                {
                    Thread.Sleep(20);
                    ++progressBar1.Value;
                    Control.CheckForIllegalCrossThreadCalls = false;
                    Thread thread = new Thread(new ParameterizedThreadStart(retrieval));
                    thread.Start(Truncate_IP(textBox1.Text, false) + i);
                    if (progressBar1.Value == progressBar1.Maximum)
                    {
                        button1.Enabled = true;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                    }
                }
            }
            else
            {
                MessageBox.Show("IP地址不合法");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void retrieval(object pathinfo) {
            try
            {
                ListViewItem viewItem = new ListViewItem(pathinfo.ToString());
                viewItem.SubItems.Add(Dns.GetHostByAddress(pathinfo.ToString()).HostName);
                listView1.Items.Add(viewItem);
            }
            catch (Exception)
            {

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //获取起始地址s
           MatchCollection matchCollection = Regex.Matches(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString(), "[0-9]{1,3}");
            int Item_count = 0;
            foreach (Match item in matchCollection)
            {
                ++Item_count;
                if (matchCollection.Count == Item_count)
                {
                    textBox1.AppendText("1");
                }
                else
                {
                    switch (item.Length)
                    {
                        case 1:
                            textBox1.AppendText(item.Value + ".");
                            break;
                        case 2:
                            textBox1.AppendText(item.Value + ".");
                            break;
                        default:
                            textBox1.AppendText(item.Value + ".");
                            break;
                    }
                    
                }
            }
        }

        private void Ip_2(string Ip_1) {
            textBox2.Text = null;
            MatchCollection matchCollection = Regex.Matches(Ip_1, "[0-9]{1,3}");
            int Item_count = 0;
            foreach (Match item in matchCollection)
            {
                ++Item_count;
                if (matchCollection.Count == Item_count)
                {
                    textBox2.AppendText("255");
                }
                else
                {
                    textBox2.AppendText(item.Value + ".");
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            Ip_2(textBox1.Text);
        }

        private void progressBar1_VisibleChanged(object sender, EventArgs e)
        {
           
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            
        }
    }
}
