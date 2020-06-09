using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SellingFood.Helper
{
    public class CustomCarouselPage : CarouselPage
    {
        public static readonly BindableProperty IsSwippingProperty = BindableProperty.Create(nameof(IsSwipping), typeof(bool), typeof(CustomCarouselPage), default(bool));
        public bool IsSwipping
        {
            get { return (bool)GetValue(IsSwippingProperty); }
            set { SetValue(IsSwippingProperty, value); }
        }
    }
}
