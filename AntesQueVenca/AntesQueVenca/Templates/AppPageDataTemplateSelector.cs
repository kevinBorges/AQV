using AntesQueVenca.ContentViews;
using AntesQueVenca.Models;
using Xamarin.Forms;

namespace AntesQueVenca.Templates
{
    public class AppPageDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate _home;
        private readonly DataTemplate _products;
        private readonly DataTemplate _orders;
        private readonly DataTemplate _profile;

        public AppPageDataTemplateSelector()
        {
            _home = new DataTemplate(typeof(HomeContentView));
            _products = new DataTemplate(typeof(ProductsContentView));
            _orders = new DataTemplate(typeof(OrdersContentView));
            _profile = new DataTemplate(typeof(ProfileView));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is AppPage appPage)
            {
                if (appPage.Type == AppPageType.Home)
                    return _home;

                if (appPage.Type == AppPageType.Products)
                    return _products;

                if (appPage.Type == AppPageType.Orders)
                    return _orders;

                if (appPage.Type == AppPageType.Profile)
                    return _profile;

                return new DataTemplate(typeof(ContentView));
            }

            return new DataTemplate(typeof(ContentView));
        }
    }
}
