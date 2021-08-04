using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeProject.Presentation.Mobile.App.Components.Alerts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Alert : ContentView
    {

        public ObservableCollection<string> Alerts { get { return (ObservableCollection<string>)GetValue(AlertsProperty); } set { SetValue(AlertsProperty, value); } }

        public static BindableProperty AlertsProperty = BindableProperty.Create("Alerts",
                                                                            typeof(ObservableCollection<string>),
                                                                            typeof(Alert),
                                                                            defaultValue: new ObservableCollection<string>(),
                                                                            propertyChanged: AlertsPropertyChanging
                                                                            );
        public Alert()
        {
            InitializeComponent();
        }

        public static async void AlertsPropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Alert;

            //var oldValueList = (List<string>)oldValue;
            var newValueList = (ObservableCollection<string>)newValue;


            bool open = newValue != null && newValueList.Any();

            if (open)
                await control.TranslateTo(0, 0, 500, Easing.SpringOut);
            else
                await control.TranslateTo(0, -100, 500, Easing.SpringIn);
        }


        public async Task ClearAlerts(Alert alert = null)
        {
            if (alert == null) alert = this;
            Alerts = new ObservableCollection<string>();
            await alert.TranslateTo(0, -100, 500, Easing.SpringIn);
        }

        public async void CloseTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await ClearAlerts();
        }
    }
}