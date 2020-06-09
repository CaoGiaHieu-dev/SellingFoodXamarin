using SellingFood.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SellingFood.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodShop : ContentPage
    {
        public FoodShop()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}