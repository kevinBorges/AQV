using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AntesQueVenca.Mobile.Admin.Views.Master
{
    [Preserve(AllMembers = true)]
    public class MainPage : MasterDetailPage
    {
        private MasterPage masterPage;

        public MainPage()
        {
            masterPage = new MasterPage();
            NavigationPage.SetHasNavigationBar(this, false);
            Master = masterPage;
            Detail = new NavigationPage(new OrdersView()) { BarBackgroundColor = Color.FromHex("{StaticResource GreenPrincipalColor}") };
            MasterBehavior = MasterBehavior.Popover;
            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as MasterPageItem;

                if (item != null)
                {
                    if (item.Title.Equals("Sair"))
                    {
                        await Navigation.PopToRootAsync();
                        await Navigation.PushAsync(new LoginView());
                        Navigation.RemovePage(this);
                        return;
                    }
                    await Navigation.PushAsync((Page)Activator.CreateInstance(item.TargetType));
                    masterPage.ListView.SelectedItem = null;

                    if (MasterBehavior != MasterBehavior.SplitOnLandscape)
                        IsPresented = false;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", "Erro: " + ex, "OK");
                return;
            }
        }
    }
}
