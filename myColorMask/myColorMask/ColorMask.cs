using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace myColorMask
{
    public partial class ColorMask : Form
    {
        //declaring global variables

        // rtsp://admin:abcdefg@192.168.0.20:554/live1.sdp // works in VLC
        // http://admin:abcdefg@192.168.0.20/dms?nowprofileid=1 //JPEG
        // http://admin:abcdefg@192.168.0.20/image/jpeg.cgi //JPEG
        // http://admin:abcdefg@192.168.0.20/dms.jpg //JPEG
        // http://admin:abcdefg@192.168.0.20/ipcam/stream.cgi?nowprofileid=1 //	MJPEG

        private Capture capture;// = new Capture("rtsp://admin:abcdefg@192.168.0.20:554/live1.sdp");       //takes images from camera as image frames
        private bool captureInProgress; // checks if capture is executing
        //private int x, y;
        private bool takeSnapshot;
        private int countSnapshot = 0;
        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> img = capture.QueryFrame();  //line 1
            liveCamera.Image = img;        //line 2
            

            Image<Hsv, byte> hsvimg = img.Convert<Hsv, byte>();

            Image<Gray, byte> imggray = img.Convert<Gray, byte>();
            Image<Gray, byte> imggrayt = img.Convert<Gray, byte>();


            Gray grayThreshold = new Gray(128); // First Canny threshold, used for both circle detction, and line / triangle / rectangle / polygon detection
            Gray grayMaxValue = new Gray(128); // Second Canny threshold for circle dtection, higher number = more selective

            Image<Gray, Byte> imgThreshold = imggray.ThresholdBinary(grayThreshold, grayMaxValue); //Thresholding the grayscale image to get better results


            /*
            byte hueMin = 0;
            byte saturationMin = 100;
            byte valueMin = 100;
            byte hueMax = 0;
            byte saturationMax = 255;
            byte valueMax = 255;
            //byte hueBase = 0;
            byte hueMinR = 170;
            byte hueMaxR = 180;

            byte col = 0;

            //6 textbox for enter 2 hsv value (min and max)
            if (radioRed.Checked)
            {
                //hueBase = 0;
                hueMin = 0;
                hueMax = 15;
                col = 1;
            }
            else if (radioBlue.Checked)
            {
                //hueBase = 120;
                hueMin = 100;
                hueMax = 130;
                col = 2;
            }
            else if (radioGreen.Checked)
            {
                //hueBase = 60;
                hueMin = 40;
                hueMax = 70;
                col = 3;
            }
            else if (radioYellow.Checked)
            {
                //hueBase = 30;
                hueMin = 20;
                hueMax = 35;
                col = 4;
            }
            else if (radioPurple.Checked)
            {
                //hueBase = 150;
                hueMin = 130;
                hueMax = 155;
                col = 5;
            }
            else if (radioCyan.Checked)
            {
                //hueBase = 90;
                hueMin = 85;
                hueMax = 95;
                col = 6;
            }

            if (col != 1)
            {
                //use inrange method
                imggray = hsvimg.InRange(new Hsv(hueMin, saturationMin, valueMin), new Hsv(hueMax, saturationMax, valueMax));
            }
            else
            {
                //use inrange method
                imggray = hsvimg.InRange(new Hsv(hueMin, saturationMin, valueMin), new Hsv(hueMax, saturationMax, valueMax));
                imggrayt = hsvimg.InRange(new Hsv(hueMinR, saturationMin, valueMin), new Hsv(hueMaxR, saturationMax, valueMax));
                imggray = imggray | imggrayt;
            }

            /*
            //define hsv color
            Hsv skin = new Hsv();

            for (int i = 0; i < hsvimg.Height; i++)
                for (int j = 0; j < hsvimg.Width; j++)
                {
                    skin = hsvimg[i, j];

                    if (imggray.Data[i, j, 0] == 255)
                        hsvimg[i, j] = new Hsv(hueBase, 255, 255);
                    else
                        hsvimg[i, j] = new Hsv(0, 0, 0);
                }
            */

            /*
            #region simple EMGU.CV GravityCenter finding (FASTER)
            
            MCvMoments moment = imggray.GetMoments(true);
            Point gravityCenter = new Point((int)(moment.m10 / moment.m00), (int)(moment.m01 / moment.m00));
            
            x = gravityCenter.X;
            y = gravityCenter.Y;

            if ((x >= 0 && x <= imggray.Width) || (y >= 0 && y <= imggray.Height))
            {
                txtX.Text = Convert.ToString(x);
                txtY.Text = Convert.ToString(y);
            }
            else
            {
                txtX.Text = "تهی";
                txtY.Text = "تهی";
            }

            CircleF dot = new CircleF(new PointF(x,y), 10);
            imggray.Draw(dot, new Gray(100), -1);
            
            #endregion

            #region comprehensive GravityCenter finding (SLOWER)
            /*
            int m = 0;
            int sumX = 0, sumY = 0;

            for (int i = 0; i < imggray.Width; i++)
            {
                for (int j = 0; j < imggray.Height; j++)
                {
                    // get a pixel and check it´s value

                    if (imggray.Data[j, i, 0] == 255)//if it´s white
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

                txtX.Text = Convert.ToString(x);
                txtY.Text = Convert.ToString(y);

                CircleF circle = new CircleF(new PointF(x, y), 10);
                imggray.Draw(circle, new Gray(100), -1);
            }
            
            #endregion
            */
            if (takeSnapshot) img.Save("Snapshot" + countSnapshot.ToString() + ".jpg");
            takeSnapshot = false;
            maskedFrame.Image = imgThreshold;

        }

        //Show the image in Windows Form PictureBox called "pictureBox1"
        //pictureBox1.Image = imggray.ToBitmap();

        private void ReleaseData()
        {
            if (capture != null)
                capture.Dispose();
        }

        public ColorMask()
        {
            InitializeComponent();
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
                    btnStart.Text = "شروع"; //
                    Application.Idle -= ProcessFrame;
                }
                else
                {
                    //if camera is NOT getting frames then start the capture and set button
                    // Text to "Stop" for pausing capture
                    btnStart.Text = "توقف";
                    Application.Idle += ProcessFrame;
                }

                captureInProgress = !captureInProgress;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Image<Bgr, Byte> img = new Image<Bgr, byte>("test.jpg");  //line 1
            liveCamera.Image = img;        //line 2

            Image<Hsv, byte> hsvimg = img.Convert<Hsv, byte>();

            Image<Gray, byte> imggray = img.Convert<Gray, byte>();
            Image<Gray, byte> imggrayt = img.Convert<Gray, byte>();
            Image<Gray, byte> imggray2 = img.Convert<Gray, byte>();


            /*
            byte hueMin = 0;
            byte saturationMin = 100;
            byte valueMin = 100;
            byte hueMax = 0;
            byte saturationMax = 255;
            byte valueMax = 255;
            //byte hueBase = 0;
            byte hueMinR = 170;
            byte hueMaxR = 180;

            byte col = 0;

            //6 textbox for enter 2 hsv value (min and max)
            if (radioRed.Checked)
            {
                //hueBase = 0;
                hueMin = 0;
                hueMax = 15;
                col = 1;
            }
            else if (radioBlue.Checked)
            {
                //hueBase = 120;
                hueMin = 100;
                hueMax = 130;
                col = 2;
            }
            else if (radioGreen.Checked)
            {
                //hueBase = 60;
                hueMin = 40;
                hueMax = 70;
                col = 3;
            }
            else if (radioYellow.Checked)
            {
                //hueBase = 30;
                hueMin = 20;
                hueMax = 35;
                col = 4;
            }
            else if (radioPurple.Checked)
            {
                //hueBase = 150;
                hueMin = 130;
                hueMax = 155;
                col = 5;
            }
            else if (radioCyan.Checked)
            {
                //hueBase = 90;
                hueMin = 85;
                hueMax = 95;
                col = 6;
            }

            if (col != 1)
            {
                //use inrange method
                imggray = hsvimg.InRange(new Hsv(hueMin, saturationMin, valueMin), new Hsv(hueMax, saturationMax, valueMax));
            }
            else
            {
                //use inrange method
                imggray = hsvimg.InRange(new Hsv(hueMin, saturationMin, valueMin), new Hsv(hueMax, saturationMax, valueMax));
                imggrayt = hsvimg.InRange(new Hsv(hueMinR, saturationMin, valueMin), new Hsv(hueMaxR, saturationMax, valueMax));
                imggray = imggray | imggrayt;
            }

            /*
            //define hsv color
            Hsv skin = new Hsv();

            for (int i = 0; i < hsvimg.Height; i++)
                for (int j = 0; j < hsvimg.Width; j++)
                {
                    skin = hsvimg[i, j];

                    if (imggray.Data[i, j, 0] == 255)
                        hsvimg[i, j] = new Hsv(hueBase, 255, 255);
                    else
                        hsvimg[i, j] = new Hsv(0, 0, 0);
                }
            */
            
            #region simple EMGU.CV GravityCenter finding (FASTER)
            /*
            MCvMoments moment = imggray.GetMoments(true);
            Point gravityCenter = new Point((int)(moment.m10 / moment.m00), (int)(moment.m01 / moment.m00));
            x = gravityCenter.X;
            y = gravityCenter.Y;
            /*
            if ((x >= 0 && x <= imggray.Width) || (y >= 0 && y <= imggray.Height))
            {
                txtX.Text = Convert.ToString(x);
                txtY.Text = Convert.ToString(y);
            }
            else
            {
                txtX.Text = "تهی";
                txtY.Text = "تهی";
            }
            */
            /*
            CircleF dot = new CircleF(new PointF(x, y), 10);
            imggray.Draw(dot, new Gray(100), -1);
            */
            #endregion

            #region comprehensive GravityCenter finding (SLOWER)
            /*
            int m = 0;
            int sumX = 0, sumY = 0;

            for (int i = 0; i < imggray.Width; i++)
            {
                for (int j = 0; j < imggray.Height; j++)
                {
                    // get a pixel and check it´s value

                    if (imggray.Data[j, i, 0] == 255)//if it´s white
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

                txtX.Text = Convert.ToString(x);
                txtY.Text = Convert.ToString(y);

                CircleF circle = new CircleF(new PointF(x, y), 10);
                imggray.Draw(circle, new Gray(100), -1);
            }
            */
            #endregion
            /*
            //Load the image from file and resize it for display
            Image<Bgr, Byte> rgbimg = imggray.Convert<Bgr, Byte>();
            
            // Dimensions (in centimeters)
            double fieldRealSize = 250;
            double minObjectSize = 10;
            double maxObjectSize = 30;
            double robotSize = 20;
            double minDistBetweenObjects = 10;

            // Canny and edge detection
            double dblGrayCannyThreshold = 160;
            double dblGrayCircleAccumThreshold = 120;
            double dblGrayThreshLinking = 80;

            Gray grayCannyThreshold = new Gray(dblGrayCannyThreshold); // First Canny threshold, used for both circle detction, and line / triangle / rectangle / polygon detection
            Gray grayCircleAccumThreshold = new Gray(dblGrayCircleAccumThreshold); // Second Canny threshold for circle dtection, higher number = more selective
            Gray grayThreshLinking = new Gray(dblGrayThreshLinking); // Second Canny threshold for line / triangle / rectangle / polygon detection

            // HoughCircles arguments
            double dblAccumRes = 3; // Resolution of the accumulator used to detect centers of circles
            double dblMinDistBetweenCircles = 60; // Min distance between centers of detected circles
            int intMinRadius = 5; // Min radius of circles to search for
            int intMaxRadius = 0; // Max radius of circles to search for

            //Image<Gray, Byte> imgCanny = imggray.Canny(dblGrayCannyThreshold, dblGrayThreshLinking); // Canny image used for line, triangle, rectangle, and polygon dtection
            */
            // ThresholdBinary arguments
            Gray grayThreshold = new Gray(128); // First Canny threshold, used for both circle detction, and line / triangle / rectangle / polygon detection
            Gray grayMaxValue = new Gray(255); // Second Canny threshold for circle dtection, higher number = more selective
            /*
            Image<Gray, Byte> imgThreshold0 = imggray2.ThresholdBinary(grayThreshold, grayMaxValue); //Thresholding the grayscale image to get better results
            
            // Find triangles and rectangles
            Contour<Point> contours0 = imgThreshold0.FindContours(); // Find a sequence (similar to a linked list) of contours using the simple approximation method
            List<MCvBox2D> lstRectangles0 = new List<MCvBox2D>(); // Declare list of rectangles

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
                while (contours0 != null)
                {
                    Contour<Point> contour = contours0.ApproxPoly(contours0.Perimeter * 0.025, storage); // Approximates one or more curves and returns the approximation results
                    if (contour.Area > 250.0)
                    {
                        if (contour.Total == 4) // If 4, 5, or 6 points, could be a rectangle or a polygon
                        {
                            Point[] ptPoints = contour.ToArray(); // Get contour points
                            bool blnIsRectangle = true; // To start with, let's suppose it's a rectangle

                            LineSegment2D[] ls2dEdges = PointCollection.PolyLine(ptPoints, true); // Get edges between points
                            for (int i = 0; i <= ls2dEdges.Length - 1; i++) // Step through edges
                            {
                                double dblAngle = Math.Abs(ls2dEdges[(i + 1) % ls2dEdges.Length].GetExteriorAngleDegree(ls2dEdges[i]));
                                if (dblAngle < 80.0 || dblAngle > 100.0) // If not about 90 degrees angle between edges
                                {
                                    blnIsRectangle = false; // Then it's not a rectangle
                                    break;
                                }
                            }

                            if (blnIsRectangle) // If a rectangle
                            {
                                lstRectangles0.Add(contour.GetMinAreaRect()); // Add to list of rectangles
                                rgbimg.Draw(contour.GetMinAreaRect(), new Bgr(Color.Red), 2);
                                double[] len = new double[2];
                                len[0] = Math.Pow((ptPoints[0].X - ptPoints[1].X), 2) + Math.Pow((ptPoints[0].Y - ptPoints[1].Y), 2);
                                len[1] = Math.Pow((ptPoints[1].X - ptPoints[2].X), 2) + Math.Pow((ptPoints[1].Y - ptPoints[2].Y), 2);
                                txtX.Text = txtX.Text + " | " + Convert.ToString(len[0]) + "," +Convert.ToString(len[1]);
                            }
                        }
                    }
                    contours0 = contours0.HNext;
                }

            double fieldImageSize;
            */
            Image<Gray, Byte> imgThreshold = imggray.ThresholdBinary(grayThreshold, grayMaxValue); //Thresholding the grayscale image to get better results

            /*
            // Find circles
            CircleF[] circles = imgThreshold.HoughCircles(grayCannyThreshold, grayCircleAccumThreshold, dblAccumRes, dblMinDistBetweenCircles, intMinRadius, intMaxRadius)[0];
			
            // Find triangles and rectangles
            Contour<Point> contours = imgThreshold.FindContours(); // Find a sequence (similar to a linked list) of contours using the simple approximation method
            List<Triangle2DF> lstTriangles = new List<Triangle2DF>(); // Declare list of triangles
            List<MCvBox2D> lstRectangles = new List<MCvBox2D>(); // Declare list of rectangles
            List<Contour<Point>> lstPentagons = new List<Contour<Point>>(); // Declare list of pentagons
            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
            while (contours != null)
            {/*
                Contour<Point> contour = contours.ApproxPoly(contours.Perimeter * 0.025, storage); // Approximates one or more curves and returns the approximation results
                if (contour.Area > 250.0)
                {
                    if (contour.Total == 3) // If 3 points, it's a triangle
                    {
                        Point[] ptPoints = contour.ToArray(); // Get contour points
                        lstTriangles.Add(new Triangle2DF(ptPoints[0], ptPoints[1], ptPoints[2])); // And add to triangle list
                        if (radioTriangle.Checked)
                        {
                            rgbimg.Draw(new Triangle2DF(ptPoints[0], ptPoints[1], ptPoints[2]), new Bgr(Color.Blue), 2);
                            int dotX = (ptPoints[0].X + ptPoints[1].X + ptPoints[2].X) / 3;
                            int dotY = (ptPoints[0].Y + ptPoints[1].Y + ptPoints[2].Y) / 3;
                            CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                            rgbimg.Draw(dot, new Bgr(Color.Blue), -1);
                        }
                    }
                    else if (contour.Total == 4) // If 4, 5, or 6 points, could be a rectangle or a polygon
                    {
                        Point[] ptPoints = contour.ToArray(); // Get contour points
                        bool blnIsRectangle = true; // To start with, let's suppose it's a rectangle

                        LineSegment2D[] ls2dEdges = PointCollection.PolyLine(ptPoints, true); // Get edges between points
                        for (int i = 0; i <= ls2dEdges.Length - 1; i++) // Step through edges
                        {
                            double dblAngle = Math.Abs(ls2dEdges[(i + 1) % ls2dEdges.Length].GetExteriorAngleDegree(ls2dEdges[i]));
                            if (dblAngle < 80.0 || dblAngle > 100.0) // If not about 90 degrees angle between edges
                            {
                                blnIsRectangle = false; // Then it's not a rectangle
                                break;
                            }
                        }

                        if (blnIsRectangle) // If a rectangle
                        {
                            lstRectangles.Add(contour.GetMinAreaRect()); // Add to list of rectangles
                            if (radioRectangle.Checked)
                            {
                                rgbimg.Draw(contour.GetMinAreaRect(), new Bgr(Color.Red), 2);
                                int dotX = (ptPoints[0].X + ptPoints[1].X + ptPoints[2].X + ptPoints[3].X) / 4;
                                int dotY = (ptPoints[0].Y + ptPoints[1].Y + ptPoints[2].Y + ptPoints[3].Y) / 4;
                                CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                                rgbimg.Draw(dot, new Bgr(Color.Red), -1);
                            }
                        }
                    }
                    else if (contour.Total == 5)
                    {
                        lstPentagons.Add(contour); // Add to list of pentagons
                        if (radioPentagon.Checked)
                        {
                            Point[] ptPoints = contour.ToArray(); // Get contour points
                            rgbimg.Draw(contour, new Bgr(Color.Purple), 2);
                            int dotX = (ptPoints[0].X + ptPoints[1].X + ptPoints[2].X + ptPoints[3].X + ptPoints[4].X) / 5;
                            int dotY = (ptPoints[0].Y + ptPoints[1].Y + ptPoints[2].Y + ptPoints[3].Y + ptPoints[4].Y) / 5;
                            CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                            rgbimg.Draw(dot, new Bgr(Color.Purple), -1);
                        }
                    }
                }

                MCvMoments moment = contours.GetMoments();
                float x = (float)moment.GravityCenter.x;
                float y = (float)moment.GravityCenter.y;
                
                rgbimg.Draw(contours, new Bgr(Color.Purple), -1);
                
                CircleF dot = new CircleF(new PointF(x, y), 3);
                rgbimg.Draw(dot, new Bgr(Color.Red), -1);

                contours = contours.HNext;
            }

            // draw circles
            if (radioCircle.Checked)
            {
                //Image<Bgr, Byte> circleImage = img.CopyBlank();
                foreach (CircleF circle in circles)
                {
                    rgbimg.Draw(circle, new Bgr(Color.Green), 2);
                    float dotX = circle.Center.X;
                    float dotY = circle.Center.Y;
                    CircleF dot = new CircleF(new PointF(dotX, dotY), 3);
                    rgbimg.Draw(dot, new Bgr(Color.Green), -1);
                }
            }
*/
            // draw triangles and rectangles
            /*
            else if (radioTriangle.Checked)
            {
                foreach (Triangle2DF triangle in lstTriangles)
                    rgbimg.Draw(triangle, new Bgr(Color.Blue), 2);
            }
            else if (radioRectangle.Checked)
            {
                foreach (MCvBox2D rectangle in lstRectangles)
                    rgbimg.Draw(rectangle, new Bgr(Color.Red), 2);
            }
            else if (radioPentagon.Checked)
            {
                foreach (Contour<Point> pentagon in lstPentagons)
                {
                    //rgbimg.Draw(pentagon, new Bgr(Color.Purple), 2);
                    /*
                    Point[] ptPoints = pentagon.ToArray(); // Get contour points
                    rgbimg.Draw(new LineSegment2D(ptPoints[0], ptPoints[1]), new Bgr(Color.Purple), 2);
                    rgbimg.Draw(new LineSegment2D(ptPoints[1], ptPoints[2]), new Bgr(Color.Purple), 2);
                    rgbimg.Draw(new LineSegment2D(ptPoints[2], ptPoints[3]), new Bgr(Color.Purple), 2);
                    rgbimg.Draw(new LineSegment2D(ptPoints[3], ptPoints[4]), new Bgr(Color.Purple), 2);
                    rgbimg.Draw(new LineSegment2D(ptPoints[4], ptPoints[0]), new Bgr(Color.Purple), 2);
                    */
            /*
                }
            }
    
            */

            maskedFrame.Image = imgThreshold;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            takeSnapshot = true;
            countSnapshot++;
        }
    }
}
