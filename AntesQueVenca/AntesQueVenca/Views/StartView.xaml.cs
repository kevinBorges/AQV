using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartView : ContentPage
    {
        public StartView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LocationView());
        }
    }
}