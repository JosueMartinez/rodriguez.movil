using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using rodriguez.Data;
using System.Collections.ObjectModel;
using System.Linq;
using FormsPlugin.Iconize;
using rodriguez.UI.Compras;

namespace rodriguez
{
    public partial class ComprasView : ContentPage
    {
        private INavigation _navigation;

        public ComprasView()
        {
            InitializeComponent();

            //Toolbar Items
            ToolbarItems.Add(new IconToolbarItem
            {
                Icon = "fa-plus",
                IconColor = Color.White,
                Command = new Command(this.AgregarProducto)
            });

            BindingContext = new ComprasViewModel(Navigation);
        }

        private void AgregarProducto(object obj)
        {
            Navigation.PushModalAsync(new AgregarProducto());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as ComprasViewModel).RefrescarLista();
        }
    }
}
