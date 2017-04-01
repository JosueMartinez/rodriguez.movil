using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Internals;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using rodriguez.Data;
using System.Linq;

namespace rodriguez
{
	public partial class AddBono : ContentPage
	{
		ObservableCollection<moneda> monedas;
		readonly MonedaManager manager = new MonedaManager();


		public AddBono()
		{
			tasaDia = 45.90; //TODO obtener tasa diaria con respecto a la moneda seleccionada
			getMonedas();

			MonedasList = new ObservableCollection<moneda>() {
				new moneda(){ descripcion= "Peso Dominicano", simbolo = "RD$" },
				new moneda(){ descripcion= "Dolar Estadounidense", simbolo = "USD" },
				new moneda(){ descripcion= "Euro", simbolo = "EUR" }
			};
			MonedaSeleccionada = MonedasList.Where(x => x.simbolo == "USD");

			InitializeComponent();


			BindingContext = this;




		}

		async void getMonedas()
		{
			var monedasLista = await manager.GetAll();  //obtaining bonos from Server

			foreach (moneda m in monedasLista)
			{
				monedas.Add(m);
			}

		}

		async void comprarBono(object sender, System.EventArgs e)
		{

			Bono b = new Bono()
			{
				//nombreDestino = txtDestinatario.Text,
				//cedulaDestino = txtCedula.Text,
				//telefonoDestino = txtCelular.Text
			};
			b.monedaId = 1;  //  TODO get selected moneda ID ----- cbMoneda.SelectedIndex ;
			b.clienteId = 1; //TODO get logged user
			b.fechaCompra = DateTime.Now;

			//TODO comprar bono
			if (validarBono(b))
			{
				Debug.WriteLine("Listo para comprar");
			}
			else
			{
				Debug.WriteLine("Hay errores en los datos introducidos");
			}
		}

		bool validarBono(Bono b)
		{
			return (Utils.WordsCount(b.nombreCompleto) > 0 &&
					Utils.IsValidCedula(b.cedulaDestino) &&
					Utils.IsValidPhone(b.telefonoDestino.Trim()));

		}

		void OnMontoChange(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			double valorNuevo = 0.00;
			double.TryParse(e.NewTextValue, out valorNuevo);
			montoRD = valorNuevo * tasaDia;
			lbMontoRD.Text = MontoRD;
		}

		private object monedaSeleccionada;
		public object MonedaSeleccionada
		{
			get
			{
				return monedaSeleccionada;
			}
			set
			{
				monedaSeleccionada = value;
				OnPropertyChanged("MonedaSeleccionada");
			}
		}

		public ObservableCollection<moneda> MonedasList
		{
			get { return monedas; }
			set
			{
				monedas = value;
				OnPropertyChanged("MonedasList");
			}
		}

		private double tasaDia { get; set; }
		public string TasaDia
		{
			get
			{
				return String.Format("Tasa del día: {0:0.00}", tasaDia);
			}
		}

		private double montoRD { get; set; }
		public string MontoRD
		{
			get
			{
				return String.Format("RD$ {0:#,##0.00}", montoRD);
			}
		}

	}
}