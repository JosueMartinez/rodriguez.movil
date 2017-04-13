using System;
using System.Collections.Generic;

namespace rodriguez.Data
{
	public class listacompra
	{
		public int id { get; set; }
		public string nombre { get; set; }
		public int clienteId { get; set; }
		public DateTime fechaCreacion { get; set; }
		public DateTime fechaUltimaModificacion { get; set; }
		public cliente cliente { get; set; }
		public ICollection<listacompraproducto> productosLista { get; set; }

		public string FechaCreacion {
			get{
				return String.Format("Creada en: {0:dd/MMM/yyyy}", fechaCreacion);
			}
		}

		public string CantProductos{
			get{
				int count = 0;
				foreach(var p in productosLista){
					count++;
				}
				if(count > 1)
					return String.Format("{0} productos", count);
				return String.Format("{0} producto", count);
			}
		}
	}


}