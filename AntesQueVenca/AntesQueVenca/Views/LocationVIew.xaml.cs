using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationView : ContentPage
    {
        public LocationView()
        {
            InitializeComponent();
         //   NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationComplementView());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MainView());
        }
    }
}