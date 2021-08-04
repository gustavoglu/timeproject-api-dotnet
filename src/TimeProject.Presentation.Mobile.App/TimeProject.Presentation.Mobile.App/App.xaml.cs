using System;
using System.Net.Http;
using TimeProject.Presentation.Mobile.App.Api;
using TimeProject.Presentation.Mobile.App.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeProject.Presentation.Mobile.App
{
    public partial class App : Application
    {
        public static HttpClient ApiClient;
        public App()
        {
            InitializeComponent();

            ApiClient = new TimeProjectHttpClient().Client;
            MainPage = new NavigationPage(new LoginPage()) {  };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
