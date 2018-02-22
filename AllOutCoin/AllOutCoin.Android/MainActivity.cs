using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using HockeyApp.Android;
using HockeyApp.Android.Metrics;

namespace AllOutCoin.Droid
{
    [Activity(Label = "AllOutCoin", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            MetricsManager.Register(Application, "a11b309001cc42e59c0a9f4959ee013e");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            CrashManager.Register(this, "a11b309001cc42e59c0a9f4959ee013e");
        }
    }
}

