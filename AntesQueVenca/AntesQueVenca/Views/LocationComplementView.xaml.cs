using AntesQueVenca.Views.MasterPage;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationComplementView : ContentPage
    {
        public LocationComplementView()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync(true);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}