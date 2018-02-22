using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AllOutCoin.Models;
using AllOutCoin.ViewModels;
using Microcharts.Forms;
using Microcharts;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace AllOutCoin.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDetailPage : ContentPage
	{
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                TextColor = SKColor.Parse("#266489")
                },
                new Microcharts.Entry(400)
                {
                Label = "February",
                ValueLabel = "400",
                TextColor = SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(-100)
                {
                Label = "March",
                ValueLabel = "-100",
                TextColor = SKColor.Parse("#90D585")
                }
            };
            var chart = new LineChart() { Entries = entries, BackgroundColor=SKColor.Parse("00FFFFFF")  };
            this.chartView.Chart = chart;
        }

        public ItemDetailPage()
        {
            InitializeComponent();
            
            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };
            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}