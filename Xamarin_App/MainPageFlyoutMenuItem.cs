using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin_App
{
    public class MainPageFlyoutMenuItem
    {
        public MainPageFlyoutMenuItem()
        {
            TargetPage = typeof(MainPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetPage { get; set; }
    }
}