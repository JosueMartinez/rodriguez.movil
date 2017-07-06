using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Iconize;
using FormsPlugin.Iconize;
using rodriguez.Data;

namespace rodriguez
{
    public partial class BonosView : ContentPage
    {
        IList<Bono> bonos { get; set; }
        BonosManager manager { get; set; }
        MonedaManager monedaManager { get; set; }

        public BonosView()
        {
            bonos = new ObservableCollection<Bono>();
            manager = new BonosManager();
            monedaManager = new MonedaManager();

            this.Appearing += (object sender, EventArgs e) =>
            {
                refreshData();
                BonosList.ItemsSource = bonos;
            };

            InitializeComponent();

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
            bonos.Clear();
            var bonosLista = await manager.GetAll();  //obtaining bonos from Server

            if (bonosLista != null)
            {
                if (bonosLista.Count() > 0)
                {

                    foreach (Bono bono in bonosLista.OrderByDescending(x => x.fechaCompra))
                    {
                        bono.tasa.moneda = await monedaManager.GetByID(bono.tasa.monedaId);
                        if (bonos.All(b => b.id != bono.id))
                            bonos.Add(bono);
                    }
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
