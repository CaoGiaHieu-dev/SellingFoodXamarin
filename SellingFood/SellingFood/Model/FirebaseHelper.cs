using Firebase.Database;
using Firebase.Database.Query;
using SellingFood.Model.Cart;
using SellingFood.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SellingFood.Model
{
    public class FirebaseHelper
    {
        public static FirebaseClient firebase = new FirebaseClient("http://foodshop-2957e.firebaseio.com/");

        public static async Task<List<CartModel>> GetAllCartModels()
        {
            try
            {
                var userlist = (await firebase.Child("CartModels").OnceAsync<CartModel>()).Select(item =>
                new CartModel
                {
                    Id = item.Object.Id,
                    Name = item.Object.Name,
                    Detail = item.Object.Detail,
                    Image = item.Object.Image,
                    Number = item.Object.Number,
                    Price = item.Object.Price,
                    Total = item.Object.Total
                }).ToList();
                return userlist;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task AddCartModel(int Id, string Name, string Detail , string Image , int Number, float Price, float Total)
        {
            try
            {
                await firebase.Child("CartModels").PostAsync(new CartModel()
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
            catch(Exception e)
            {
                Debug.WriteLine($"Error:{e}");
            }

        }

        public static async Task<CartModel> GetCartModel(int Id)
        {
            try
            {
                var allCartModels = await GetAllCartModels();
                await firebase.Child("CartModels").OnceAsync<CartModel>();
                return allCartModels.Where(a => a.Id == Id).FirstOrDefault();
            }
            catch ( Exception e)
            {
                Debug.WriteLine($"Error:{e}");
                return null;
            }
        }

        public static async Task UpdateCartModel(int Id, string Name, string Detail, string Image, int Number, float Price, float Total)
        {
            var toUpdateCartModel = (await firebase.Child("CartModels").OnceAsync<CartModel>()).Where(a => a.Object.Id == Id).FirstOrDefault();

             await firebase.Child("CartModels").Child(toUpdateCartModel.Key).PutAsync(new CartModel() 
            {
                Id = Id,
                Name = Name,
                Detail = Detail,
                Image = Image.ToString(),
                Number = Number,
                Price = Price,
                Total = Total
            });
        }

        public static async Task DeleteCartModel(int Id)
        {
            var toDeleteCartModel = (await firebase.Child("CartModels").OnceAsync<CartModel>()).Where(a => a.Object.Id == Id).FirstOrDefault();

            await firebase.Child("CartModels").Child(toDeleteCartModel.Key).DeleteAsync();

        }
    }
}
