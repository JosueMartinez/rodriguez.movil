using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using Xamarin.Forms.Internals;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
using rodriguez.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace rodriguez
{
	public partial class AddBono : ContentPage
	{
		ObservableCollection<moneda> monedas;
		readonly MonedaManager monedaManager = new MonedaManager();
		readonly TasaManager tasaManager = new TasaManager();
		readonly BonosManager bonoManager = new BonosManager();
		tasa tasaActual;


		public AddBono()
		{

			getMonedas();

			MonedaSeleccionada = new moneda() { descripcion = "Dolar Estadounidense", simbolo = "USD" };

			MonedasList = new ObservableCollection<moneda>() {
				new moneda(){ descripcion= "Peso Dominicano", simbolo = "RD" },
				MonedaSeleccionada,
				new moneda(){ descripcion= "Euro", simbolo = "EUR" }
			};

			GetTasaActual(MonedaSeleccionada);
			InitializeComponent();
			BindingContext = this;
		}

		async void getMonedas()
		{
			var monedasLista = await monedaManager.GetAll();  //obtaining bonos from Server

			foreach (moneda m in monedasLista)
			{
				monedas.Add(m);
			}

		}

		async void GetTasaActual(moneda moneda)
		{
			tasaActual = await tasaManager.GetBySimbolo(moneda.simbolo);
			tasaDia = tasaActual.valor;

		}

		async void comprarBono(object sender, System.EventArgs e)
		{
			var loading = new ActivityIndicator()
			{
				Color = Color.Red,
				IsVisible = true,
				IsEnabled = true,
				IsRunning = true
			};


			try
			{
				Bono b = new Bono()
				{
					nombreDestino = txtNombreDestinatario.Text,
					apellidoDestino = txtApellidoDestinatario.Text,
					cedulaDestino = txtCedula.Text,
					telefonoDestino = txtCelular.Text,
					monto = int.Parse(txtMonto.Text)
				};
				b.tasaId = 11;  //  TODO get selected moneda ID ----- cbMoneda.SelectedIndex ;
				b.clienteId = 1; //TODO get logged user
				b.fechaCompra = DateTime.Now;


				//TODO comprar bono
				if (validarBono(b))
				{
					if (bonoManager.buyBono(b) != null)
					{
						await Navigation.PushAsync(new BonosView());
					}
					else
					{
						await DisplayAlert("Error!", "Ha ocurrido un error. Por favor intente de nuevo", "OK");
					}
				}
				else
				{
					Debug.WriteLine("Hay errores en los datos introducidos");
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				await DisplayAlert("", "Hay errores en los datos introducidos", "OK");
			}

		}

		bool validarBono(Bono b)
		{
			return (b.nombreDestino.Length > 0 && b.apellidoDestino.Length > 0 &&
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

		private moneda monedaSeleccionada;
		public moneda MonedaSeleccionada
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

		async void OnMonedaChange(object sender, System.EventArgs e)
		{
			if (cbMoneda.SelectedIndex != -1)
			{
				moneda mon = cbMoneda.SelectedItem as moneda;
				Task<tasa> tasaTask = tasaManager.GetBySimbolo(mon.simbolo);
				tasa tasa = await tasaTask;
				tasaDia = tasa.valor;
				lbTasaDia.Text = TasaDia;
			}
		}

	}
}