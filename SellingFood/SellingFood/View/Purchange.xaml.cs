﻿using Rg.Plugins.Popup.Pages;
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
    public partial class Purchange : PopupPage
    {
        public Purchange()
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
                ViewModel.LoadPurchageList();
            }
            catch
            {

            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.LoadPurchageList();
        }
    }
}