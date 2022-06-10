using AntesQueVenca.Mobile.Admin.Interfaces;
using AntesQueVenca.Mobile.Admin.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Mobile.Admin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersView : ContentPage, IMessage
    {
        private OrdersViewModel ordersViewModel;

        public OrdersView()
        {
            InitializeComponent();
            BindingContext = ordersViewModel = new OrdersViewModel { Navigation = this.Navigation, Message = this };
            
            datePickerDe.Date = DateTime.Now.Date;
            datePickerAte.Date = DateTime.Now.Date;
            datePickerDe.MinimumDate = DateTime.Now.AddYears(-1);
            datePickerAte.MaximumDate = DateTime.Now.AddMonths(1);
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await Navigation.PushAsync(new OrderDetailView());
        }
    }
}