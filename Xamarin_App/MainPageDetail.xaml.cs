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
        public ObservableCollection<Ieraksts> Ieraksti { 
            get { return _ieraksti; }
            private set { 
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
                ItemTemplate = new DataTemplate(() => {
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

                    stack.Children.Add(label1);
                    stack.Children.Add(label2);
                    cell.View = stack;
                    cell.ContextActions.Add(menuItem1);
                    return cell;
                })
            };





            for (int i = 1; i <= 50; i++) {
                Ieraksti.Add( new Ieraksts()
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

            int id = Int16.Parse(elem.AutomationId)-1;

            if (Ieraksti[id].Virs == "Virsaksts nr. " + (id+1))
            Ieraksti[id] = new Ieraksts() { Numurs = id+1, 
                Teksts = Ieraksti[id].Teksts,
                Virs = "Mainīts virsraksts nr." + (id + 1)
            };
            else
            {
                Ieraksti[id] = new Ieraksts()
                {
                    Numurs = id + 1,
                    Teksts = Ieraksti[id].Teksts,
                    Virs = "Virsaksts nr." + (id + 1)
                };
            }
        }
    }
    public class Ieraksts
    {   
        public int Numurs { get; set; }
        public string Virs { get; set; }
        public string Teksts { get; set; }
    }

}