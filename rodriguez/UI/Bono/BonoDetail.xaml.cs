using System;
using System.Collections.Generic;
using System.Diagnostics;
using rodriguez.Data;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class BonoDetail : ContentPage
	{
		public BonoDetail() { }   //Just for Previewing

		public BonoDetail(Bono bono)
		{
			InitializeComponent();
			inicializarComponentes(bono);
			//Setting toolbar
			//ToolbarItems.Add(new ToolbarItem("Go Back", null, () =>
			//{
			//	Debug.WriteLine("Go Back");
			//}));

		}

		private void inicializarComponentes(Bono b)
		{
			bonoIdDetail.Text = b.id.ToString();
			montoDetail.Text = b.Monto;
			emitidoADetail.Text = b.destinoCompleto;
			metodoPagoDetail.Text = "**** - 1756"; //TODO: obtener pago
			montoRdDetail.Text = 25.50.ToString();

		}
	}
}
