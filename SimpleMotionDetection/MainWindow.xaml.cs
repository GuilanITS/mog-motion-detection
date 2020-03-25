using System;
using System.Windows;
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
        Mat originalFrame;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private void LoadVideoInit()
        {

        }

        private void ProcessVideo(object sender, EventArgs e)
        {
        }
    }
}