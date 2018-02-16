using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtilites
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rand;
        Dictionary<string, double> metricaLength;
        Dictionary<string, double> metricaWeight;
        Dictionary<string, double> metricaCurrent;

        string [] unitsLength = new string[] {"mm","cm","dm","m","km","mile"};
        string [] unitsWeight = new string[] { "g", "kg", "t", "lb","oz" };

        char[] SpecialSymbols = new char[] { '%', ')', '?', '#', '$', '^', '&', '~' };

        public MainForm()
        {
            InitializeComponent();
            rand = new Random();

            metricaLength = new Dictionary<string, double>();
            metricaLength.Add("mm", 1);
            metricaLength.Add("cm", 10);
            metricaLength.Add("dm", 100);
            metricaLength.Add("m", 1000);
            metricaLength.Add("km", 1000000);
            metricaLength.Add("mile", 1609344);

            metricaWeight = new Dictionary<string, double>();
            metricaWeight.Add("g", 1);
            metricaWeight.Add("kg", 1000);
            metricaWeight.Add("t", 1000000);
            metricaWeight.Add("lb", 453.6);
            metricaWeight.Add("oz", 28.3);

            cbMetric.Text = "length";
            metricaCurrent = metricaLength;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Olya's first Windows Forms ever.\nYahoo!", "About program");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
                lblCount.Text = count.ToString();
            }
            else
                count = 0;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = Convert.ToString(count);
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int n;
            if (from.Value <= to.Value)
            {
                n = rand.Next(Convert.ToInt32(from.Value), Convert.ToInt32(to.Value)+1);
                lblRandom.Text = n.ToString();
                if (cbRandom.Checked)
                {
                    int i = 0;
                    while (tbRandom.Text.IndexOf(n.ToString()) != -1)
                    {
                        n = rand.Next(Convert.ToInt32(from.Value), Convert.ToInt32(to.Value) + 1);
                        i++;
                        if (i > 100) break;
                    }
                    if(i <= 100)
                        tbRandom.AppendText(n + "\r\n"); //автоматическое приведение к string
                }
                else
                    tbRandom.AppendText(n + "\r\n");
            }
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopyBuf_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(tbRandom.Text) || !String.IsNullOrWhiteSpace(tbRandom.Text))
                Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString()+"\r\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\r\n");
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            OpenNotepad();
        }

        void OpenNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("error saving file\r\n");
            }
        
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("error saving file\r\n");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenNotepad();
            clbPasswordSettings.SetItemChecked(2,true);
        }

        private void btnGeneratePassword_Click(object sender, EventArgs e)
        {
            if (clbPasswordSettings.CheckedItems.Count==0) return;
            string password = "";
            for (int i=0; i<nudPasswordLength.Value; i++)
            {
                int n = rand.Next(0, clbPasswordSettings.CheckedItems.Count);
                string s = clbPasswordSettings.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Digits":
                        {
                            password += Convert.ToString(rand.Next(0, 10));
                            break;
                        }
                    case "Capital letters":
                        {
                            password += Convert.ToChar(rand.Next(65, 91));
                            break;
                        }
                    case "Small letters": 
                        {
                            password += Convert.ToChar(rand.Next(97, 123));
                            break;
                        }
                    case "Special symbols: %,),?,#,$,^,&,~":
                        {
                            password += SpecialSymbols[rand.Next(0, SpecialSymbols.Length)];
                            break;
                        }


                }
                tbPassword.Text = password;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPassword.Text) || !String.IsNullOrWhiteSpace(tbPassword.Text))
                Clipboard.SetText(tbPassword.Text);
        }

        private void clbPasswordSettings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metricaCurrent[cbFrom.Text];
            double m2 = metricaCurrent[cbTo.Text];
            tbTo.Text = Convert.ToString(Convert.ToDouble(tbFrom.Text) * m1 / m2);
        }

        private void btnExchange_Click(object sender, EventArgs e)
        {
            string tmp;
            tmp = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = tmp;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetric.Text)
            {
                case "length":
                    {
                        metricaCurrent = metricaLength;

                        cbFrom.Items.Clear();
                        cbFrom.Items.AddRange(unitsLength);
                        cbFrom.Text = "mm";

                        cbTo.Items.Clear();
                        cbTo.Items.AddRange(unitsLength);
                        cbTo.Text = "mm";
                        break;
                    }
                case "weight":
                    {
                        metricaCurrent = metricaWeight;
                        cbFrom.Items.Clear();
                        cbFrom.Items.AddRange(unitsWeight);
                        cbFrom.Text = "g";

                        cbTo.Items.Clear();
                        cbTo.Items.AddRange(unitsWeight);
                        cbTo.Text = "g";
                        break;
                    }
            }
        }
    }
}
