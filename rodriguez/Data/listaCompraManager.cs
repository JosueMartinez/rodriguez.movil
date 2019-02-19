using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using rodriguez.Data;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace rodriguez
{
    public class listaCompraManager
    {
        private string authorizationKey;
        HttpClient client { get; set; }
        cliente cliente { get; set; }

        private HttpClient GetClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.baseUrl);
            if (Application.Current.Properties.ContainsKey("token"))
            {
                authorizationKey = Convert.ToString(Application.Current.Properties["token"]);//tokenDictionary["access_token"];
                cliente = Application.Current.Properties.ContainsKey("cliente") ? (rodriguez.Data.cliente)Application.Current.Properties["cliente"] : null;
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorizationKey);
                return client;
            }
            return null;


        }

        public async Task<ObservableCollection<listacompra>> GetAll()
        {
            try
            {
                if (client == null)
                    client = GetClient();

                if (client != null && cliente != null)
                {
                    var idCliente = cliente.ClienteId;
                    var url = String.Format("cliente/{0}/listas", idCliente);
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var listas = JsonConvert.DeserializeObject<ObservableCollection<listacompra>>(content);
                        return listas;
                    }
                }
                else
                {
                    return null;
                }


            }
            catch (HttpRequestException ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }

            return new ObservableCollection<listacompra>();
        }

    }
}
