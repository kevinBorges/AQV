using AntesQueVenca.Views;
using Xamarin.Forms;

namespace AntesQueVenca
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new RegisterView());
        }
    }
}
