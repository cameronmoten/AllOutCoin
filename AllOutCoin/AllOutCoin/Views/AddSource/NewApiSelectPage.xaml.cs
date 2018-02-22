using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AllOutCoin.Models;
using System.Collections.ObjectModel;

namespace AllOutCoin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewApiSelectPage : ContentPage
    {

        public APILogin apilogin { get; set; }
        public List<ExchangeEntity> exchanges { get; set; }

        public NewApiSelectPage()
        {
            exchanges = new List<ExchangeEntity>();
            foreach (var x in StaticData.supportedExchanges)
            {
                exchanges.Add(x.Value);
            }

            InitializeComponent();

            apilogin = new APILogin
            {
                exchange = Exchanges.Wallet,
                API_KEY = "",
                API_KEY_SECRET = "",
            };
            
            BindingContext = this;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ExchangeEntity;
            if (item == null)
                return;


            await Navigation.PushAsync(new NewApiDataEntry(item));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddApi", apilogin);
            await Navigation.PopModalAsync();
        }
    }
}