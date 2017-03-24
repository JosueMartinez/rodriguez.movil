using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace rodriguez
{
	public partial class BonosView : ContentPage
	{
		readonly IList<Bono> bonos = new ObservableCollection<Bono>();
		readonly BonosManager manager = new BonosManager();

		public BonosView()
		{
			InitializeComponent();
			//Setting toolba
			ToolbarItems.Add(new ToolbarItem("Click", null, () =>
			{
				Debug.WriteLine("Clicked");
			}));

			//Populating ListView
			//BindingContext = bonos;
			refresh();
			BonosList.SeparatorColor = Color.Red;
			BonosList.ItemsSource = bonos;
		}

		async void refresh()
		{
			this.IsBusy = true;
			var bonosLista = await manager.GetAll();  //obtaining bonos from Server
			foreach (Bono bono in bonosLista)
			{
				if (bonos.All(b => b.id != bono.id))
					bonos.Add(bono);
			}
			this.IsBusy = false;
		}
	}
}
