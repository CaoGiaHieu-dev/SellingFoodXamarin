using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SellingFood.Model.Cart;
using SellingFood.Model.User;
using SellingFood.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SellingFood.Model
{
    public class FirebaseHelper
    {
        #region Config firebase
        public FirebaseClient firebase = new FirebaseClient("https://foodshop-2957e.firebaseio.com/");

        static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "nJxjDRT3eED8mtlXrgj56wf8YXKo6Thetphs7Sg5",

            BasePath = "https://foodshop-2957e.firebaseio.com/"
        };

        IFirebaseClient client = new FireSharp.FirebaseClient(config);
        #endregion

        #region field
        public DateTime Time()
        {
            DateTime today = DateTime.Now;

            return today;
        }
        #endregion

        #region CartModels

        /// <summary>
        /// Get all Items
        /// </summary>
        /// <returns></returns>
        public async Task<List<CartModel>> GetAllCartModels()
        {
            //var child = FirebaseKeyGenerator.Next();

            try
            {
                List<CartModel> ListCart = new List<CartModel>();

                List<string> listValue = new List<string>();

                FirebaseResponse response = await client.GetAsync("CartModels/");

                //Convert json
                dynamic mList = JsonConvert.DeserializeObject<dynamic>(response.Body);

                foreach (var itemDynamic in mList)
                {
                    if (itemDynamic != null)
                    {
                        foreach (var items in itemDynamic)
                        {
                            var value = items.Value;
                            listValue.Add(value.ToString());
                        }
                        var list = new CartModel()
                        {
                            Id = int.Parse(listValue[1]),
                            Detail = listValue[0],
                            Image = listValue[2],
                            Name = listValue[3],
                            Number = int.Parse(listValue[4]),
                            Price = float.Parse(listValue[5]),
                            Total = float.Parse(listValue[6]),
                        };
                        ListCart.Add(list);
                    }
                }

                //listtemp.Add(list);

                //FirebaseResponse response = await client.GetAsync("CartModels/1");
                //CartModel todo = response.ResultAs<JObject>().ToObject<CartModel>();

                //var userlist = (await firebase.Child("CartModels/1").OnceAsync<CartModel>()).Select(item =>
                //new CartModel
                //{
                //    Id = item.Object.Id,
                //    Name = item.Object.Name,
                //    Detail = item.Object.Detail,
                //    //Image = item.Object.Image,
                //    Number = item.Object.Number,
                //    Price = item.Object.Price,
                //    Total = item.Object.Total
                //}).ToList();

                return ListCart;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        /// <summary>
        /// Add to firebase
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Detail"></param>
        /// <param name="Image"></param>
        /// <param name="Number"></param>
        /// <param name="Price"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public async Task AddCartModel(string username, int Id, string Name, string Detail, ImageSource Image, int Number, float Price, float Total)
        {

            try
            {
                await client.SetAsync("CartModels/" + username + "/" + Time().ToString("dd-MM-yyyy/hh-mm") + "/" + Id, new CartModel()
                {
                    Id = Id,
                    Name = Name,
                    Detail = Detail,
                    Image = Image,
                    Number = Number,
                    Price = Price,
                    Total = Total
                });


                //await firebase.Child("CartModels").PostAsync(new CartModel()
                //{
                //    Id = Id,
                //    Name = Name,
                //    Detail = Detail,
                //    Image = Image,
                //    Number = Number,
                //    Price = Price,
                //    Total = Total
                //});
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }

        }

        /// <summary>
        /// Get Value by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<CartModel> GetCartModel(int Id)
        {
            try
            {

                var allCartModels = await GetAllCartModels();

                //await firebase.Child("CartModels").Child(FirebaseKeyGenerator.Next()).OnceAsync<CartModel>();

                return allCartModels.Where(a => a.Id == Id).FirstOrDefault();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        /// <summary>
        /// Update value
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Detail"></param>
        /// <param name="Image"></param>
        /// <param name="Number"></param>
        /// <param name="Price"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public async Task UpdateCartModel(string username, int Id, string Name, string Detail, ImageSource Image, int Number, float Price, float Total)
        {
            //var toUpdateCartModel = (await firebase.Child("CartModels").OnceAsync<CartModel>()).Where(a => a.Object.Id == Id).FirstOrDefault();

            //await firebase.Child("CartModels").Child(toUpdateCartModel.Key).PutAsync(new CartModel()
            //{
            //    Id = Id,
            //    Name = Name,
            //    Detail = Detail,
            //    Image = Image,
            //    Number = Number,
            //    Price = Price,
            //    Total = Total
            //});

            try
            {
                await client.SetAsync("CartModels/" + username + "/" + Time().ToString("dd-MM-yyyy/hh-mm") + "/" + Id, new CartModel()
                {
                    Id = Id,
                    Name = Name,
                    Detail = Detail,
                    Image = Image,
                    Number = Number,
                    Price = Price,
                    Total = Total
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }
        }

        /// <summary>
        /// DeleteValue
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteCartModel(int Id)
        {
            var toDeleteCartModel = (await firebase.Child("CartModels").OnceAsync<CartModel>()).Where(a => a.Object.Id == Id).FirstOrDefault();

            await firebase.Child("CartModels").Child(toDeleteCartModel.Key).DeleteAsync();

        }

        #endregion

        #region UserModels

        /// <summary>
        /// Get all User
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserModel>> GetallUser(string username)
        {
            try
            {
                List<UserModel> ListCart = new List<UserModel>();

                List<string> listValue = new List<string>();

                FirebaseResponse response = await client.GetAsync("UserModels/" + username);

                //Convert json
                dynamic mList = JsonConvert.DeserializeObject<dynamic>(response.Body);

                foreach (var itemDynamic in mList)
                {
                    if (itemDynamic != null)
                    {
                        foreach (var items in itemDynamic)
                        {
                            var value = items.Value;
                            listValue.Add(value.ToString());
                        }
                    }
                }
                var list = new UserModel()
                {
                    UserName = listValue[1],
                    Password = listValue[0]
                };
                ListCart.Add(list);
                return ListCart;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> GetUser(string username, string password)
        {
            try
            {

                var allUserModels = await GetallUser(username);

                if (allUserModels.Where(a => a.UserName == username && a.Password == password).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //await firebase.Child("CartModels").Child(FirebaseKeyGenerator.Next()).OnceAsync<CartModel>();


            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task AddNewUser(string username, string password)
        {
            try
            {
                await client.SetAsync("UserModels/" + username, new UserModel()
                {
                    UserName = username,
                    Password = password
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }
        }

        #endregion

        #region HistoryModels

        /// <summary>
        /// Get all History
        /// </summary>
        /// <returns></returns>
        public async Task<List<HistoryModel>> GetHistory(string username)
        {
            try
            {
                List<HistoryModel> ListHistory = new List<HistoryModel>();

                FirebaseResponse response = await client.GetAsync("HistoryModels/" + username +"/");

                //Convert json
                dynamic mList = JsonConvert.DeserializeObject<dynamic>(response.Body);

                foreach (var itemDynamic in mList)
                {
                    if (itemDynamic != null)
                    {
                        foreach (var itemsbyday in itemDynamic)
                        {
                            foreach (var items in itemsbyday)
                            {
                                foreach (var item in items)
                                {
                                    List<string> listValue = new List<string>();

                                    foreach (var info in item)
                                    {
                                        var value = info.Value;
                                        listValue.Add(value.ToString());
                                    }
                                    var list = new HistoryModel()
                                    {
                                        Datetime = itemDynamic.Name,
                                        Time = items.Name,
                                        Total = float.Parse(listValue[0]),
                                        TotalItems = int.Parse(listValue[1])
                                    };

                                    ListHistory.Add(list);
                                }
                            }
                        }
                        
                    }
                }
                
                return ListHistory;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        /// <summary>
        /// Get History of User
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<bool> GetHistoryofUser(string username)
        {
            try
            {

                var allUserModels = await GetHistory(username);

                allUserModels.OrderByDescending(a => a.Datetime);

                if (allUserModels.Where(a => a.Datetime == Time().ToString("dd-MM-yyyy")).FirstOrDefault() != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //await firebase.Child("CartModels").Child(FirebaseKeyGenerator.Next()).OnceAsync<CartModel>();


            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return false;
            }
        }

        /// <summary>
        /// Add New History
        /// </summary>
        /// <param name="username"></param>
        /// <param name="itensNumber"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public async Task AddNewHistory(string username, int itensNumber , float Total)
        {
            try
            {
                await client.SetAsync("HistoryModels/" + username +"/"+Time().ToString("dd-MM-yyyy/hh-mm"), new HistoryModel()
                {
                    TotalItems = itensNumber,
                    Total = Total
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }
        }

        #endregion
    }
}
