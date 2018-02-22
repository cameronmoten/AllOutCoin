using System;

using AllOutCoin.Views;
using Xamarin.Forms;
using Xamarin.Forms.Themes;

namespace AllOutCoin
{
	public partial class App : Application
	{
        public static App myapp;
		public App ()
		{
            myapp = this;

            InitializeComponent();
            //Resources = new DarkThemeResources();

            MainPage = (new MainPage());
        }
        public void SwitchTheme()
        {
            if (Resources?.GetType() == typeof(DarkThemeResources))
            {
                Resources = new LightThemeResources();
                return;
            }
            Resources = new DarkThemeResources();
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
