using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Diagnostics;
using rodriguez.Data;
using System.Threading.Tasks;
using XLabs.Ioc;
using System.Linq;
using System.Collections;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using PayPal.Forms.Abstractions.Enum;
using rodriguez.Data.PayPal;


namespace rodriguez
{
    public partial class AddBono : ContentPage
    {
        IEnumerable<moneda> monedas;
        readonly MonedaManager monedaManager = new MonedaManager();
        readonly TasaManager tasaManager = new TasaManager();
        readonly BonosManager bonoManager = new BonosManager();
        private tasa tasa { get; set; }
        private moneda monedaSeleccionada { get; set; }
        private double tasaDia { get; set; }
        private double montoRD { get; set; }
        private cliente cliente { get; set; }

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
            IList monedasList = (IList)monedas;
            cbMoneda.ItemsSource = monedasList;
            if (monedas != null && monedas.Count() > 0)
                monedaSeleccionada = (from mn in monedas where mn.simbolo.Equals("USD") select mn).FirstOrDefault();
            else
            {
                await DisplayAlert("", "No hay conexión para realizar las transacciones", "Ok");
                await Navigation.PopAsync();
            }
            cbMoneda.SelectedItem = monedaSeleccionada;
        }

        async void comprarBono(object sender, System.EventArgs e)
        {
            cliente = (rodriguez.Data.cliente)Application.Current.Properties["cliente"];
            btnComprar.IsEnabled = false;
            double montoBono;

            //tasa = cbMoneda.SelectedItem;
            Bono b = new Bono()
            {
                nombreDestino = txtNombreDestinatario.Text,
                apellidoDestino = txtApellidoDestinatario.Text,
                cedulaDestino = txtCedula.Text,
                telefonoDestino = txtCelular.Text,
                //monto = int.Parse(txtMonto.Text),
                fechaCompra = DateTime.Now
            };
            b.clienteId = cliente.id;  //1; //TODO get logged user

            if (txtMonto.Text != null)
            {
                double.TryParse(txtMonto.Text, out montoBono);
                b.monto = montoBono;
            }

            if (monedaSeleccionada != null)
            {
                b.tasaId = monedaSeleccionada.tasas.First().id;
            }

            if (validarBono(b))
            {
                var currency = monedaSeleccionada.simbolo.Equals("RD") ? "DOP" : monedaSeleccionada.simbolo.Equals("EU") ? "EUR" : monedaSeleccionada.simbolo;
                var payment = (new PayPalItem("Test Product", (decimal)b.monto, currency));


                var result = await CrossPayPalManager.Current.Buy(payment, new Decimal(0));
                if (result.Status == PayPalStatus.Cancelled)
                {
                    await DisplayAlert("Cancelado", "Ha cancelado el proceso", "Ok");
                }
                else if (result.Status == PayPalStatus.Error)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                }
                else if (result.Status == PayPalStatus.Successful)
                {
                    var paymentId = result.ServerResponse.Response.Id;
                    Debug.WriteLine(result.ServerResponse.Response.Id);
                    PayPalClient paypal = new PayPalClient();
                    PayPalPayment paymentDetail = await paypal.getPayment(paymentId);

                    b.paypalId = paymentId;

                    //TODO agregar demas propiedades del pago de paypal (estado y metodo)
                    try
                    {
                        var bonoResult = bonoManager.buyBono(b);
                        if (bonoResult != null)
                        {
                            await DisplayAlert("Exito", "Se ha comprado el bono de forma exitosa", "Ok");
                            await Navigation.PopAsync();

                            Debug.WriteLine("si");
                        }
                        else
                        {
                            await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.ToString());
                        await DisplayAlert("Error", "Ha ocurrido un error.  Intene de nuevo mas tarde", "Ok");
                    }


                }

                btnComprar.IsEnabled = true;

            }
            else
            {
                btnComprar.IsEnabled = true;
                await DisplayAlert("Faltan Datos", "Hay Errores en los datos introducidos", "Ok");
                Debug.WriteLine("Hay errores en los datos introducidos");
            }
        }

        bool validarBono(Bono b)
        {
            return (b.nombreDestino != null && b.apellidoDestino != null &&
                    Utils.IsValidCedula(b.cedulaDestino) &&
                    Utils.IsValidPhone(b.telefonoDestino) &&
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
                monedaSeleccionada = cbMoneda.SelectedItem as moneda;
                //Task<tasa> tasaTask = tasaManager.GetBySimbolo(mon.simbolo);
                //tasa = await tasaTask;
                tasaDia = monedaSeleccionada.tasas.First().valor;
                lbTasaDia.Text = TasaDia;
                montoRD = double.Parse(txtMonto.Text != null ? txtMonto.Text : "0.00") * tasaDia;
                lbMontoRD.Text = MontoRD;
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