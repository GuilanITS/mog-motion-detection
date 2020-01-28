# Motion detection by MOG Background Subtraction
Background Subtraction is a commonly used method to segment moving parts from static scenes (background and foreground). The moving regions which contain edges of vehicles are detected by subtracting the current frame of the video from a reference static background. The reference background creation process is known as background modeling. The background model must be continuously updated and contain no moving objects.
##### 1. Mixture of Gaussian (MOG) background subtraction
One of the extensions to the common background subtraction method is **Mixture of Gaussian (MOG)** background subtraction that is dependent to a combination of frames instead of only one frame. In this method, for each background pixel, a mixture of *k* Gaussian distribution and a weighting parameter are utilized to save the lifetime of pixels in the scene, where *k* can vary in the range of 3 to 5. Thus, remaining pixels with more than a threshold time in the scene means they have the higher possibility of belonging to the background scene. On the other hand, if the pixel remains unchanged for a period of time, it is considered as a dominant background pixel. To update the model, **MoG-BS** method can be used as an online approximation scale. If the difference of pixels in a frame is more than a predefined threshold, they are classified as moving parts. This method is very sensitive to changes in the environment. To use this method, we can simply use below code in C#:
```csharp
BackgroundSubtractorMOG mog = new BackgroundSubtractorMOG(mog_history, mog_nMixtures, mog_backgroundRatio, mog_noiseSigma);

// Appropriate function
mog.apply();
```

##### 2. Mixture of Gaussian (MOG) background subtraction
Another Gaussian Mixture-based Background/Foreground segmentation algorithm is known as MOG2. The difference between MOG and MOG2 is that MOG2 selects the appropriate number of gaussian distribution for each pixel, where MOG takes a *K* gaussian distribution for modeling. For this reason, MOG2 provides a better adaptibility to varying scenes due illumination changes. We can also set the algorithm to detect shadows as well.

As in previous case, we have to create a background subtractor object. Here, you have an option of selecting whether shadow to be detected or not. If detectShadows = True (which is so by default), it detects and marks shadows, but decreases the speed. Shadows will be marked in gray color.
```csharp
bool detectShadows = True;
BackgroundSubtractorMOG2 mog2 = new BackgroundSubtractorMOG2(history, varThreshold, detectShadows);

// Appropriate function
mog2.Update(frame);
```

### Environment
The application is implemented in **C#** programming language and utilizes **AForge.Net** and **EmguCV** image processing libraries.

### Sample Output
A sample output of the application is presented in below image. As it can be seen, calibration of the input video to omit any existing noises is really important.

### References
1. P. KaewTraKulPong and R. Bowden, "***An Improved Adaptive Background Mixture Model for Real-time Tracking with Shadow Detection***,” 2nd European Workshop on Advanced Video-based Surveillance Systems, Genova, 2002. ([link](https://www.researchgate.net/publication/2557021_An_Improved_Adaptive_Background_Mixture_Model_for_Realtime_Tracking_with_Shadow_Detection "link"))
2. Z. Zivkovic, ***"Improved Adaptive Gaussian Mixture Model for Background Subtraction,"*** Proceedings of the 17th International Conference on Pattern Recognition, Cambridge, 2004. ([link](https://www.researchgate.net/publication/4090386_Improved_Adaptive_Gaussian_Mixture_Model_for_Background_Subtraction "link"))
3. A. Tourani, A. Shahbahrami, A. Akoushideh, S. Khazaee, and C. Y Suen "***Motion-based Vehicle Speed Measurement for Intelligent Transportation Systems***," International Journal of Image, Graphics and Signal Processing, vol. 11, no. 4, pp. 42-54, 2019. ([link](https://www.researchgate.net/publication/332297032_Motion-based_Vehicle_Speed_Measurement_for_Intelligent_Transportation_Systems "link"))
4. A. Tourani, A. Shahbahrami and A. Akoushideh, "***Challenges of Video-Based Vehicle Detection and Tracking in Intelligent Transportation Systems***," International Conference on Soft Computing, Rudsar, 2017. ([link](https://www.researchgate.net/publication/321254958_Challenges_of_Video-Based_Vehicle_Detection_and_Tracking_in_Intelligent_Transportation_Systems "link"))
