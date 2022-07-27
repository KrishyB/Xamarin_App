using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : FlyoutPage
    {
        public MainPage()
        {
            InitializeComponent();
            izvelne.listview.ItemSelected += Izvelets;
        }

        private void Izvelets(object sender, SelectedItemChangedEventArgs e)
        {
            MainPageFlyoutMenuItem item = e.SelectedItem as MainPageFlyoutMenuItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetPage));
                izvelne.listview.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}