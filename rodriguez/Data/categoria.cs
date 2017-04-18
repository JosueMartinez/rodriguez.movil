using System;
using System.Collections.Generic;

namespace rodriguez
{
	public class categoria
	{
		public categoria()
		{
		}

		public int id { get; set; }
		public string descripcion { get; set; }

		public List<producto> productos { get; set; }
	}
}
