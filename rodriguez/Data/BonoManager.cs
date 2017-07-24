using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Diagnostics;
using rodriguez.Data;
//using XLabs.Forms.Controls;
using System.Text;
using rodriguez.UI;
using System.Collections;
using System.Linq;

namespace rodriguez
{
    public class BonosManager
    {
        //Obtener todos los bonos
        //const string Url = "http://smrodriguez.azurewebsites.net/api/bonos";
        private string authorizationKey;
        HttpClient client { get; set; }

        public BonosManager()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.baseUrl);
        }

        private async Task<HttpClient> GetClient()
        {
            //HttpClient client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", "tester"),
                new KeyValuePair<string, string> ( "password", "tester123" )
            };
            var content = new FormUrlEncodedContent(pairs);

            if (string.IsNullOrEmpty(authorizationKey))
            {
                var response = client.PostAsync(Constants.baseUrl + "token", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    // Deserialize the JSON into a Dictionary<string, string>
                    Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    authorizationKey = tokenDictionary["access_token"];
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorizationKey);
                    return client;
                }
                else
                {
                    return null;
                }

            }

            return client;
        }


        public async Task<IEnumerable<Bono>> GetAll()
        {
            try
            {
                client = await GetClient();
                if (client != null)
                {
                    var idCliente = 1;   //TODO : obtain logged user
                    var url = String.Format("cliente/{0}/bonos", idCliente);
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var bonos = JsonConvert.DeserializeObject<List<Bono>>(content);
                        return bonos;
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

            return Enumerable.Empty<Bono>();

        }

        public async Task<Bono> buyBono(Bono b)
        {
            try
            {

                //HttpClient cliente = new HttpClient(
                var response = await client.PostAsync(Constants.baseUrl + "bonos",
                                new StringContent(
                                    JsonConvert.SerializeObject(b),
                                    Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<Bono>(
                        await response.Content.ReadAsStringAsync());
                }
            }
            catch (HttpRequestException e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }

            return null;

        }
    }
}
