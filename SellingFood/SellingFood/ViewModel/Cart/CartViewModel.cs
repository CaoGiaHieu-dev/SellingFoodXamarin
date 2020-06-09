using SellingFood.Model.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SellingFood.ViewModel.Cart
{
    public class CartViewModel : CartController
    {
        #region Display Value
        public void LoadCart()
        {
            cartList = new ObservableCollection<CartModel>();
        }
        #endregion
        public CartViewModel()
        {
            LoadCart();
        }
    }
}
