using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using rodriguez.Data;
using XLabs.Forms.Controls;
using System.Text;

namespace rodriguez
{
	public class TasaManager
	{
		//Obtener todos los tasas
		const string Url = "http://smrodriguez.azurewebsites.net/api/tasas";
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

		public async Task<tasa> GetByID(int id)
		{
			HttpClient cliente = new HttpClient();
			var response = await cliente.GetAsync(Constants.baseUrl + String.Format("monedas/{0}/tasa", id));
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<tasa>(
								await response.Content.ReadAsStringAsync());
			}

			return null;
		}

		public async Task<tasa> GetBySimbolo(string simbolo)
		{
			HttpClient cliente = new HttpClient();
			var response = await cliente.GetAsync(Constants.baseUrl + String.Format("monedas/{0}/tasa", simbolo));
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<tasa>(
								await response.Content.ReadAsStringAsync());
			}

			return null;
		}

	}
}
