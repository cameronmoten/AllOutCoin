using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AllOutCoin.Models;
using System.Collections.ObjectModel;
using AllOutCoin.Services;
using System.Linq;
namespace AllOutCoin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewApiDataEntry : ContentPage
    {

        public APILogin apilogin { get; set; }
        public ExchangeEntity exchange { get; set; }

        public bool _isSaveVisible = false;
        public bool IsSaveVisible
        {
            get
            {
                return _isSaveVisible;
            }
            set
            {
                _isSaveVisible = value;
                //RaisePropertyChanged("IsSaveVisible");
            }
        }

        public NewApiDataEntry(ExchangeEntity exchange)
        {
            this.exchange = exchange;

            InitializeComponent();

            apilogin = new APILogin
            {
                exchange = this.exchange.exchange,
                API_KEY = "",
                API_KEY_SECRET = "",
                API_KEY_PASSWORD=""
            };

            this.help.Clicked += delegate
            {
                Device.OpenUri(new Uri("http://alloutcoin.com"));

            };
            

            BindingContext = this;
        }

        async void Test_Clicked(object sender, EventArgs e)
        {
            try
            {

                apilogin.API_KEY_PASSWORD = apilogin.API_KEY_PASSWORD.Trim();
                apilogin.API_KEY = apilogin.API_KEY.Trim();
                apilogin.API_KEY_SECRET = apilogin.API_KEY_SECRET.Trim();

                this.apikey.IsEnabled = false;
                this.apikeypassword.IsEnabled = false;
                this.apikeysecret.IsEnabled = false;
                this.error.IsVisible = true;

                var data = await LoadData.LoadCoinBalanceAsync(apilogin);

                this.error.TextColor = Color.Blue;
                this.error.Text = "Success ! Loaded " + data.Values.Count + " Coin Types.";

                var currentExchanges = await Settings.ExchangeListAsync();
                if (currentExchanges == null)
                {
                    currentExchanges = new List<APILogin>();
                }

                if (currentExchanges.FirstOrDefault(x => x.API_KEY == apilogin.API_KEY && x.API_KEY_SECRET == apilogin.API_KEY_SECRET
                                                    && x.API_KEY_PASSWORD == apilogin.API_KEY_PASSWORD) == null)
                {

                    currentExchanges.Add(apilogin);
                    await Settings.SaveExchangesList(currentExchanges);
                    currentExchanges = await Settings.ExchangeListAsync();
                    if (currentExchanges.FirstOrDefault(x => x.API_KEY == apilogin.API_KEY) != null)
                    {
                        this.test.Text = "Test is Complete";

                        await Navigation.PopToRootAsync();
                    }
                    else
                    {

                        this.error.Text = "ERROR Saving Please Retry.";
                    }
                }
                else
                {
                    this.error.Text = "This API is already added to your list!";
                }
                this.test.IsEnabled = false;

            }
            catch (Exception x)
            {
                this.error.Text = x.Message?.ToString() + " " + x.InnerException?.ToString();
                this.apikey.IsEnabled = true;
                this.apikeypassword.IsEnabled = true;
                this.apikeysecret.IsEnabled = true;
            }
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
           
        }
    }
}