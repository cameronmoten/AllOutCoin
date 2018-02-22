using System;

using Xamarin.Forms;
using AllOutCoin.Services;
using AllOutCoin.ViewModels;
using Xamarin.Forms.Xaml;


namespace AllOutCoin.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainListPage : ContentPage
    {
        MainPageListViewModel viewModel;
        public MainListPage()
        {
            InitializeComponent();
            this.BindingContext  = viewModel = new MainPageListViewModel();

        }


        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            
        }


        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {


            await Navigation.PushAsync(new AddChoicePage());
            // await Navigation.PushModalAsync(new NavigationPage(new AddChoicePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.coins.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}



