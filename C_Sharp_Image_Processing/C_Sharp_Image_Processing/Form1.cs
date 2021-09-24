using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using System.Diagnostics;

namespace C_Sharp_Image_Processing
{   
    public partial class Form1 : Form
    {
        private Bitmap originalImage = new Bitmap("tulips_in_bloom-1920x1200.jpg");
        private Image<Bgr, byte> originalEmguImage = new Image<Bgr, byte>("tulips_in_bloom-1920x1200.jpg");
        private Capture capture;// = new Capture("rtsp://admin:abcdefg@192.168.0.20:554/live1.sdp");       //takes images from camera as image frames
        private bool captureInProgress; // checks if capture is executing

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> img = capture.QueryFrame();  //line 1
            pictureBox1.Image = img.ToBitmap();        //line 2

            StringBuilder msgBuilder = new StringBuilder("Performance: ");
            Stopwatch watch = Stopwatch.StartNew();

            if (radioButton1.Checked)
            {
                #region simple EMGU.CV GravityCenter finding (FASTER)

                Image<Gray, byte> imggray = img.Convert<Gray, byte>();
                Gray grayThreshold = new Gray(50); // First Canny threshold, used for both circle detction, and line / triangle / rectangle / polygon detection
                Gray grayMaxValue = new Gray(255); // Second Canny threshold for circle dtection, higher number = more selective
                Image<Gray, Byte> imgThreshold = imggray.ThresholdBinary(grayThreshold, grayMaxValue); //Thresholding the grayscale image to get better results
                
                MCvMoments moment = (~imgThreshold).GetMoments(true);
                Point gravityCenter = new Point((int)(moment.m10 / moment.m00), (int)(moment.m01 / moment.m00));

                int x = gravityCenter.X;
                int y = gravityCenter.Y;


                CircleF dot = new CircleF(new PointF(x, y), 10);
                imgThreshold.Draw(dot, new Gray(100), -1);

                pictureBox2.Image = imgThreshold.ToBitmap();

                #endregion
            }
            else if (radioButton2.Checked)
            {
                #region comprehensive GravityCenter finding using Emgu (SLOWEST)

                Image<Gray, byte> imggray = img.Convert<Gray, byte>();
                Gray grayThreshold = new Gray(50); // First Canny threshold, used for both circle detction, and line / triangle / rectangle / polygon detection
                Gray grayMaxValue = new Gray(255); // Second Canny threshold for circle dtection, higher number = more selective
                Image<Gray, Byte> imgThreshold = imggray.ThresholdBinary(grayThreshold, grayMaxValue); //Thresholding the grayscale image to get better results
                
                int m = 0;
                int sumX = 0, sumY = 0;
                int x, y;
                for (int i = 0; i < imgThreshold.Width; i++)
                {
                    for (int j = 0; j < imgThreshold.Height; j++)
                    {
                        // get a pixel and check it´s value

                        if (imgThreshold.Data[j, i, 0] == 0)//if it´s white
                        {
                            //store the x and y coordinates of this pixel in the lists

                            sumX += i;
                            sumY += j;
                            m++;
                        }
                    }
                }

                if (m != 0)
                {
                    x = sumX / m;
                    y = sumY / m;

                    CircleF circle = new CircleF(new PointF(x, y), 10);
                    imgThreshold.Draw(circle, new Gray(100), -1);
                }

                pictureBox2.Image = imgThreshold.ToBitmap();

                #endregion
            }
            else if (radioButton3.Checked)
            {
                #region comprehensive GravityCenter finding using C# 2D Array (SLOWER)
                Bitmap BMP = new Bitmap(img.ToBitmap());
                BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
                                                                    BMP.Width, BMP.Height),
                                                                    ImageLockMode.ReadOnly,
                                                                    PixelFormat.Format32bppArgb);
                int x = 0;
                int y = 0;
                int n = 0;
                unsafe
                {
                    byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                    for (int i = 0; i < bitmapData1.Height; i++)
                    {
                        for (int j = 0; j < bitmapData1.Width; j++)
                        {
                            int b = imagePointer1[0];
                            int g = imagePointer1[1];
                            int r = imagePointer1[2];

                            int avg = (b + g + r) / 3;
                            int thresh = 50;

                            if (avg < thresh)
                            {
                                b = g = r = 0;
                                x += j;
                                y += i;
                                n++;
                            }
                            else
                                b = g = r = 255;

                            imagePointer1[0] = (byte)b;
                            imagePointer1[1] = (byte)g;
                            imagePointer1[2] = (byte)r;

                            imagePointer1 += 4;
                        }
                        imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    }

                    if (n != 0)
                    {
                        x /= n;
                        y /= n;
                    }

                    /*
                    int pointSize = 20;
                    imagePointer1 = (byte*)bitmapData1.Scan0;
                    for (int i = 0; i < bitmapData1.Height; i++)
                    {
                        for (int j = 0; j < bitmapData1.Width; j++)
                        {
                            int b = imagePointer1[0];
                            int g = imagePointer1[1];
                            int r = imagePointer1[2];

                            if ((j > x - pointSize) && (j < x + pointSize) && (i > y - pointSize) && (i < y + pointSize))
                            {
                                r = 255;
                                g = 0;
                                b = 255;
                            }

                            imagePointer1[0] = (byte)b;
                            imagePointer1[1] = (byte)g;
                            imagePointer1[2] = (byte)r;

                            imagePointer1 += 4;
                        }
                        imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    }*/
                }
                BMP.UnlockBits(bitmapData1);
                Image<Gray, byte> imggray = new Image<Gray,byte>(BMP);
                CircleF circle = new CircleF(new PointF(x, y), 10);
                imggray.Draw(circle, new Gray(100), -1);
                pictureBox2.Image = imggray.ToBitmap();

                #endregion
            }

            watch.Stop();
            msgBuilder.Append(String.Format("{0} ms", watch.ElapsedMilliseconds));
            label3.Text = msgBuilder.ToString();
        }

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            #region if capture is not created, create it now
            if (capture == null)
            {
                try
                {
                    capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
            #endregion

            if (capture != null)
            {
                if (captureInProgress)
                {  //if camera is getting frames then stop the capture and set button Text
                    // "Start" for resuming capture
                    btnStart.Text = "Capture"; //
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    btnStart.Text = "Stop";
                    Application.Idle += ProcessFrame;
                }

                captureInProgress = !captureInProgress;
            }
            
        }
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = originalImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originalImage;
            Bitmap BMP = new Bitmap(originalImage);
            BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
                                                                BMP.Width, BMP.Height),
                                                                ImageLockMode.ReadOnly,
                                                                PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        int b = imagePointer1[0];
                        int g = imagePointer1[1];
                        int r = imagePointer1[2];

                        b = 255 - b;
                        g = 255 - g;
                        r = 255 - r;

                        imagePointer1[0] = (byte)b;
                        imagePointer1[1] = (byte)g;
                        imagePointer1[2] = (byte)r;

                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            BMP.UnlockBits(bitmapData1);
            pictureBox2.Image = BMP;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originalImage;
            Bitmap BMP = new Bitmap(originalImage);
            BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
                                                                BMP.Width, BMP.Height),
                                                                ImageLockMode.ReadOnly,
                                                                PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        int b = imagePointer1[0];
                        int g = imagePointer1[1];
                        int r = imagePointer1[2];

                        int avg = (b + g + r) / 3;

                        b = g = r = avg;

                        imagePointer1[0] = (byte)b;
                        imagePointer1[1] = (byte)g;
                        imagePointer1[2] = (byte)r;

                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            BMP.UnlockBits(bitmapData1);
            pictureBox2.Image = BMP;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originalImage;
            Bitmap BMP = new Bitmap(originalImage);
            BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
                                                                BMP.Width, BMP.Height),
                                                                ImageLockMode.ReadOnly,
                                                                PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        int b = imagePointer1[0];
                        int g = imagePointer1[1];
                        int r = imagePointer1[2];

                        int avg = (b + g + r) / 3;
                        int thresh = 128;

                        if (avg > thresh)
                            b = g = r = 255;
                        else
                            b = g = r = 0;

                        imagePointer1[0] = (byte)b;
                        imagePointer1[1] = (byte)g;
                        imagePointer1[2] = (byte)r;

                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            BMP.UnlockBits(bitmapData1);
            pictureBox2.Image = BMP;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = originalImage;
            StringBuilder msgBuilder = new StringBuilder("Performance: ");
            Stopwatch watch = Stopwatch.StartNew();

            Bitmap BMP = new Bitmap(originalImage);
            BitmapData bitmapData1 = BMP.LockBits(new Rectangle(0, 0,
                                                                BMP.Width, BMP.Height),
                                                                ImageLockMode.ReadOnly,
                                                                PixelFormat.Format32bppArgb);
            unsafe
            {
                int x = 0;
                int y = 0;
                int n = 0;

                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        int b = imagePointer1[0];
                        int g = imagePointer1[1];
                        int r = imagePointer1[2];

                        int avg = (b + g + r) / 3;
                        int thresh = 128;

                        if (avg < thresh)
                        {
                            b = g = r = 0;
                            x += j;
                            y += i;
                            n++;
                        }
                        else
                            b = g = r = 255;
                        
                        imagePointer1[0] = (byte)b;
                        imagePointer1[1] = (byte)g;
                        imagePointer1[2] = (byte)r;

                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }

                if (n != 0)
                {
                    x /= n;
                    y /= n;
                }

                int pointSize = 20;
                imagePointer1 = (byte*)bitmapData1.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        int b = imagePointer1[0];
                        int g = imagePointer1[1];
                        int r = imagePointer1[2];

                        if ((j > x - pointSize) && (j < x + pointSize) && (i > y - pointSize) && (i < y + pointSize))
                        {
                            r = 255;
                            g = 0;
                            b = 255;
                        }

                        imagePointer1[0] = (byte)b;
                        imagePointer1[1] = (byte)g;
                        imagePointer1[2] = (byte)r;

                        imagePointer1 += 4;
                    }
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            BMP.UnlockBits(bitmapData1);
            pictureBox2.Image = BMP;
            
            watch.Stop();
            msgBuilder.Append(String.Format("{0} ms", watch.ElapsedMilliseconds));
            label3.Text = msgBuilder.ToString();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
