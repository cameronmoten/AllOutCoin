using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AllOutCoin.Models;

namespace AllOutCoin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChoicePage : ContentPage
    {
        public APILogin apilogin { get; set; }

        public AddChoicePage()
        {
            InitializeComponent();
            BindingContext = this;

            addApi.Clicked += async  delegate {
                await Navigation.PushAsync(new NewApiSelectPage());
            };
        }
        
      
    }
}