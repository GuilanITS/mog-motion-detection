using System;
using System.Windows;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;

namespace SimpleMotionDetection
{
    /// <summary>
    /// A project to check the performance of Motion Detection algorithms
    /// </summary>
    public partial class MainWindow : Window
    {
        // Global Vaiables
        VideoCapture capturedVideo;                 // Capture Video
        double TotalFrames = 0;                     // Video's Total Freams
        double FrameCounter = 0;                    // Video's Frame-rate
        decimal playingState = 0;                   // Set the State of Showing Frames (= Combobox Index)
        Mat originalFrame, displayingFrame, thresholdedFrame;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            // When the user clicks on the "Load Video" button
            OpenFileDialog loadVideoDialog = new OpenFileDialog();
            loadVideoDialog.Filter = "Video files (*.avi, *.mkv, *.mp4) | *.avi; *.mkv; *.mp4";
            loadVideoDialog.ShowDialog();
            try
            {
                capturedVideo = new VideoCapture(loadVideoDialog.FileName);
                if (loadVideoDialog.FileName != "")
                {
                    LoadVideoInit();
                    originalFrame = new Mat();
                    capturedVideo.ImageGrabbed += ProcessVideo;
                    capturedVideo.Start();
                }
                else
                {
                    return;
                }
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("Something went wrong!\n" + err.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            loadVideoDialog.Dispose();
        }

        private void LoadVideoInit()
        {
            TotalFrames = Convert.ToDouble(capturedVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount));
            playingState = 0;
        }

        private void ProcessVideo(object sender, EventArgs e)
        {
            FrameCounter++;
            try
            {
                // Check the end of video
                if (FrameCounter == TotalFrames)
                {
                    capturedVideo.Dispose();
                    return;
                }
                capturedVideo.Retrieve(originalFrame);
                // Check which frames to show
                if (playingState == 0)
                    displayingFrame = originalFrame.Clone();
                else if (playingState == 1)
                    displayingFrame = thresholdedFrame.Clone();
                else if (playingState == 2)
                    displayingFrame = thresholdedFrame.Clone();
                // Use another thread to update UI
                this.Dispatcher.Invoke(() =>
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    using (MemoryStream memoryLocation = new MemoryStream())
                    {
                        displayingFrame.Bitmap.Save(memoryLocation, ImageFormat.Png);
                        memoryLocation.Position = 0;
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = memoryLocation;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                    }
                    ImageViewer.Source = bitmapImage;
                });
            }
            catch (Exception err)
            {
                System.Windows.MessageBox.Show("Something went wrong!\n" + err.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void RadioButtonMain_Checked(object sender, RoutedEventArgs e)
        {
            playingState = 0;
        }

        private void RadioButtonMog_Checked(object sender, RoutedEventArgs e)
        {
            playingState = 1;
        }

        private void RadioButtonMog2_Checked(object sender, RoutedEventArgs e)
        {
            playingState = 2;
        }
    }
}