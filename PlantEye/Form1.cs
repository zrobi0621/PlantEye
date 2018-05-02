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
        Bitmap origipic;
        Bitmap pic;
        float sumPixels;
        float bad = 0;
        int red = 0;
        int green = 0;
        int blue = 0;
        int redTopLimit = 0;
        int redBottomLimit = 0;
        int greenTopLimit = 0;
        int greenBottomLimit = 0;
        int blueTopLimit = 0;
        int blueBottomLimit = 0;
        int percent = 10;
        Bitmap miniRGB = new Bitmap(50, 50);
        Color highlightColor = Color.Red;

        public Form1()
        {
            InitializeComponent();
            this.Text = "PlantEye";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            colorMiniRGBbox(red, green, blue);

            redTextBox.Text = red.ToString();
            greenTextBox.Text = green.ToString();
            blueTextBox.Text = blue.ToString();
            percentTextBox.Text = percent.ToString();

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
                origipic = new Bitmap(ofdlg.FileName);
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

            findBadPixels(0,10, 0,10, 0,10);
            
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            findLimits();
            findBadPixels(redBottomLimit, redTopLimit, greenBottomLimit, greenTopLimit, blueBottomLimit, blueTopLimit);
        }

        //  FIND BAD PIXELS
        void findBadPixels(int redBottomLimit ,int redTopLimit, int greenBottomLimit,int greenTopLimit, int blueBottomLimit, int blueTopLimit)
        {
            bad = 0;
            pic = (Bitmap)origipic.Clone();
            for (int x = 0; x < pic.Width; x++)
            {
                for (int y = 0; y < pic.Height; y++)
                {
                    Color pixelColor = pic.GetPixel(x, y);
                    if (pixelColor.R > redBottomLimit && pixelColor.R < redTopLimit && pixelColor.G > greenBottomLimit && pixelColor.G < greenTopLimit && pixelColor.B  > blueBottomLimit && pixelColor.B < blueTopLimit)
                    {
                        pic.SetPixel(x, y, highlightColor);
                        bad++;
                    }
                }
            }
            pictureBox2.Image = pic;
            label2.Text = "Bad spots: " + bad / sumPixels * 100 + " %";
        }


        //  COLORING MINI RGB BOX
        void colorMiniRGBbox(int red, int green, int blue)
        {
            
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    miniRGB.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            }
            rgbPictureBox.Image = miniRGB;
        }

        //  FIND LIMITS
        void findLimits()
        {
            redTopLimit = red + (red * percent / 100);
            redBottomLimit = red - (red * percent / 100);
            greenTopLimit = green + (green * percent / 100);
            greenBottomLimit = green - (green * percent / 100);
            blueTopLimit = blue + (blue * percent / 100);
            blueBottomLimit = blue - (blue * percent / 100);
            Debug.Text = redBottomLimit + " " + redTopLimit;
        }

        //  TRACKBARS
        private void redTrackBar_Scroll(object sender, EventArgs e)
        {

            red = redTrackBar.Value;
            redTextBox.Text = red.ToString();
            colorMiniRGBbox(red, green, blue);
        }

        private void greenTrackBar_Scroll(object sender, EventArgs e)
        {
            green = greenTrackBar.Value;
            greenTextBox.Text = green.ToString();
            colorMiniRGBbox(red, green, blue);
        }

        private void blueTrackBar_Scroll(object sender, EventArgs e)
        {
            blue = blueTrackBar.Value;
            blueTextBox.Text = blue.ToString();
            colorMiniRGBbox(red, green, blue);
        }

        private void percentTrackBar_Scroll(object sender, EventArgs e)
        {
            percent = percentTrackBar.Value;
            percentTextBox.Text = percent.ToString();

        }

        //TEXTBOX_TEXTCHANGED
        private void redTextBox_TextChanged(object sender, EventArgs e)
        {
            red = int.Parse(redTextBox.Text);
            redTrackBar.Value = red;
            colorMiniRGBbox(red, green, blue);
        }

        private void greenTextBox_TextChanged(object sender, EventArgs e)
        {
            green = int.Parse(greenTextBox.Text);
            greenTrackBar.Value = green;
            colorMiniRGBbox(red, green, blue);
        }

        private void blueTextBox_TextChanged(object sender, EventArgs e)
        {
            blue = int.Parse(blueTextBox.Text);
            blueTrackBar.Value = blue;
            colorMiniRGBbox(red, green, blue);
        }

        private void percentTextBox_TextChanged(object sender, EventArgs e)
        {
            percent = int.Parse(percentTextBox.Text);
            percentTrackBar.Value = percent;

        }

    }
}
