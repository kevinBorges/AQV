using AntesQueVenca.Helper;
using AntesQueVenca.ViewModels;
using AntesQueVenca.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfileView : ContentView
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null) 
            {
                var page = (ProfilePage)e.SelectedItem;
                switch (page.PageId)
                {
                    case 1:
                        await Navigation.PushAsync(new AddressesView());
                        break;
                    case 2:
                        await Navigation.PushAsync(new PaymentsView());
                        break;
                    case 3:
                        await Navigation.PushAsync(new MyOrdersView());
                        break;
                    case 4:
                        await Navigation.PushAsync(new SuggestMarketView());
                        break;
                    default:
                        break;
                }
            }
        }

        private async void InformationListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var page = (ProfilePage)e.SelectedItem;
                switch (page.PageId)
                {
                    case 1:
                        await Navigation.PushAsync(new TermsView());
                        break;
                    case 2:
                        await Navigation.PushAsync(new PartnerView());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}