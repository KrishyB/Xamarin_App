using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]



    public partial class MainPageDetail : ContentPage
    {

        private ObservableCollection<Ieraksts> _ieraksti;
        public ObservableCollection<Ieraksts> Ieraksti
        {
            get { return _ieraksti; }
            private set
            {
                _ieraksti = value;
                OnPropertyChanged("Ieraksti");
            }
        }

        public MainPageDetail()
        {
            Ieraksti = new ObservableCollection<Ieraksts>();
            this.BindingContext = this;

            Content = new ListView()
            {
                ItemsSource = Ieraksti,
                ItemTemplate = new DataTemplate(() =>
                {
                    ViewCell cell = new ViewCell();
                    StackLayout stack = new StackLayout();
                    Label label1 = new Label();
                    Label label2 = new Label();
                    MenuItem menuItem1 = new MenuItem();

                    label1.SetBinding(Label.TextProperty, "Virs");
                    label1.FontAttributes = FontAttributes.Bold;
                    label2.SetBinding(Label.TextProperty, "Teksts");
                    stack.Padding = new Thickness(10);
                    menuItem1.Clicked += ChangeTitle;
                    menuItem1.BindingContext = cell;
                    menuItem1.Text = "Mainīt";
                    cell.SetBinding(ViewCell.AutomationIdProperty, "Numurs");
                    stack.VerticalOptions = LayoutOptions.FillAndExpand;

                    stack.Children.Add(label1);
                    stack.Children.Add(label2);

                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        cell.View = stack;
                        cell.ContextActions.Add(menuItem1);
                        return cell;
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        Grid grid = new Grid
                        {
                            RowDefinitions =
                            {
                                new RowDefinition { Height = new GridLength(2, GridUnitType.Auto) },
                            },
                            ColumnDefinitions =
                            {
                                new ColumnDefinition { Width = new GridLength(4, GridUnitType.Star) },
                                new ColumnDefinition { Width = new GridLength(2, GridUnitType.Auto) },
                            }
                        };

                        ImageButton imagebutton = new Xamarin.Forms.ImageButton
                        {
                            Source = "three_dots_icon.png",
                            Padding = new Thickness(10),
                            BackgroundColor = Color.Transparent,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            BindingContext = cell
                        };
                        imagebutton.Clicked += async (s, arg) =>
                        {
                            var action = await DisplayActionSheet("", "Atcelt", null, "Mainīt");
                            if (action == "Mainīt")
                            {
                                ChangeTitle(menuItem1, null);
                            }
                        };
                        grid.VerticalOptions = LayoutOptions.FillAndExpand;
                        grid.HorizontalOptions = LayoutOptions.FillAndExpand;
                        grid.Children.Add(stack, 0, 0);
                        grid.Children.Add(imagebutton, 1, 0);


                        cell.View = grid;
                        return cell;
                    }

                    return cell;
                }),
                RowHeight = 60,
                VerticalOptions = LayoutOptions.FillAndExpand,

            };




            for (int i = 1; i <= 500; i++)
            {
                Ieraksti.Add(new Ieraksts()
                {
                    Numurs = i,
                    Teksts = "Saturs nr. " + i,
                    Virs = "Virsaksts nr. " + i
                });
            }
            InitializeComponent();
        }

        private void ChangeTitle(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;

            var elem = menu.BindingContext as ViewCell;

            int id = Int16.Parse(elem.AutomationId) - 1;

            Ieraksts jauns = new Ieraksts()
            {
                Numurs = id + 1,
                Teksts = Ieraksti[id].Teksts,
                Virs = "",
            };

            if (Ieraksti[id].Virs == "Virsaksts nr. " + (id + 1))
            {
                jauns.Virs = "Mainīts virsraksts nr." + (id + 1);
            }
            else
            {
                jauns.Virs = "Virsaksts nr. " + (id + 1);
            }

            Ieraksti[id] = jauns;
        }

    }
    public class Ieraksts
    {
        public int Numurs { get; set; }
        public string Virs { get; set; }
        public string Teksts { get; set; }
    }

}