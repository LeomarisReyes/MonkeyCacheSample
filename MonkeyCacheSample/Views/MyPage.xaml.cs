using System;
using System.Collections.Generic;
using MonkeyCacheSample.ViewModels;
using Xamarin.Forms;

namespace MonkeyCacheSample.Views
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();
            BindingContext = new MyPageViewModel();
        }
    }
}
