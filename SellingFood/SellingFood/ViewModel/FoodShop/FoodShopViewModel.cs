using SellingFood.Model.Cart;
using SellingFood.Model.FoodShop;
using SellingFood.ViewModel.FoodShop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Linq;
using SQLite;
using SellingFood.Model;

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
        public async Task LoadCartAsync()
        {
            totalMoney = 0;
            cartList = new ObservableCollection<CartModel>(CartStore);
            foreach(var total in cartList)
            {
                totalMoney += total.Total;
            }

            var temp = await Firebase.GetCartModel(selectFoodList.Id);

            //Update if allready exits items
            if (temp != null)
            {
                foreach (var items in CartStore)
                {
                    if (items.Id == selectFoodList.Id)
                    {
                        await Firebase.UpdateCartModel(items.Id, items.Name, items.Detail, items.Image, items.Number, items.Price, items.Total);
                    }
                }
            }
            // Adding new items 
            else
            {
                foreach (var items in CartStore)
                {
                    if (items.Id == selectFoodList.Id)
                    {
                        await Firebase.AddCartModel(items.Id, items.Name, items.Detail, items.Image, items.Number, items.Price, items.Total);
                    }
                }

            }
        }
        #endregion

        #region Property

        /// <summary>
        /// Add to cart store
        /// </summary>
        /// <param name="items"></param>
        public async Task AddCartAsync(CartModel items)
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
                    Image = selectFoodList.Image.ToString(),
                    Price = selectFoodList.Price,
                    Number = numbertemp.Number + 1,
                    Total = (numbertemp.Number + 1)*numbertemp.Price,
                });

                //Create food list temp and update
                var tempfoodlist = foodList.Select(c => { c.Number = numbertemp.Number + 1; return c; }).ToList().Where(x => x.Id ==selectFoodList.Id);
                foreach (var item in foodList.Where(x => x.Id == selectFoodList.Id ).ToList())
                {
                    foodList.Remove(item);
                    foodList.Insert(item.Id-1, tempfoodlist.FirstOrDefault());
                }

                //await FirebaseHelper.UpdateCartModel(selectFoodList.Id, selectFoodList.Name, selectFoodList.Detail, selectFoodList.Image.ToString(), numbertemp.Number + 1, selectFoodList.Price, (numbertemp.Number + 1) * numbertemp.Price);

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
                    Image = selectFoodList.Image.ToString(),
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

                //await FirebaseHelper.AddCartModel( selectFoodList.Id, selectFoodList.Name, selectFoodList.Detail, selectFoodList.Image.ToString(), numbertemp.Number + 1, selectFoodList.Price, (numbertemp.Number + 1) * numbertemp.Price);
            }
            //await Firebase.AddCartModel(1, "1", "1", ("1"), 1, 1, 1);

            //await SaveCartModel();

            LoadCartAsync();
        }

        /// <summary>
        /// Add to realm
        /// </summary>
        //public async Task SaveCartModel()
        //{
        //    var numbertemp = CartStore.Where(x => x.Id == selectFoodList.Id).Select(i => new CartModel() { Number = i.Number, Price = i.Price }).Single();
        //    RealmDB.Write(() =>
        //    {
        //        new CartModel()
        //        {
        //            Id = selectFoodList.Id,
        //            Name = selectFoodList.Name,
        //            Detail = selectFoodList.Detail,
        //            Image = selectFoodList.Image.ToString(),
        //            Price = selectFoodList.Price,
        //            Number = numbertemp.Number + 1,
        //            Total = (numbertemp.Number + 1) * numbertemp.Price,
        //        };
        //    });
        //}

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
                Image = selectFoodList.Image.ToString(),
                Price = selectFoodList.Price,
                Number = 1,
            };
            AddCartAsync(cartListTemp);
            
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
            CollapseFood = true;

            this.foodList = new ObservableCollection<FoodShopModel>();

            LoadFood();

            CartStore = new List<CartModel>();
            AddtoCart = new Command(async () => await AddToCart());
            collapseFoodList = new Command(async () => await CollapseFoodList());
        }
    }
}
