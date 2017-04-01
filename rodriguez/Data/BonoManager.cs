using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using rodriguez.Data;

namespace rodriguez
{
	public class BonosManager
	{
		//Obtener todos los bonos
		const string Url = "http://smrodriguez.azurewebsites.net/api/bonos";
		private string authorizationKey;

		private async Task<HttpClient> GetClient()
		{
			HttpClient client = new HttpClient();
			if (string.IsNullOrEmpty(authorizationKey))
			{
				authorizationKey = await client.GetStringAsync(Url + "login");
				authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
			}

			client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			return client;
		}


		public async Task<IEnumerable<Bono>> GetAll()
		{
			HttpClient cliente = new HttpClient();
			var idCliente = 1;   //TODO : obtain logged user
			var response = await cliente.GetAsync(Constants.baseUrl + String.Format("cliente/{0}/bonos", idCliente));
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var bonos = JsonConvert.DeserializeObject<List<Bono>>(content);
				return bonos;
			}

			return null;
		}
	}
}
