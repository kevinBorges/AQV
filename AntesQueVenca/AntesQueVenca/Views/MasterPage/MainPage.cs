using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AntesQueVenca.Views.MasterPage
{
    [Preserve(AllMembers = true)]
    public class MainPage : MasterDetailPage
    {
        private MasterPage masterPage;

        public MainPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            Master = new MasterPage(); ;
            Detail = new NavigationPage(new MainView()) { BarBackgroundColor = Color.FromHex("{StaticResource GreenPrincipalColor}") };
            MasterBehavior = MasterBehavior.Popover;
            masterPage.ListView.ItemSelected += OnItemSelected;
        }

        //protected override void OnDisappearing()
        //{
        //    base.OnDisappearing();
        //    Navigation.RemovePage(this);
        //}

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
                    //else if (item.Title.Equals("Enviar log"))
                    //{
                    //    await Share.RequestAsync(new ShareFileRequest
                    //    {
                    //        Title = item.Title,
                    //        File = new ShareFile(Path.Combine(FileSystem.CacheDirectory, "Log.txt"))
                    //    });

                    //    return;
                    //}

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
