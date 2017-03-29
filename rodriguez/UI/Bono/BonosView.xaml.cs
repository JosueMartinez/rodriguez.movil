using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Iconize;

namespace rodriguez
{
	public partial class BonosView : ContentPage
	{
		readonly IList<Bono> bonos = new ObservableCollection<Bono>();
		readonly BonosManager manager = new BonosManager();

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
			BindingContext = bonos;
		}

		async void refreshData()
		{
			this.IsBusy = true;
			var bonosLista = await manager.GetAll();  //obtaining bonos from Server

			if (bonosLista.Count() > 0)
			{
				foreach (Bono bono in bonosLista)
				{
					if (bonos.All(b => b.id != bono.id))
						bonos.Add(bono);
				}
			}
			else
			{
				BonosList.IsVisible = false;
				BonosListMessage.IsVisible = true;
			}


			this.IsBusy = false;
		}

		async void ViewDetails(object sender, ItemTappedEventArgs e)
		{
			Bono b = (Bono)e.Item;
			await Navigation.PushAsync(new BonoDetail(b));
		}

		async void addBono()
		{
			Debug.WriteLine("Agregar Bono");
			await Navigation.PushAsync(new AddBono());
		}
	}
}
