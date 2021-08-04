using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeProject.Presentation.Mobile.App.Models;
using TimeProject.Presentation.Mobile.App.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeProject.Presentation.Mobile.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        public string Tenanty { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool IsError
        {
            get { return this.Errors.Any(); }
        }

        public ObservableCollection<KeyValuePair<string,string>> Errors { get; set; }

        private AuthService _authService;

        public RegisterPage()
        {
            Errors = new ObservableCollection<KeyValuePair<string, string>>();

            InitializeComponent();

            this.BindingContext = this;

          
            _authService = new AuthService();
        }

        private void SetErrors<T>(ApiResponse<T> apiResponse)
        {
            Errors.Clear();

            if (apiResponse.Success || !apiResponse.Errors.Any()) return;
            else apiResponse.Errors.ForEach(error => Errors.Add(error));
            this.OnPropertyChanged(nameof(IsError));
        }

        private async void AcceptButton_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;
            var res = await _authService.SignUp(Tenanty, Email, Name, Password, ConfirmPassword);
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
    }
}