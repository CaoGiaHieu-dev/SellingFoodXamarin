using Firebase.Database;
using Firebase.Database.Query;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SellingFood.Model.Cart;
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
        public FirebaseClient firebase = new FirebaseClient ("https://foodshop-2957e.firebaseio.com/");

        static IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "nJxjDRT3eED8mtlXrgj56wf8YXKo6Thetphs7Sg5",

            BasePath = "https://foodshop-2957e.firebaseio.com/"
        };

        IFirebaseClient client = new FireSharp.FirebaseClient(config);
        #endregion

        public FirebaseHelper()
        {
            
        }

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

                FirebaseResponse response = await client.GetAsync("CartModels//");

                //Convert json
                dynamic mList = JsonConvert.DeserializeObject<dynamic>(response.Body);

                foreach (var itemDynamic in mList)
                {
                    if(itemDynamic != null)
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
        public async Task AddCartModel(int Id, string Name, string Detail, ImageSource Image, int Number, float Price, float Total)
        {
            
            try
            {
                await client.SetAsync("CartModels/" + Id, new CartModel()
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
        public async Task UpdateCartModel(int Id, string Name, string Detail, ImageSource Image, int Number, float Price, float Total)
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
                await client.SetAsync("CartModels/" + Id, new CartModel()
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
    }
}
