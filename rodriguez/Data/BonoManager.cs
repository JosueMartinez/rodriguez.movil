using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rodriguez
{
	public class BonosManager
	{
		//Obtener todos los bonos
		public async Task<IEnumerable<Bono>> GetAll()
		{
			var Url = Constants.baseUrl + "cliente/1/bonos";
			HttpClient cliente = new HttpClient();
			var bonos = await cliente.GetStringAsync(Url);
			return JsonConvert.DeserializeObject<List<Bono>>(bonos);
		}
	}
}
