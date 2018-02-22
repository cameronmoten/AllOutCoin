using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using SkiaSharp;
using AllOutCoin.Models;
using AllOutCoin.Views;
using AllOutCoin.Services;

using Xamarin.Forms;
namespace AllOutCoin.ViewModels
{

    public class MainPageListViewModel : BaseViewModel
    {
        public ObservableCollection<Coin> coins { get; set; }
        public Command LoadItemsCommand { get; set; }

        public IDataStore<Coin> DataStoreCoin => DependencyService.Get<IDataStore<Coin>>() ?? new CoinDataStore();

        public MainPageListViewModel()
        {
            Title = "Browse shit";
            coins = new ObservableCollection<Coin>();
            LoadItemsCommand = new Command(async () => await this.ExecuteLoadItemsCommand());

            //this.repository = repository;

            //this.SelectModeCommand = new RelayCommand<RideMeasure>(x => this.Mode = x, (x) => true);
        }


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStoreCoin.GetItemsAsync(true);

                coins.Clear();
                foreach (var item in items)
                {
                    coins.Add(item);
                }
            }
            catch (Exception ex)
            {
                coins.Add(new Coin() { CoinTicker = "Error Loading Data " + ex.Message });
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /*private RideMeasure mode;

        private readonly IRideRepository repository;

        public IEnumerable<Ride> LastRides => repository.GetAll().Take(8);

        public IEnumerable<RideItemViewModel> Rides => repository.GetAll().Select(x => new RideItemViewModel(x));

        public RelayCommand<RideMeasure> SelectModeCommand { get; }

        public RideMeasure Mode
        {
            get => this.mode;
            set => this.Set(ref mode, value).ThenRaise(() => this.LastRidesChart);
        }*/

        #region Header charts

       /* private Entry CreateEntry(Ride ride)
        {
            return null;
            var label = ride.Date.ToString("MM/dd");
            var color = SKColors.White;
            var textcolor = SKColors.White.WithAlpha(180);
            switch (this.mode)
            {
                case RideMeasure.Calories:
                    return new Entry(ride.Calories)
                    {
                        ValueLabel = ((int)ride.Calories).ToString(),
                        TextColor = textcolor,
                        Label = label,
                        Color = color.WithAlpha(225),
                    };
                case RideMeasure.Distance:
                    return new Entry(ride.Distance)
                    {
                        ValueLabel = ((int)(ride.Distance / 1000)).ToString(),
                        TextColor = textcolor,
                        Label = label,
                        Color = color,
                    };
                case RideMeasure.Speed:
                    return new Entry(ride.AverageSpeed)
                    {
                        ValueLabel = ride.AverageSpeed.ToString("F1"),
                        TextColor = textcolor,
                        Label = label,
                        Color = color.WithAlpha(225),
                    };
                case RideMeasure.Time:
                    return new Entry((float)ride.TotalTime.TotalMinutes)
                    {
                        ValueLabel = $"{(int)ride.TotalTime.TotalHours}:{(int)ride.TotalTime.Minutes}",
                        TextColor = textcolor,
                        Label = label,
                        Color = color,
                    };
                default:
                    throw new NotSupportedException();

            }
        }*/

        public Chart LastRidesChart
        {
            get
            {
                var entrys = new List<Microcharts.Entry>();
                return new PointChart()
                {
                    Entries = entrys,
                    PointMode = PointMode.Circle,
                    PointSize = 20,
                    BackgroundColor = SKColors.Transparent,
                };
            }
            /*
             * get{
                var entries = this.LastRides.Select(CreateEntry);

                switch (this.mode)
                {
                    case RideMeasure.Distance:
                        return new PointChart()
                        {
                            Entries = new List<Entry>(),
                            PointMode = PointMode.Circle,
                            PointSize = 20,
                            BackgroundColor = SKColors.Transparent,
                        };

                    case RideMeasure.Speed:
                        return new PointChart()
                        {
                            Entries = entries,
                            PointMode = PointMode.Circle,
                            PointSize = 20,
                            MinValue = entries.Min(x => x.Value) - 0.1f,
                            BackgroundColor = SKColors.Transparent,
                        };

                    case RideMeasure.Calories:
                        return new BarChart()
                        {
                            Entries = entries,
                            BackgroundColor = SKColors.Transparent,
                        };
                    default:
                        return new LineChart()
                        {
                            Entries = entries,
                            LineMode = LineMode.Straight,
                            PointMode = PointMode.Circle,
                            PointSize = 20,
                            BackgroundColor = SKColors.Transparent,
                        };
                }
            }*/
        }

        #endregion
    }
}
