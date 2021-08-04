using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;
using TimeProject.Presentation.Mobile.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TimeProject.Presentation.Mobile.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public string Tenanty { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public bool IsError
        {
            get { return this.Errors.Any(); }
        }

        private ObservableCollection<string> errors;

        public ObservableCollection<string> Errors
        {
            get { return errors; }
            set { errors = value; OnPropertyChanged(nameof(Errors)); }
        }

        private AuthService _authService;

        public LoginPage()
        {
            Errors = new ObservableCollection<string>();
            PropertyChanged += LoginPage_PropertyChanged;
            InitializeComponent();

            this.BindingContext = this;


            _authService = new AuthService();
        }



        protected override void OnAppearing()
        {

            base.OnAppearing();

            Task.WhenAll(
            pathLogo.RotateTo(360, 1000),
            pathLogo.TranslateTo(-10, 20, 1000)
            );



        }

        private async void LoginPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsError))
            {
                if (IsError)
                    await errorFrame.TranslateTo(0, 0, 500, Easing.SpringOut);
                else
                    await errorFrame.TranslateTo(0, -100, 500, Easing.SpringIn);
            }
        }

        private void SetErrors<T>(ApiResponse<T> apiResponse)
        {
            Errors.Clear();

            if (apiResponse.Success || !apiResponse.Errors.Any()) { return; }
            else
            {
                apiResponse.Errors.ForEach(error => Errors.Add(error.Value));
                this.OnPropertyChanged(nameof(Errors));
                this.OnPropertyChanged(nameof(IsError));
            }

            this.OnPropertyChanged(nameof(IsError));
        }

        private async void AcceptButton_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;

            var res = await _authService.SignIn(Tenanty, Email, Password);

            IsBusy = false;

            if (!res.Success) { SetErrors(res); }
            else
            {
                await DisplayAlert("Ok", "Ok", "Ok");
                this.OnPropertyChanged(nameof(IsError));
                Errors.Clear();

            }
        }



        private void CloseAlertTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Errors.Clear();
            this.OnPropertyChanged(nameof(IsError));

        }

        private async void SignOutTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new RegisterPage());
        }

        private async void Grid_LayoutChanged(object sender, EventArgs e)
        {
            var grid = (Grid)sender;

            await grid.FadeTo(IsBusy ? 1 : 0, 3000);

        }

        private async Task Loading()
        {
            //await indicatorGrid.FadeTo(IsBusy ? 1 : 0, 1000, Easing.SpringOut);
        }

        private async void ActivityIndicator_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsRunning") await Loading();
        }
    }
}