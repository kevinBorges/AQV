using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AntesQueVenca.Mobile.Admin.Views.Master
{
    [Preserve(AllMembers = true)]
    public class MasterPage : ContentPage
    {
        private ListView listView;
        public ListView ListView
        {
            get
            {
                return listView;
            }
        }

        public MasterPage()
        {
            this.Title = "Menu";

            var masterPageItems = new List<MasterPageItem>();
            AddMasterPageItens(masterPageItems);
            listView = GetListView(masterPageItems);

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    listView
                }
            };
        }

        private void AddMasterPageItens(List<MasterPageItem> masterPageItems)
        {
            masterPageItems.Add(new MasterPageItem
            {
                IconSource = "ordersIcon.png",
                Title = "Pedidos",
                TargetType = typeof(OrdersView)
            });

            masterPageItems.Add(new MasterPageItem
            {
                IconSource = "logoutIcon.png",
                Title = "Sair",
                TargetType = typeof(OrdersView)
            });
        }

        private ListView GetListView(List<MasterPageItem> masterPageItems)
        {
            return new ListView
            {
                ItemTemplate = GetItemTemplate(),
                ItemsSource = masterPageItems,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None
            };
        }

        private DataTemplate GetItemTemplate()
        {
            return new DataTemplate(() =>
            {
                var layout = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    BackgroundColor = Color.White,
                    Margin = new Thickness(0, 0, 0, 0)
                };

                layout.Children.Add(GetIcon());
                layout.Children.Add(GetItemLabel());
                return new ViewCell { View = layout };
            });
        }

        private Image GetIcon()
        {
            var img = new Image
            {
                HeightRequest = 32,
                WidthRequest = 32,
                Aspect = Aspect.AspectFit,
                Margin = new Thickness(20, 0, 0, 0)
            };
            img.SetBinding(Image.SourceProperty, "IconSource");
            return img;
        }

        private Label GetItemLabel()
        {
            var lblItem = new Label
            {
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(10)
            };
            lblItem.SetBinding(Label.TextProperty, "Title");
            return lblItem;
        }
    }
}
