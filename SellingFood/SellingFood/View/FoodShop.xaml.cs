using SellingFood.Helper;
using SellingFood.Model.FoodShop;
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

        private SellingFood.ViewModel.FoodShop.FoodShopViewModel ViewModel
        {
            get { return BindingContext as SellingFood.ViewModel.FoodShop.FoodShopViewModel; }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                //ViewModel.LoadFood();
            }
            catch
            {

            }
        }
    }
}