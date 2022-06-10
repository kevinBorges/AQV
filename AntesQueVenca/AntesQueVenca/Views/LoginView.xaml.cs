using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage//, IMessage
    {
        public LoginView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void EsqueciSenha_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Será implementado em breve", "OK");
        }

        private async void CriarConta_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterView());
        }

        private async void EntrarButton_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LocationView());
        }

        private async void FacebookButton_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Será implementado em breve", "OK");
        }

        private async void GoogleButton_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("Aviso", "Será implementado em breve", "OK");
        }
    }
}