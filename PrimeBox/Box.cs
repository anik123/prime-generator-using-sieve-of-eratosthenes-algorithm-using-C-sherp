using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PrimeBox
{
    public partial class Box : Form
    {
        Prime prime;
        public Box()
        {
            InitializeComponent();
            prime = new Prime();
            prime.GetPrime();

        }
        void EnableChange(Control con, bool flag)
        {
            con.Enabled = flag;
        }
        void VisibleChange(Control con, bool flag)
        {
            con.Visible = flag;
        }
        void LoadData()
        {
            int numb1 = 0, numb2 = 0, max = 0, min = 0;

            lvView.Clear();
            if (rbRange.Checked == true)
            {
                numb1 = int.Parse(nmFrom.Value.ToString());
                numb2 = int.Parse(nmTo.Value.ToString());
                if (numb1 < numb2)
                {
                    max = numb2;
                    min = numb1;
                }
                else
                {
                    min = numb2;
                    max = numb1;
                }

                for (int i = min; i <= max; i++)
                {
                    if (prime.prime[i] == false)
                        lvView.Items.Add(i.ToString());
                }
                /* var result = from o in lvView.Items
                               select new { o };
                  */
                if (lvView.Items.Count <= 0)
                    MessageBox.Show("No Prime Number Found!");
                else
                {
                    lvView.Sorting = SortOrder.Ascending;
                    lvView.Sort();
                }
            }
            else
            {
                numb1 = int.Parse(nmNumber.Value.ToString());
                if (rbBelow.Checked == true)
                {
                    min = 2; max = numb1;
                }
                else if (rbUpper.Checked == true)
                {
                    max = 10000000;
                    min = numb1;
                }
                else if (rbSingle.Checked == true)
                {
                    max = numb1;
                    min = numb2;
                }
                for (int i = min; i <= max; i++)
                {

                    if (prime.prime[i] == false)
                        lvView.Items.Add(i.ToString());

                }
                if (lvView.Items.Count <= 0)
                    MessageBox.Show("No Prime Number Found!");
                else
                {

                    lvView.Sort();
                }
            }
        }
        private void listViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvView.View = View.SmallIcon;

        }

        private void tileViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvView.CheckBoxes != true)
                lvView.View = View.Tile;
            else
                MessageBox.Show("To tile view you have to off checkbox!");
        }

        private void smallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvView.View = View.SmallIcon;
        }

        private void largeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvView.View = View.LargeIcon;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lvView.Items.Count > 0)
            {
                lvView.CheckBoxes = true;
                LoadData();
            }
            else
                MessageBox.Show("Search Some Prime Number!");

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            lvView.CheckBoxes = true;
            for (int i = 0; i < lvView.Items.Count; i++)
            {
                lvView.Items[i].Checked = true;
            }
        }

        private void btnDoc_Click(object sender, EventArgs e)
        {

        }

        private void lvView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            EnableChange(btnSelectAll, false);
            EnableChange(btnUnselect, false);
            EnableChange(btnOff, false);
            lvView.CheckBoxes = false;
        }

        private void lvView_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int Item = 0;
            if (lvView.CheckBoxes == true)
            {
                for (int i = 0; i < lvView.Items.Count; i++)
                {
                    if (lvView.Items[i].Checked == true)
                        Item++;
                }

                if (Item > 0)
                    EnableChange(btnUnselect, true);
                if (Item <= 0)
                    EnableChange(btnUnselect, false);
                //btnUnselect.Enabled = true;
                EnableChange(btnSelectAll, true);
                EnableChange(btnOff, true);

            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            sfvFile.Title = "Save text Files";
            //sfvFile.CheckFileExists = true;
            //sfvFile.CheckPathExists = true;
            //saveFileDialog1.DefaultExt = "txt";
            sfvFile.Filter = "Text files (*.txt)|*.txt";
            //saveFileDialog1.FilterIndex = 2;
            sfvFile.RestoreDirectory = true;
            string Name = "";
            if (sfvFile.ShowDialog() == DialogResult.OK)
            {
                if (lvView.CheckBoxes == true)
                {
                    int Item = 0;
                    for (int i = 0; i < lvView.Items.Count; i++)
                    {
                        if (lvView.Items[i].Checked == true)
                        {
                            Item++;
                            Name += lvView.Items[i].Text.ToString() + Environment.NewLine;
                        }
                    }

                    if (Item > 0)
                        File.WriteAllText(sfvFile.FileName, Name);
                    else
                        MessageBox.Show("Please Select Number!");
                }
                else
                {
                    for (int i = 0; i < lvView.Items.Count; i++)
                    {

                        Name += lvView.Items[i].Text.ToString() + Environment.NewLine;

                    }
                    File.WriteAllText(sfvFile.FileName, Name);
                }
                // textBox1.Text = saveFileDialog1.FileName;

            }
        }

        private void rbRange_CheckedChanged(object sender, EventArgs e)
        {
            VisibleChange(lblNumber, false);
            VisibleChange(nmNumber, false);
            VisibleChange(lblFrom, true);
            VisibleChange(lblTo, true);
            VisibleChange(nmFrom, true);
            VisibleChange(nmTo, true);
            VisibleChange(btnReset, true);
            VisibleChange(btnSearch, true);
        }

        private void rbBelow_CheckedChanged(object sender, EventArgs e)
        {
            VisibleChange(lblNumber, true);
            VisibleChange(nmNumber, true);
            VisibleChange(lblFrom, false);
            VisibleChange(lblTo, false);
            VisibleChange(nmFrom, false);
            VisibleChange(nmTo, false);
            VisibleChange(btnReset, true);
            VisibleChange(btnSearch, true);
        }

        private void rbUpper_CheckedChanged(object sender, EventArgs e)
        {
            VisibleChange(lblNumber, true);
            VisibleChange(nmNumber, true);
            VisibleChange(lblFrom, false);
            VisibleChange(lblTo, false);
            VisibleChange(nmFrom, false);
            VisibleChange(nmTo, false);
            VisibleChange(btnReset, true);
            VisibleChange(btnSearch, true);
        }

        private void rbSingle_CheckedChanged(object sender, EventArgs e)
        {
            VisibleChange(lblNumber, true);
            VisibleChange(nmNumber, true);
            VisibleChange(lblFrom, false);
            VisibleChange(lblTo, false);
            VisibleChange(nmFrom, false);
            VisibleChange(nmTo, false);
            VisibleChange(btnReset, true);
            VisibleChange(btnSearch, true);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            rbBelow.Checked = false;
            rbUpper.Checked = false;
            rbRange.Checked = false;
            rbSingle.Checked = false;
            VisibleChange(lblNumber, false);
            VisibleChange(nmNumber, false);
            VisibleChange(lblFrom, false);
            VisibleChange(lblTo, false);
            VisibleChange(nmFrom, false);
            VisibleChange(nmTo, false);
            VisibleChange(btnReset, false);
            VisibleChange(btnSearch, false);
            lvView.Items.Clear();
            EnableChange(btnSelectAll, false);
            EnableChange(btnUnselect, false);
            EnableChange(btnOff, false);
            EnableChange(btnExport, false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            LoadData();
            EnableChange(btnExport, true);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About ab = new About();
            ab.Show();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help h = new Help();
            h.Show();
        }

       


    }
}
