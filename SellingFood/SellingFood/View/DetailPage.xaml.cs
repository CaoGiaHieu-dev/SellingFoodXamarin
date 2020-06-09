using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace SellingFood.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : Xamarin.Forms.TabbedPage
    {
        public DetailPage()
        {
            InitializeComponent();

            // Make tabbed at bottom
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            this.On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            //adding page to tabbed
        }
       
    }
}