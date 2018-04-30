using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlantEye
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.Text = "PlantEye";

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbox = new AboutBox1();
            aboutbox.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

          
            //openFileDialog1.InitialDirectory = "c:\\";
            //openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;

            OpenFileDialog ofdlg = new OpenFileDialog();

            ofdlg.Title = "Open Image";
            //ofdlg.Filter = "bmp files (*.bmp)|*.bmp";

            if (ofdlg.ShowDialog() == DialogResult.OK)
            {
                //PictureBox PictureBox1 = new PictureBox();
                pictureBox1.Image = new Bitmap(ofdlg.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
