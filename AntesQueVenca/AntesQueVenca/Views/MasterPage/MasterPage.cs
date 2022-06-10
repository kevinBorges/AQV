using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AntesQueVenca.Views.MasterPage
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

            //var imageHeader = GetCircleImage();
            //var lblNomeUsuario = GetLabelNomeUsuario();

            //listView.Header = GetStackHeader(imageHeader, lblNomeUsuario);

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
                IconSource = "ordersOrangeIcon.png",
                Title = "Main",
                TargetType = typeof(MainView)
            });

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "sincronizacao.png",
            //    Title = "Atualizar dados",
            //    TargetType = typeof(SincronizacaoView)
            //});

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "suporte.png",
            //    Title = "Suporte",
            //    TargetType = typeof(ListaLaudosView)
            //});

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "criteriosTecnicos.png",
            //    Title = "Critérios técnicos",
            //    TargetType = typeof(CriteriosTecnicosView)
            //});

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "termosVistoria.png",
            //    Title = "Termo de vistoria",
            //    TargetType = typeof(TermoVistoriaView)
            //});

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "suporte.png",
            //    Title = "Enviar log"
            //});

            //masterPageItems.Add(new MasterPageItem
            //{
            //    IconSource = "sair.png",
            //    Title = "Sair"
            //});
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

        //private CircleImage GetCircleImage()
        //{
        //    return new CircleImage
        //    {
        //        BorderColor = (Color)Application.Current.Resources["VermelhoCorPrincipal"],
        //        BorderThickness = 3,
        //        HeightRequest = 100,
        //        WidthRequest = 100,
        //        Aspect = Aspect.AspectFit,
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        Source = "https://scontent.fcgh19-1.fna.fbcdn.net/v/t1.0-9/50768143_2277126799198664_2416671496370913280_n.jpg?_nc_cat=109&_nc_ht=scontent.fcgh19-1.fna&oh=600db8003dae98fd4e8b12cbcce40711&oe=5D4E7E1E"
        //    };
        //}

        //private Label GetLabelNomeUsuario()
        //{
        //    return new Label
        //    {
        //        TextColor = (Color)Application.Current.Resources["AzulCorTextoPrincipal"],
        //        Text = StoreVars.Loggeduser != null && StoreVars.Loggeduser.Pessoa != null && !string.IsNullOrEmpty(StoreVars.Loggeduser.Pessoa.Nome) ? StoreVars.Loggeduser.Pessoa.Nome : "Super Visão",
        //        FontAttributes = FontAttributes.Bold,
        //        HorizontalTextAlignment = TextAlignment.Center,
        //        FontSize = 20
        //    };
        //}

        //private StackLayout GetStackHeader(CircleImage imageHeader, Label lblNomeUsuario)
        //{
        //    var stHeaderTop = new StackLayout() { };
        //    var stHeader = new StackLayout()
        //    {
        //        HorizontalOptions = LayoutOptions.CenterAndExpand,
        //        Padding = new Thickness(20),
        //        BackgroundColor = Color.White,
        //    };

        //    //stHeader.Children.Add(imageHeader);
        //    stHeader.Children.Add(lblNomeUsuario);

        //    stHeaderTop.Children.Add(stHeader);
        //    stHeaderTop.Children.Add(new BoxView { HeightRequest = 1, BackgroundColor = (Color)Application.Current.Resources["VermelhoCorPrincipal"] });
        //    return stHeaderTop;
        //}
    }
}
