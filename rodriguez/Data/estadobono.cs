using System.Collections.Generic;

namespace rodriguez.Data
{
	public class estadobono
	{
		public int id { get; set; }
		public string descripcion { get; set; }
		public virtual ICollection<Bono> bonos { get; set; }
		public virtual ICollection<historialbono> historial { get; set; }
	}
}