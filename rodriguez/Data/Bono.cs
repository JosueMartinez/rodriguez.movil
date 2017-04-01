using System;
using System.Collections.Generic;

namespace rodriguez.Data
{
	public class Bono
	{
		public Bono()
		{
		}

		public int id { get; set; }
		public double monto { get; set; }
		public int monedaId { get; set; }
		public int clienteId { get; set; }
		public string nombreDestino { get; set; }
		public string apellidoDestino { get; set; }
		public string cedulaDestino { get; set; }
		public string telefonoDestino { get; set; }
		public DateTime fechaCompra { get; set; }
		public cliente cliente { get; set; }
		public moneda moneda { get; set; }
		public estadobono estadobono { get; set; }

		public ICollection<historialbono> historialbonoes { get; set; }

		#region custom
		public string nombreCompleto
		{
			get { return nombreDestino + " " + apellidoDestino; }
		}

		public string Monto
		{
			get { return moneda.simbolo + "$ " + monto; }
		}

		public string destinoCompleto
		{
			get
			{
				return nombreCompleto + "\n" +
					cedulaDestino + "\n" +  //TODO: Mask cedula
					telefonoDestino;  //TODO: Mask telefono
			}
			set
			{
				destinoCompleto = value;
			}
		}
		#endregion
	}
}
