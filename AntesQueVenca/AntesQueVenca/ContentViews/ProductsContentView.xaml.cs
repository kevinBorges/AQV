using AntesQueVenca.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsContentView : ContentView
    {
        public ProductsContentView()
        {
            InitializeComponent();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new ProductDetailView());
        }
    }
}