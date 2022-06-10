using AntesQueVenca.Mobile.Admin.Views.Master;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Mobile.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private async void EsqueciSenha_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Será implementado em breve", "OK");
        }

        private async void CriarConta_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Será implementado em breve", "OK");
        }

        private async void EntrarButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}