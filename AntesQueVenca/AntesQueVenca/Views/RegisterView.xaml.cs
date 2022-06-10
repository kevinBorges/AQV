using AntesQueVenca.Interfaces;
using AntesQueVenca.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage, IMessage
    {
        private UserViewModel userViewModel;

        public RegisterView()
        {
            InitializeComponent();
            BindingContext = userViewModel = new UserViewModel { Navigation = this.Navigation, Message = this };
        }
    }
}