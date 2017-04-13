using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using rodriguez.Data;
using Newtonsoft.Json;

namespace rodriguez
{
	public class listaCompraManager
	{
		private string authorizationKey;

		private async Task<HttpClient> GetClient()
		{
			HttpClient client = new HttpClient();
			if (string.IsNullOrEmpty(authorizationKey))
			{
				authorizationKey = await client.GetStringAsync(Constants.baseUrl + "login");
				authorizationKey = JsonConvert.DeserializeObject<string>(authorizationKey);
			}

			client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
			client.DefaultRequestHeaders.Add("Accept", "application/json");
			return client;
		}

		public async Task<IEnumerable<listacompra>> GetAll(){
			HttpClient cliente = new HttpClient();
			var idCliente = 1;   //TODO : obtain logged user
			var response = await cliente.GetAsync(Constants.baseUrl + String.Format("cliente/{0}/listas", idCliente));
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var listas = JsonConvert.DeserializeObject<List<listacompra>>(content);
				return listas;
			}

			return null;
		}

	}
}
