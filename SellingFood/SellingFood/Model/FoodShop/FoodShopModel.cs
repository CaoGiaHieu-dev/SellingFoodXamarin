using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SellingFood.Model.FoodShop
{
    [Table("FoodList")]
    public class FoodShopModel
    {
        // ID
        private int _Id;
        [PrimaryKey]
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                this._Id = value;
            }
        }
        //Name
        private string _Name;
        [NotNull]
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                this._Name = value; 
            }
        }
        // Detai
        private string _Detail;
        public string Detail
        {
            get
            {
                return _Detail;
            }
            set
            {
                this._Detail = value;
            }
        }
        // Image
        private ImageSource _Image;
        public ImageSource Image
        {
            get
            {
                return _Image;
            }
            set
            {
                this._Image = value;
            }
        }
        //Price
        private float _Price;
        [NotNull]
        public float Price
        {
            get
            {
                return _Price;
            }
            set
            {
                this._Price = value;
            }
        }
        // Number
        private int _Number;
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                this._Number = value;
            }
        }
        
    }
}
