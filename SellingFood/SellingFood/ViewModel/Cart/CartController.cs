using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using SellingFood.Model.Cart;

namespace SellingFood.ViewModel.Cart
{
    public class CartController : BaseViewModel
    {
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
                if(_cartList != value)
                {
                    _cartList = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
