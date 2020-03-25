using System;
using System.Windows;
using Microsoft.Win32;
using Emgu.CV;

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
        Mat originalFrame;

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
                if (loadVideoDialog.FileName != "")
                {
                    LoadVideoInit();
                    capturedVideo = new VideoCapture(loadVideoDialog.FileName);
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
                MessageBox.Show("Something went wrong!\n" + err.ToString(), "Error!", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }


        private void LoadVideoInit()
        {
            TotalFrames = Convert.ToDouble(capturedVideo.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount));
        }

        private void ProcessVideo(object sender, EventArgs e)
        {
            FrameCounter++;
            // Check the end of video
            if (FrameCounter == TotalFrames)
            {
                capturedVideo.Dispose();
                return;
            }
            capturedVideo.Retrieve(originalFrame);
        }
    }
}