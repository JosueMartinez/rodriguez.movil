using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class BonosMasterDetail : MasterDetailPage
	{
		readonly IList<Bono> bonos;
		readonly BonosManager manager;

		public BonosMasterDetail()
		{

			manager = new BonosManager();
			bonos = new ObservableCollection<Bono>();
			refresh();

			InitializeComponent();
			Label header = new Label
			{
				Text = "MasterDetailPage",
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
				HorizontalOptions = LayoutOptions.Center
			};

			// Create ListView for the master page.
			ListView listView = new ListView
			{
				ItemsSource = bonos
			};

			// Create the master page with the ListView.
			this.Master = new ContentPage
			{
				Title = "Color List",       // Title required!
				Content = new StackLayout
				{
					Children = {
						header,
						listView
					}
				}
			};

			BonoDetail detail = new BonoDetail();

			this.Detail = detail;
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
