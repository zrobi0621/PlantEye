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

        Bitmap pic;
        float sumPixels;
        float bad = 0;

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
                Bitmap origipic = new Bitmap(ofdlg.FileName);
                pic = new Bitmap(ofdlg.FileName);
                pictureBox1.Image = origipic;
                pictureBox2.Image = pic;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                int imgWidth = pictureBox1.Image.Width;
                int imgHeight = pictureBox1.Image.Height;
                sumPixels = imgWidth * imgHeight;
                label1.Text = "Size: " + imgWidth + " x " + imgHeight;

            }


            for (int x = 0; x < pic.Width; x++)
            {
                for (int y = 0; y < pic.Height; y++)
                {
                    Color pixelColor = pic.GetPixel(x, y);
                    if (pixelColor.R >100 && pixelColor.R < 200 && pixelColor.G > 70 && pixelColor.G < 170 && pixelColor.B < 160 && pixelColor.B > 70)
                    {
                        Color myColor = Color.Red;
                        pic.SetPixel(x, y, myColor);
                        bad++;
                    }
                }
            }

            label2.Text = "Bad spots: " + bad/sumPixels*100 + " %";


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
