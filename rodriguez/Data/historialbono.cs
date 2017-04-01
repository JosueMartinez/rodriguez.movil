using System;

namespace rodriguez.Data
{
	public class historialbono
	{
		public int id { get; set; }
		public int bonoId { get; set; }
		public int estadoBonoId { get; set; }
		public DateTime fechaEntradaEstado { get; set; }
		public DateTime? fechaSalidaEstado { get; set; }
		public virtual Bono bono { get; set; }
		public virtual estadobono estadobono { get; set; }
	}
}