using System;
using System.Diagnostics.Contracts;
using rodriguez.Data;
namespace rodriguez
{
	public class tasa
	{

		public int id { get; set; }
		public int monedaId { get; set; }
		public double valor { get; set; }
		public DateTime fecha { get; set; }
		public moneda moneda { get; set; }
	}
}
