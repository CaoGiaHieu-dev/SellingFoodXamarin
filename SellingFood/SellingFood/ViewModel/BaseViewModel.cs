using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SellingFood.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T blackfield, T value, [CallerMemberName]string propertyName = null)
        {
            if( EqualityComparer<T>.Default.Equals(blackfield,value))
            {
                return false;
            }
            blackfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        public void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public const string ItemsPropertyName = "Items";
        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            /*Only Support in C# 6.0 in Visual Studio 2015*/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            /*ELSE Verison*/
            //if (PropertyChanged!=null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //}
        }

    }
}
