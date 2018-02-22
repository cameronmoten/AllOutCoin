using System;
using System.Windows.Input;

using Xamarin.Forms;

//Cameron Moten
namespace AllOutCoin.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://alloutcoin.com/")));
        }

        public ICommand OpenWebCommand { get; }
    }
}