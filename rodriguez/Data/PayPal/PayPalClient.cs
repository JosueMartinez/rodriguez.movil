
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace rodriguez.Data.PayPal
{
    public class PayPalClient
    {
        HttpClient client { get; set; }

        public PayPalClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.PayPalApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<PayPalGetTokenResponse> GetAccessToken()
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "oauth2/token");    //the beginning slash is added to the baseUrl because this method takes it as file if        

                var byteArray = Encoding.GetEncoding("iso-8859-1").GetBytes(String.Format("{0}:{1}", Constants.PayPalClientId, Constants.PayPalSecret));

                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                // add data to send       
                var formData = new Dictionary<string, string>
                {
                    ["grant_type"] = "client_credentials"
                };
                request.Content = new FormUrlEncodedContent(formData);

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<PayPalGetTokenResponse>(content);
                    return token;
                }


                return new PayPalGetTokenResponse() { errorCode = (int)response.StatusCode, DisplayError = "Ha ocurrido un error" };

            }
            catch (Exception e)
            {
                Debug.WriteLine("Hay errores en los datos introducidos");
                return null;
            }
        }

        public async Task<PayPalPayment> getPayment(string paymentID)
        {
            try
            {
                var accessTokenData = await GetAccessToken();
                var payment = new PayPalPayment();

                if (accessTokenData.errorCode == 0)
                {

                    var request = new HttpRequestMessage(HttpMethod.Get, "payments/payment/" + paymentID);

                    // Add headers        
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenData.Access_Token);
                    var response = new HttpResponseMessage();
                    try
                    {
                        response = await client.SendAsync(request);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        payment = JsonConvert.DeserializeObject<PayPalPayment>(content);
                        return payment;
                    }

                }
            }
            catch (Exception e)
            {
                return null;
            }

            return null;
        }
    }
}
