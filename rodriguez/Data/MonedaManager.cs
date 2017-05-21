using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace rodriguez.Data
{
    public class MonedaManager
    {
        private string authorizationKey;

        public MonedaManager()
        {
        }

        public async Task<IEnumerable<moneda>> GetAll()
        {
            HttpClient cliente = new HttpClient();
            var response = await cliente.GetAsync(Constants.baseUrl + "monedas");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var monedas = JsonConvert.DeserializeObject<List<moneda>>(content);
                return monedas;
            }

            return null;
        }

        public async Task<moneda> GetByID(int id)
        {
            HttpClient cliente = new HttpClient();
            var response = await cliente.GetAsync(Constants.baseUrl + String.Format("monedas/{0}", id));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<moneda>(content);
            }

            return null;
        }
    }
}
