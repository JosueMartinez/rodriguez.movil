using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using rodriguez.Data;
using System.Threading.Tasks;

namespace rodriguez
{
	public partial class AddBono : ContentPage
	{
		IEnumerable<moneda> monedas;
		readonly MonedaManager monedaManager = new MonedaManager();
		readonly TasaManager tasaManager = new TasaManager();
		readonly BonosManager bonoManager = new BonosManager();
		private tasa tasa {get;set;}
		private moneda monedaSeleccionada { get; set; }
		private double tasaDia { get; set; }
		private double montoRD { get; set; }

		public AddBono()
		{

			getMonedas();
			InitializeComponent();
			BindingContext = this;
		}

		async void getMonedas()
		{

			Task<IEnumerable<moneda>> monedasTask = monedaManager.GetAll();
			monedas = await monedasTask;
			cbMoneda.ItemsSource = (System.Collections.IList)monedas;

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
					monto = int.Parse(txtMonto.Text),
					fechaCompra = DateTime.Now,
					tasaId = tasa.id
				};
				b.clienteId = 1; //TODO get logged user

				//TODO comprar bono
				if (validarBono(b))
				{
					if (bonoManager.buyBono(b) != null)
					{
						
						await Navigation.PopAsync();
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
					Utils.IsValidPhone(b.telefonoDestino.Trim()) &&
				        monedaSeleccionada != null);

		}

		void OnMontoChange(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			double valorNuevo = 0.00;
			double.TryParse(e.NewTextValue, out valorNuevo);
			montoRD = valorNuevo * tasaDia;
			lbMontoRD.Text = MontoRD;
		}

		async void OnMonedaChange(object sender, System.EventArgs e)
		{
			if (cbMoneda.SelectedIndex != -1)
			{
				moneda mon = cbMoneda.SelectedItem as moneda;
				Task<tasa> tasaTask = tasaManager.GetBySimbolo(mon.simbolo);
				tasa = await tasaTask;
				tasaDia = tasa.valor;
				lbTasaDia.Text = TasaDia;
				montoRD = double.Parse(txtMonto.Text != null? txtMonto.Text: "0.00") * tasaDia;
				lbMontoRD.Text = MontoRD;
				monedaSeleccionada = mon;
			}
		}

		public string TasaDia
		{
			get
			{
				return String.Format("Tasa del día: {0:0.00}", tasaDia);
			}
		}


		public string MontoRD
		{
			get
			{
				return String.Format("RD$ {0:#,##0.00}", montoRD);
			}
		}

	}
}