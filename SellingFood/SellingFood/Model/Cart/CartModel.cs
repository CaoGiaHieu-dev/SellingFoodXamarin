using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using Realms;

namespace SellingFood.Model.Cart
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CartModel 
    {
        [JsonProperty("Detail")]
        public string Detail { get; set; }

        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Image")]
        public ImageSource Image { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Price")]
        public float Price { get; set; }
        
        [JsonProperty("Total")]
        public float Total { get; set; }

    }

}
