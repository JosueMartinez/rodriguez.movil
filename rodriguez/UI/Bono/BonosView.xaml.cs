using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Iconize;
using rodriguez.Data;

namespace rodriguez
{
	public partial class BonosView : ContentPage
	{
		readonly IList<Bono> bonos = new ObservableCollection<Bono>();
		readonly BonosManager manager = new BonosManager();
		readonly MonedaManager monedaManager = new MonedaManager();

		public BonosView()
		{
			InitializeComponent();

			#region toolbar
			ToolbarItems.Add(new ToolbarItem("Add", null, () =>
			{
				addBono();
			}));
			#endregion

			refreshData();
			BonosList.ItemsSource = bonos;
			//BindingContext = bonos;
		}

		async void refreshData()
		{
			this.IsBusy = true;
			var bonosLista = await manager.GetAll();  //obtaining bonos from Server

			if (bonosLista != null)
			{
				if (bonosLista.Count() > 0)
				{
					foreach (Bono bono in bonosLista)
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
			//await DisplayAlert("Subject?", "Text", "Yes", "No");
		}

		async void ViewDetails(object sender, ItemTappedEventArgs e)
		{
			Bono b = (Bono)e.Item;
			await Navigation.PushAsync(new BonoDetail(b));
		}

		async void addBono()
		{
			await Navigation.PushAsync(new AddBono());
		}
	}
}
