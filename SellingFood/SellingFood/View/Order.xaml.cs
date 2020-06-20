using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SellingFood.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Order : ContentPage
    {
        public Order()
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
                ViewModel.LoadCartAsync();
            }
            catch
            {

            }

        }

        
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new Purchange());
        }
    }
}