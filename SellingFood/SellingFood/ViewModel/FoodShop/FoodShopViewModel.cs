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
using SellingFood.View;
using SellingFood.Model.User;

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
                    Id          = 1 ,
                    Image       = ImageSource.FromResource(url + "ComGa.jpg") ,
                    Name        = "Com ga" ,
                    Detail ="Day khong phai com vit",
                    Number=0,
                    Price = 2000 ,
                },
                new FoodShopModel
                {
                    Id        = 2 ,
                    Image        = ImageSource.FromResource(url + "ComVit.jpg") ,
                    Name        = "Com vit" ,
                    Detail ="Day khong phai com ga",
                    Price = 4000 ,
                    Number=0,
                },
                new FoodShopModel
                {
                    Id          = 3 ,
                    Image       = ImageSource.FromResource(url + "NguFood.jpg") ,
                    Name        = "Ngu Food" ,
                    Detail      ="Mai hong ngu",
                    Price = 5000 ,
                    Number=0,
                },
                new FoodShopModel
                {
                    Id        = 4 ,
                    Image        = ImageSource.FromResource(url + "ComHaiSan.jpg") ,
                    Name        = "Com hai san" ,
                    Detail ="Com hai san",
                    Number=0,
                    Price = 23000 ,
                },
                new FoodShopModel
                {
                    Id        = 5 ,
                    Image        = ImageSource.FromResource(url + "CuaHoangDe.jpg") ,
                    Name        = "Cua hoang de" ,
                    Detail ="Cua hoang de",
                    Price = 4000 ,
                    Number=0,
                },
                new FoodShopModel
                {
                    Id        = 6 ,
                    Image        = ImageSource.FromResource(url + "WibuFood.jpg") ,
                    Name        = "Ngu Food" ,
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
        }

        /// <summary>
        ///  Load purchange list
        /// </summary>
        public async Task LoadPurchageList()
        {
            totalMoney = 0;

            PurchageList = new ObservableCollection<CartModel>(PurchangeStore);

            foreach (var total in PurchageList)
            {
                totalMoney += total.Total;
            }

            CartStore = PurchangeStore;
        }

        /// <summary>
        /// Load History
        /// </summary>
        /// <returns></returns>
        public async Task LoadHistory()
        {
            HistoryStore = new List<HistoryModel>();

            var userinfo = UserStore.Select(a => new UserModel() { UserName = a.UserName }).Single();

            HistoryStore = await Firebase.GetHistory(userinfo.UserName);

            HistoryList = new ObservableCollection<HistoryModel>(HistoryStore.OrderByDescending(a=>a.Time));
            //if (temp != null)
            //{
            //    foreach (var items in temp)
            //    {
            //        HistoryStore.Add(new HistoryModel
            //        {
            //            Datetime = items.Datetime,
            //            Time = items.Time,
            //            Total = items.Total,
            //            TotalItems = items.TotalItems
            //        });
            //    }
            //   
            //}
            OnPropertyChanged("HistoryList");
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

                //Update to cart store
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
            }
            //await Save CartModel;
            LoadCartAsync();
        }

        /// <summary>
        /// Remove from cart
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public async Task RemoveCartAsync(CartModel items)
        {
            if (CartStore.Where(x => x.Id == selectFoodList.Id).Any())
            {
                //Get Number items 
                var numbertemp = CartStore.Where(x => x.Id == selectFoodList.Id).Select(i => new CartModel() { Number = i.Number, Price = i.Price }).Single();

                CartStore.Remove(CartStore.Where(x => x.Id == selectFoodList.Id).Single());

                //Create food list temp and update
                var tempfoodlist = foodList.Select(c => { c.Number = numbertemp.Number - 1; return c; }).ToList().Where(x => x.Id == selectFoodList.Id);
                foreach (var item in foodList.Where(x => x.Id == selectFoodList.Id).ToList())
                {
                    foodList.Remove(item);
                    foodList.Insert(item.Id - 1, tempfoodlist.FirstOrDefault());
                }

                //Reload food list
                OnPropertyChanged("foodList");

                if (numbertemp.Number != 1)
                {
                    //Update to cart store
                    CartStore.Add(new CartModel
                    {
                        Id = selectFoodList.Id,
                        Name = selectFoodList.Name,
                        Detail = selectFoodList.Detail,
                        Image = selectFoodList.Image.ToString(),
                        Price = selectFoodList.Price,
                        Number = numbertemp.Number - 1,
                        Total = (numbertemp.Number - 1) * numbertemp.Price,
                    });
                }
                else
                {
                    CartStore.RemoveAt(selectFoodList.Id);
                }

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add fail", "You doesn't have : " + selectFoodList.Name + " in your bag", "OK");

                //Reload food list
                OnPropertyChanged("foodList");
            }
            //await Save CartModel;
            LoadCartAsync();
        }
        #endregion

        #region Command

        /// <summary>
        /// Add to firebase
        /// </summary>
        /// <returns></returns>
        private async Task AddtoFirebase()
        {
            // GetUser
            var userinfo = UserStore.Select(a => new UserModel() { UserName = a.UserName }).Single();

            //Add to cardItems
            foreach (var items in PurchangeStore)
            {
                await Firebase.AddCartModel(userinfo.UserName, items.Id, items.Name, items.Detail, items.Image, items.Number, items.Price, items.Total);
            }

            await Firebase.AddNewHistory(userinfo.UserName, PurchangeStore.Count(), totalMoney);

            //var temp = await Firebase.GetCartModel(Selectedid);

            //Update if allready exits items
            //if (temp != null)
            //{
            //    foreach (var items in PurchangeStore)
            //    {
            //        if (items.Id == Selectedid)
            //        {
            //            await Firebase.UpdateCartModel(userinfo.UserName, items.Id, items.Name, items.Detail, items.Image, items.Number, items.Price, items.Total);
            //        }
            //    }
            //}
            // Adding new items 
            //else
            //{
            //    foreach (var items in PurchangeStore)
            //    {
            //        if (items.Id == Selectedid)
            //        {
            //            await Firebase.AddCartModel(userinfo.UserName, items.Id, items.Name, items.Detail, items.Image, items.Number, items.Price, items.Total);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <returns></returns>
        private async Task RegistrationNewUser()
        {
            if( Password == ConfirmPassword)
            {
                await Firebase.AddNewUser(UserName, Password);
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        private async Task LoginMainPage()
        {
            if (await Firebase.GetUser(UserName, Password) == true)
            {
                Application.Current.MainPage = new MainPage();

                UserStore = new List<UserModel>();

                UserStore.Add(new UserModel
                {
                    UserName = UserName,
                    Password = Password,
                });
                LoadHistory();

            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Fail", "Password or Username Incorrect", "OK");
            }
        }

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
            AddCartAsync(cartListTemp);
            
            //await Application.Current.MainPage.DisplayAlert("Sussces", "Add sussces : " + selectFoodList.Name, "OK");
        }

        /// <summary>
        /// Remove from cart
        /// </summary>
        /// <returns></returns>
        private async Task RemoveCart()
        {
            if(selectFoodList.Number != 0 || selectFoodList.Number ==1)
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
                RemoveCartAsync(cartListTemp);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add fail", "You doesn't have : " + selectFoodList.Name + " in your bag", "OK");
            }

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


            if (CartStore != null)
            {
                PurchangeStore = CartStore;
            }
            else
            {
                CartStore = PurchangeStore;
            }

            Purchange = new Command(async () => await AddtoFirebase());

            CartStore = new List<CartModel>();

            AddtoCart = new Command(async () => await AddToCart());

            RemovefromCart = new Command(async () => await RemoveCart());

            collapseFoodList = new Command(async () => await CollapseFoodList());

            Login = new Command(async () => await LoginMainPage());

            Registration = new Command(async () => await RegistrationNewUser());
        }
    }
}
