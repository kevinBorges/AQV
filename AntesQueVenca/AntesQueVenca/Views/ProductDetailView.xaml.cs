using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailView : ContentPage
    {
        public ProductDetailView()
        {
            InitializeComponent();
        }

        private void Button_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {

        }
    }
}