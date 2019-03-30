using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyVideoPlayer
{
    public partial class MainPage : ContentPage
    {
        string[] myvideos = new string[] { "https://sec.ch9.ms/ch9/4c34/35791e22-062a-47df-a165-6d3849e54c34/ApplicationResources.mp4", "https://sec.ch9.ms/ch9/c34c/6513119b-83b4-44c0-abc4-ff619f6bc34c/Browser.mp4", "https://sec.ch9.ms/ch9/6381/d3b6863c-de04-45c1-85fc-830b4f146381/IoTShow_IoTDeviceCert.mp4", "https://sec.ch9.ms/ch9/6b8e/ce73a376-78fe-4ede-9789-02faac466b8e/azfrChigakkagari2.mp4","https://sec.ch9.ms/ch9/14d8/774c408f-d261-4ba9-a1fc-e4e5bbc614d8/WinXMenu.mp4" };
        int index = 0;
        public MainPage()
        {
            InitializeComponent();
            FillInformation();
        }
        // With this event video is playing
        private async void ButtonPlay_Clicked(object sender, EventArgs e)
        {
            PlayVideo(index);
          
        }
        // With this event video is pausing
        private async void ButtonPause_Clicked(object sender, EventArgs e)
        {
            await CrossMediaManager.Current.Pause();
            btnpause.IsVisible = false;
            btnplay.IsVisible = true;
        }

        private void ButtonPlayBack_Clicked(object sender, EventArgs e)
        {

        }
        private async void PlayVideo(int index)
        {
            await CrossMediaManager.Current.Play(myvideos[index], MediaFileType.Video);
            btnplay.IsVisible = false;
            btnpause.IsVisible = true;
        }
        private void ButtonPlayNext_Clicked(object sender, EventArgs e)
        {
            
            index++;
            if (index > 3)
            {
                index = 0;
            }
            PlayVideo(index);
        }
        private void FillInformation()
        {
            List<VideoModel> videoModels = new List<VideoModel>
            {
                new VideoModel
                {
                    VideoImage="x101.png",
                    VideoTitle="Xamarin.Forms 101: Application Resources",
                    VideoIndex=0
                },
                new VideoModel
                {
                    VideoImage="xamarin.png",
                    VideoTitle="Open Browser (Xamarin.Essentials API of the Week)",
                    VideoIndex=1
                },
                new VideoModel
                {
                    VideoImage="iot.png",
                    VideoTitle="Securing your IoT application with Azure Security Center",
                    VideoIndex=2
                },
                new VideoModel
                {
                    VideoImage="azure",
                    VideoTitle="Deploy to Azure using GitHub Actions",
                    VideoIndex=3
                },
                new VideoModel
                {
                    VideoIndex=4,
                    VideoImage="inside.png",
                    VideoTitle="Windows-X Menu"
                }
            };
            lstvideos.BindingContext = videoModels;
            lstvideos.HeightRequest = videoModels.Count * 45;
        }
        class VideoModel
        {
            public string VideoImage { get; set; }
            public string VideoTitle { get; set; }
            public int VideoIndex { get; set; }
        }

        private void Lstvideos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            ListView lv = sender as ListView;
            lv.SelectedItem = null;

            var item = e.SelectedItem as VideoModel;
            index = item.VideoIndex;
            PlayVideo(item.VideoIndex);
        }

        private void ButtonFullScreen_Clicked(object sender, EventArgs e)
        {
            MyVideoView.AspectMode = VideoAspectMode.AspectFill;
        }
    }
}
