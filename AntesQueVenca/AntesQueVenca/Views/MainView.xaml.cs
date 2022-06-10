﻿using AntesQueVenca.Models;
using AntesQueVenca.ViewModels;
using AntesQueVenca.Views.MasterPage;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntesQueVenca.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : ContentPage
    {
        private MainViewModel _viewModel;
        public MainView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = _viewModel = new MainViewModel();
            SelectFirstItem();
        }

        private void SelectFirstItem()
        {
            if (Footer.Children.FirstOrDefault() is StackLayout stackLayout)
                ChangeState(stackLayout, "Selected");
        }

        void CarouselView_CurrentItemChanged(System.Object sender, Xamarin.Forms.CurrentItemChangedEventArgs e)
        {
            if (e.CurrentItem is AppPage currentItem)
            {
                var currentPageIndex = _viewModel.Pages.IndexOf(currentItem);

                if (Footer.Children[currentPageIndex] is StackLayout stackLayout)
                    ChangeState(stackLayout, "Selected");
            }

            if (e.PreviousItem is AppPage previousItem)
            {
                var previousPageIndex = _viewModel.Pages.IndexOf(previousItem);

                if (Footer.Children[previousPageIndex] is StackLayout stackLayout)
                    ChangeState(stackLayout, "UnSelected");
            }
        }

        private void ChangeState(StackLayout stackLayout, string stateName)
        {
            if (stackLayout.Children.FirstOrDefault() is Image icon)
                VisualStateManager.GoToState(icon, stateName);

            if (stackLayout.Children.LastOrDefault() is Label name)
                VisualStateManager.GoToState(name, stateName);
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            if (sender is StackLayout stackLayout)
            {
                var index = Footer.Children.IndexOf(stackLayout);
                CarouselView.ScrollTo(index);
            }
        }

        private async void CartIcon_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartView());
        }

        private void MenuIcon_Tapped(object sender, EventArgs e)
        {
            
        }
    }
}