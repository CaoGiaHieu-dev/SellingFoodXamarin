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
using System.Linq;

namespace SellingFood.ViewModel.FoodShop
{
    public class FoodShopViewModel : FoodShopControl
    {
        #region Display Value

        /// <summary>
        /// Load FoodList
        /// </summary>
        public void LoadFood()
        {
            foodList = new ObservableCollection<FoodShopModel>
            {
                new FoodShopModel
                {
                    Id        = 1 ,
                    Image        = ImageSource.FromResource(url + "Home.png") ,
                    Name        = "Com ga" ,
                    Detail ="Day khong phai com vit",
                    Number=0,
                    Price = 2000 ,
                },
                new FoodShopModel
                {
                    Id        = 2 ,
                    Image        = ImageSource.FromResource(url + "Home.png") ,
                    Name        = "Com vit" ,
                    Detail ="Day khong phai com ga",
                    Price = 4000 ,
                    Number=0,
                },
                new FoodShopModel
                {
                    Id        = 3 ,
                    Image        = ImageSource.FromResource(url + "Home.png") ,
                    Name        = "Wibu Food" ,
                    Detail ="Do an khong danh cho con nguoi",
                    Price = 1000 ,
                    Number=0,
                },
            };
            DefaultList = foodList;
        }
        /// <summary>
        /// Load CartList
        /// </summary>
        public void LoadCart()
        {
            totalMoney = 0;
            cartList = new ObservableCollection<CartModel>(CartStore);
            foreach(var total in cartList)
            {
                totalMoney += total.Total;
            }
        }
        #endregion

        #region Property

        /// <summary>
        /// Add to cart store
        /// </summary>
        /// <param name="items"></param>
        public void AddCart(CartModel items)
        {
            if (CartStore.Where(x => x.Id == selectFoodList.Id).Any() )
            {
                //Get Number items 
                var numbertemp = CartStore.Where(x => x.Id == selectFoodList.Id).Select(i => new CartModel() { Number = i.Number ,Price = i.Price }).Single();

                CartStore.Remove(CartStore.Where(x => x.Id == selectFoodList.Id).Single());
                
                //Add to cart store
                CartStore.Add(new CartModel 
                {
                    Id = selectFoodList.Id,
                    Name = selectFoodList.Name,
                    Detail = selectFoodList.Detail,
                    Image = selectFoodList.Image,
                    Price = selectFoodList.Price,
                    Number = numbertemp.Number + 1,
                    Total = (numbertemp.Number + 1)*numbertemp.Price,
                });
                //UpdateCart();
                
                //Create food list temp and update
                var tempfoodlist = foodList.Select(c => { c.Number = numbertemp.Number + 1; return c; }).ToList().Where(x => x.Id ==selectFoodList.Id);
                foreach (var item in foodList.Where(x => x.Id == selectFoodList.Id ).ToList())
                {
                    foodList.Remove(item);
                    foodList.Insert(item.Id-1, tempfoodlist.FirstOrDefault());
                }

                //Reload food list
                OnPropertyChanged("foodList");

            }
            else
            {
                CartStore.Add(items);

                //Get Number items 
                var numbertemp = CartStore.Where(x => x.Id == selectFoodList.Id).Select(i => new CartModel() { Number = i.Number, Price = i.Price }).Single();

                //Remove old items
                CartStore.Remove(CartStore.Where(x => x.Id == selectFoodList.Id).Single());

                //Add to cart store
                CartStore.Add(new CartModel
                {
                    Id = selectFoodList.Id,
                    Name = selectFoodList.Name,
                    Detail = selectFoodList.Detail,
                    Image = selectFoodList.Image,
                    Price = selectFoodList.Price,
                    Number = numbertemp.Number,
                    Total = (numbertemp.Number) * numbertemp.Price,
                });

                //Create food list temp and update
                var tempfoodlist = foodList.Select(c => { c.Number = numbertemp.Number ; return c; }).ToList().Where(x => x.Id == selectFoodList.Id);
                foreach (var item in foodList.Where(x => x.Id == selectFoodList.Id).ToList())
                {
                    foodList.Remove(item);
                    foodList.Insert(item.Id - 1, tempfoodlist.FirstOrDefault());
                }

                //Reload food list
                OnPropertyChanged("foodList");

                //foodList.Select(c => { c.Number = numbertemp.Number + 1; return c; }).ToList();
            }
            LoadCart();
        }

        ///<summary>
        ///
        ///</summary>

        #endregion

        #region Command

        /// <summary>
        ///  Add to cart
        /// </summary>
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
            
            //await Application.Current.MainPage.DisplayAlert("Sussces", "Add sussces : " + selectFoodList.Name, "OK");
        }

        /// <summary>
        /// Collapse food list
        /// </summary>
        private async Task CollapseFoodList()
        {
            if (CollapseFood == false)
                CollapseFood = true;
            else
                CollapseFood = false;
            OnPropertyChanged();
        }
        #endregion

        public FoodShopViewModel()
        {
            CollapseFood = false;

            this.foodList = new ObservableCollection<FoodShopModel>();
            LoadFood();

            

            CartStore = new List<CartModel>();
            AddtoCart = new Command(async () => await AddToCart());
            collapseFoodList = new Command(async () => await CollapseFoodList());
        }
    }
}
