using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_App;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ievade : ContentPage
    {
        public ImageSource ImageSource { get; set; }
        public Ievade()
        {
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

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            Stream stream = await DependencyService.Get<IAttelaIzvele>().GetImageStreamAsync();
            if (stream != null)
            {
                attels.Source = ImageSource.FromStream(() => stream);
            }

            (sender as Button).IsEnabled = true;
        }
    }

    
}