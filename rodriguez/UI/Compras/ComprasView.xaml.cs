﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using rodriguez.Data;
using System.Collections.ObjectModel;
using System.Linq;

namespace rodriguez
{
	public partial class ComprasView : ContentPage
	{
		readonly IList<listacompra> listas = new ObservableCollection<listacompra>();
		readonly listaCompraManager manager = new listaCompraManager();

		public ComprasView()
		{
			this.Appearing += (object sender, EventArgs e) => {
				refreshData();
				ListasList.ItemsSource = listas;
			};

			InitializeComponent();

		}

		async void refreshData()
		{
			this.IsBusy = true;
			listas.Clear();
			var listasLista = await manager.GetAll();  //obtaining listas from Server

			if (listasLista != null)
			{
				if (listasLista.Count() > 0)
				{
					
					foreach (listacompra lista in listasLista.OrderByDescending(x => x.fechaCreacion))
					{
						//bono.tasa.moneda = await monedaManager.GetByID(bono.tasa.monedaId);
						if (listas.All(l => l.id != lista.id))
							listas.Add(lista);
					}
				}
				else
				{
					ListasList.IsVisible = false;
					ListasListMessage.IsVisible = true;
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

		void addLista(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new AddLista());
		}
	}
}
