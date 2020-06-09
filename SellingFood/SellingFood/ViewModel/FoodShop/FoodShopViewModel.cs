using SellingFood.Model.Cart;
using SellingFood.Model.FoodShop;
using SellingFood.ViewModel.FoodShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using SellingFood.ViewModel.Cart;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace SellingFood.ViewModel.FoodShop
{
    public class FoodShopViewModel : FoodShopControl
    {
        #region Display Value
        //Load FoodList
        private void LoadFood()
        {
            foodList = new ObservableCollection<FoodShopModel>
            {
                new FoodShopModel
                {
                    Id        = 1 ,
                    Image        = ImageSource.FromResource(url + "Home.png") ,
                    Name        = "Com ga" ,
                    Detail ="Day khong phai com vit",
                    Price = 2000 ,
                },
                new FoodShopModel
                {
                    Id        = 2 ,
                    Image        = ImageSource.FromResource(url + "Home.png") ,
                    Name        = "Com vit" ,
                    Detail ="Day khong phai com ga",
                    Price = 4000 ,
                },
            };
            DefaultList = foodList;
        }
        //Load CartList
        public void LoadCart()
        {
            cartList = new ObservableCollection<CartModel>(CartStore);
        }

        #endregion

        #region Property

        //Add to cart store
        public void AddCart(CartModel items)
        {
            //do something
            CartStore.Add(items);
            LoadCart();
        }
        #endregion

        #region Command

        // Add to cart
        private async Task AddToCart()
        {
            var cartListTemp = new CartModel()
            {
                Id = selectFoodList.Id,
                Name = selectFoodList.Name,
                Detail = selectFoodList.Detail,
                Image = selectFoodList.Image,
                Price = selectFoodList.Price,
                Number = 1,
            };
            AddCart(cartListTemp);
            
            await Application.Current.MainPage.DisplayAlert("Sussces", "Add sussces : " + selectFoodList.Name, "OK");
        }
        #endregion

        public FoodShopViewModel()
        {
            this.foodList = new ObservableCollection<FoodShopModel>();

            LoadFood();
            CartStore = new List<CartModel>();
            AddtoCart = new Command(async () => await AddToCart());
        }
    }
}
