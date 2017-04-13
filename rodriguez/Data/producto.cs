using System;
namespace rodriguez
{
	public class producto
	{
		public producto()
		{
		}

		public int id { get; set; }
		public string nombre { get; set; }
		public int categoriaId { get; set; }
		public categoria categoria { get; set; }
	}
}
