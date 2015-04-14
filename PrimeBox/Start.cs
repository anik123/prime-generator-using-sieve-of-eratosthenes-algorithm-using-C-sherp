using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PrimeBox
{
    public partial class Start : Form
    {
        public Start()
        {
            InitializeComponent();
            Spy.WorkerReportsProgress = true;
            Spy.WorkerSupportsCancellation = true;
            Spy.DoWork += new DoWorkEventHandler(Spy_DoWork);
            Spy.ProgressChanged += new ProgressChangedEventHandler(Spy_ProgressChanged);
            //Spy.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Spy_RunWorkerCompleted);
        }
        private void Spy_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                if (Spy.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(100);
                    Spy.ReportProgress(i);
                }
            }

        }

        private void Spy_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (progressBar1.Value >= 100)
            {
                Spy.DoWork -= new DoWorkEventHandler(Spy_DoWork);
                Spy.ProgressChanged -= new ProgressChangedEventHandler(Spy_ProgressChanged);
               // Spy.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(Spy_RunWorkerCompleted);
                Spy.CancelAsync();
                Spy.Dispose();
                this.Hide();
                Box l = new Box();
                l.Show();

            }
            else
            {
                this.Text = e.ProgressPercentage.ToString();
                progressBar1.Value = e.ProgressPercentage;
                //label1.Text = Application.DoEvents();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Spy.RunWorkerAsync();

            progressBar1.ForeColor = Color.Red;
        }
    }
}
