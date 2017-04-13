using System;
using rodriguez.Data;

namespace rodriguez
{
	public class listacompraproducto
	{
		public listacompraproducto()
		{
			
		}

		public int id { get; set; }
		public int listaCompraId { get; set; }
		public int productoId { get; set; }
		public int cantidad { get; set; }
		public listacompra listaCompra { get; set; }
		public producto producto { get; set; }

	}
}
