using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_App;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.UI.Views;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ievade : ContentPage
    {
        public ImageSource ImageSource { get; set; }

        readonly SensorSpeed gyroAtrums = SensorSpeed.Default;
        private CameraView cameraView;

        public Ievade()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                Gyroscope.ReadingChanged += GyroUpdate;
                
                //cameraView = new CameraView()
                //{
                //    CaptureMode = CameraCaptureMode.Photo,
                //    CameraOptions = CameraOptions.Back,
                //    HorizontalOptions = LayoutOptions.FillAndExpand,
                //    VerticalOptions = LayoutOptions.FillAndExpand,

                //};
                //cameraView.MediaCaptured += CameraView_MediaCaptured;

                //KamerasRamis.Children.Add(cameraView,0,0);
            }

            InitializeComponent();
        }

        
        private void Slider_ValueChanged_1(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            label1.Text = slider.Value.ToString();
        }

        private void Slider_ValueChanged_2(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            label2.Text = slider.Value.ToString();
        }

        async void UploadPhoto(object sender, System.EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IAttelaIzvele>().GetImageStreamAsync();
            if (stream != null)
            {
                attels.Source = ImageSource.FromStream(() => stream);
            }

            (sender as Button).IsEnabled = true;
        }

        void GyroUpdate(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;

            Slider sliderX = (Slider)FindByName("slider1");
            Slider sliderY = (Slider)FindByName("slider2");

            sliderX.Value = data.AngularVelocity.X * 10;
            sliderY.Value = data.AngularVelocity.Y * 10;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                Gyroscope.Start(gyroAtrums);
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (Device.RuntimePlatform == Device.Android)
            {
                Gyroscope.Stop();
            }
        }

        private void TakePhoto(object sender, System.EventArgs e)
        {
            cameraView.Shutter();
        }
        private void CameraView_MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            ImageSource = e.Image;
            attels.Source = ImageSource;
        }
    }


}