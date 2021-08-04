
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeProject.Presentation.Mobile.App.Components.Loadings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentView
    {

        public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
                                                                                                "IsLoading",
                                                                                                typeof(bool),
                                                                                                typeof(Loading),
                                                                                                false,
                                                                                                propertyChanged: IsLoadingPropertyChanging,
                                                                                                 defaultBindingMode: BindingMode.TwoWay);
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        public Loading()
        {
            InitializeComponent();
        }

        public static async void IsLoadingPropertyChanging(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as Loading;
            var value = (bool)newValue;
            await control.FadeTo(value ? 1 : 0, 1000, Easing.SpringOut);
        }

    }
}