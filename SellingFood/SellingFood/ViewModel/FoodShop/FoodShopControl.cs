﻿using SellingFood.Model.Cart;
using SellingFood.Model.FoodShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SellingFood.ViewModel.FoodShop
{
    public class FoodShopControl : BaseViewModel
    {
        #region Property
        public string url = "SellingFood.Images.";

        //Search Command Execute
        private void SearchCommandExecute()
        {
            foodList = DefaultList;
            if (foodList != null && foodList.Count > 0)
            {
                var tempRecords = foodList.Where(x => x.Name.ToLower().Contains(SearchText.ToLower()));
                ObservableCollection<FoodShopModel> searchList = new ObservableCollection<FoodShopModel>();
                foreach (FoodShopModel item in tempRecords)
                {
                    searchList.Add(item);
                }

                foodList = searchList;

            }
        }
        #endregion

        #region DataStore
        public static List<CartModel> CartStore { get; set; }
        #endregion

        #region Model
        // List Cart
        private ObservableCollection<CartModel> _cartList { get; set; }
        public ObservableCollection<CartModel> cartList
        {
            get
            {
                return _cartList;
            }
            set
            {
                if (_cartList != value)
                {
                    _cartList = value;
                    OnPropertyChanged();
                }
            }
        }

        // Default List
        public ObservableCollection<FoodShopModel> DefaultList = new ObservableCollection<FoodShopModel>();

        //FoodList
        private ObservableCollection<FoodShopModel> _foodList;
        public ObservableCollection<FoodShopModel> foodList
        {
            get
            {
                return _foodList;
            }
            set
            {
                if(_foodList !=value)
                {
                    _foodList = value;
                }
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        // InputSreach
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                SearchCommandExecute();
            }
        }

        // Add to cart
        public ICommand AddtoCart { get; set; }

        //Select FoodList
        private FoodShopModel _selectFoodList { get; set; }
        public FoodShopModel selectFoodList
        {
            get
            {
                return _selectFoodList;
            }
            set
            {
                if(_selectFoodList != value)
                {
                    _selectFoodList = value;
                }
            }
        }
        #endregion


    }
}