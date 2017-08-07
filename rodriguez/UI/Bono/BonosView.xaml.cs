using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FormsPlugin.Iconize;
using rodriguez.Data;
using Xamarin.Forms;

namespace rodriguez
{
    public partial class BonosView : ContentPage
    {
        BonosManager manager { get; set; }
        MonedaManager monedaManager { get; set; }

        public BonosView()
        {
            manager = new BonosManager();

            InitializeComponent();

            refreshData();

            //Toolbar Items
            ToolbarItems.Add(new IconToolbarItem
            {
                Icon = "fa-plus",
                IconColor = Color.White,
                Command = new Command(this.addBono)
            });
        }

        async void refreshData()
        {
            this.IsBusy = true;
            var bonosLista = await manager.GetAll();  //obtaining bonos from Server

            if (bonosLista != null)
            {
                if (bonosLista.Count() > 0)
                {
                    BonosList.ItemsSource = bonosLista;
                }
                else
                {
                    BonosList.IsVisible = false;
                    BonosListMessage.IsVisible = true;
                }
            }
            else
            {
                await DisplayAlert("Error!", "Se ha producido un error en la conexión", "OK");
            }



            this.IsBusy = false;
        }

        async void ViewDetails(object sender, ItemTappedEventArgs e)
        {
            Bono b = (Bono)e.Item;
            await Navigation.PushAsync(new BonoDetail(b));
        }

        void addBono(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddBono());
        }

        void addBono()
        {
            Navigation.PushAsync(new AddBono());
        }
    }
}
