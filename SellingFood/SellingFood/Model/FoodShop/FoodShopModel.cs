using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SellingFood.Model.FoodShop
{
    public class FoodShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public ImageSource Image { get; set; }
        public float Price { get; set; }
    }
}
