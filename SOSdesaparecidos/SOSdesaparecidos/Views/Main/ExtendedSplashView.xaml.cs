﻿using SOSdesaparecidos.ViewModels.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SOSdesaparecidos.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedSplashView : ContentPage
    {
        public ExtendedSplashView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = BindingContext as ExtendedSplashViewModel;

            await Task.Delay(2000);

            await Navigation.PushAsync(new MainView());

            var views = Navigation.NavigationStack.ToList();

            foreach (var view in views)
            {
                Navigation.RemovePage(view);
            }
        }
    }
}
