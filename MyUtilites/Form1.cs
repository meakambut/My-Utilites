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

        public MainForm()
        {
            InitializeComponent();
            rand = new Random();
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
    }
}