using SellingFood.Model.Cart;
using SellingFood.Model.FoodShop;
using SellingFood.Model.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SellingFood.ViewModel.FoodShop
{
    public class FoodShopControl : BaseViewModel
    {
        #region Property
        public string url = "SellingFood.Images.";

        public static int Selectedid;
        //Nativigation
        public INavigation Navigation { get; set; }

        //Search Command Execute
        private void SearchCommandExecute()
        {
            foodList = DefaultList;
            if (foodList != null && foodList.Count > 0)
            {
                var tempRecords = foodList.Where(x => x.Name.ToLower().Contains(SearchText.ToLower()));
                ObservableCollection<FoodShopModel> searchList = new ObservableCollection<FoodShopModel>();
                foreach (FoodShopModel item in tempRecords)
                {
                    searchList.Add(item);
                }

                foodList = searchList;

            }
        }
        #endregion

        #region DataStore
        //Cart Store
        public static List<CartModel> CartStore { get; set; }

        //History Store
        public static List<HistoryModel> HistoryStore { get; set; }

        //Purchange Store
        public static List<CartModel> PurchangeStore { get; set; }

        //User Store
        public static List<UserModel> UserStore { get; set; }

        //Firebase
        public Model.FirebaseHelper Firebase = new Model.FirebaseHelper();

        #endregion

        #region Model
        // List Cart
        private ObservableCollection<CartModel> _cartList { get; set; }
        public ObservableCollection<CartModel> cartList
        {
            get
            {
                return _cartList;
            }
            set
            {
                if (_cartList != value)
                {
                    _cartList = value;
                    OnPropertyChanged();
                }
            }
        }

        // List Purchange
        private ObservableCollection<CartModel> _PurchageList { get; set; }
        public ObservableCollection<CartModel> PurchageList
        {
            get
            {
                return _PurchageList;
            }
            set
            {
                if (_PurchageList != value)
                {
                    _PurchageList = value;
                    OnPropertyChanged();
                }
            }
        }

        //History List
        private ObservableCollection<HistoryModel> _HistoryList { get; set; }
        public ObservableCollection<HistoryModel> HistoryList
        {
            get
            {
                return _HistoryList;
            }
            set
            {
                if (_HistoryList != value)
                {
                    _HistoryList = value;
                    OnPropertyChanged();
                }
            }
        }

        // Default List
        public ObservableCollection<FoodShopModel> DefaultList = new ObservableCollection<FoodShopModel>();

        //FoodList
        private ObservableCollection<FoodShopModel> _foodList;
        public ObservableCollection<FoodShopModel> foodList
        {
            get
            {
                return _foodList;
            }
            set
            {
                if(_foodList !=value)
                {
                    _foodList = value;
                }
                OnPropertyChanged("foodList");
            }
        }

        #endregion

        #region Command
        // InputSreach
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
                SearchCommandExecute();
            }
        }

        //Purchange
        public ICommand Purchange { get; set; }

        // Add to cart
        public ICommand AddtoCart { get; set; }

        // Remove from Cart
        public ICommand RemovefromCart { get; set; }

        // Login 
        public ICommand Login { get; set; }

        // Registration
        public ICommand Registration { get; set; }

        //Collapse food list
        public ICommand collapseFoodList { get; set; }

        //Select FoodList
        private FoodShopModel _selectFoodList { get; set; }
        public FoodShopModel selectFoodList
        {
            get
            {
                return _selectFoodList;
            }
            set
            {
                if(_selectFoodList != value)
                {
                    _selectFoodList = value;
                    Selectedid = _selectFoodList.Id;
                }
            }
        }
        #endregion

        #region Field
        //Number items
        private Boolean _CollapseFood;
        public Boolean CollapseFood { get => _CollapseFood; set { _CollapseFood = value; OnPropertyChanged(); } }

        //Total cart list
        private float _totalMoney;
        public float totalMoney { get => _totalMoney; set { _totalMoney = value; OnPropertyChanged(); } }

        //UserName
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }

        //Password
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        //ConfirmPassword
        private string _ConfirmPassword;
        public string ConfirmPassword { get => _ConfirmPassword; set { _ConfirmPassword = value; OnPropertyChanged(); } }
        

        #endregion

    }
}
