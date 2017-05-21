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
        Bono b { get; set; }

        public PayPalClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.PayPalApiUrl);
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        // Get access (bearer) token from paypal
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

        // Make a payment
        // Should do validation of the returned data
        public async Task<PayPalExecutePaymentResult> MakePayment(Bono bono)
        {
            b = bono;
            try
            {
                var accessTokenData = await GetAccessToken();
                var executePaymentResult = new PayPalExecutePaymentResult();

                if (accessTokenData.errorCode == 0)
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "payments/payment");

                    // Add headers
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessTokenData.Access_Token);

                    // Add data to send
                    //request.RequestFormat = DataFormat.Json;
                    //var payment = new PayPalMakePaymentData()
                    //{
                    //    intent = "sale"
                    //};
                    //payment.payer = new PayPalPayer() { payment_method = "paypal" };
                    //var transaction = new PayPalTransaction()
                    //{
                    //amount = new PayPalAmount()
                    //{
                    //currency = "USD", //TODO from view
                    //total = "50", //TODO from view
                    //        details = new PayPalAmountDetails()
                    //        {
                    //            subtotal = "40",
                    //            tax = "5",
                    //            shipping = "5"
                    //        }
                    //    },
                    //    item_list = new PayPalItemList
                    //    {
                    //        items = new[] {
                    //                new PayPalItem {
                    //                    quantity = "1",
                    //                    name = "Bono Supermercado Rodriguez",
                    //                    price = "40",
                    //                    currency = "USD",   //TODO from view
                    //                    description = "Compra bono en Supermercado Rodriguez (Sabaneta)",
                    //                    tax = "5"
                    //                }
                    //            }
                    //    }
                    //};
                    //payment.transactions = new[] { transaction };



                    request.Content = new StringContent(
                        JsonConvert.SerializeObject(JObject.FromObject(new PayPalMakePaymentData
                        {
                            intent = "sale",
                            redirect_urls = new PayPalMakePaymentRedirectUrls
                            {
                                return_url = Constants.ReturnUrl,
                                cancel_url = Constants.CancelUrl
                            },
                            payer = new PayPalPayer
                            {
                                payment_method = "paypal"
                            },
                            transactions = new[] {
                        new PayPalTransaction {
                            amount = new PayPalAmount {
                                total = "50",
                                currency = "USD",
                                details = new PayPalAmountDetails {
                                    subtotal = "40",
                                    tax = "5",
                                    shipping = "5"
                                }
                            },
                            item_list = new PayPalItemList {
                                items = new [] {
                                    new PayPalItem {
                                        quantity = "1",
                                        name = "Booking reservation",
                                        price = "40",
                                        currency = "USD",
                                        description = "Booking reservation at ABC hotel at 24/03/2015 from 1pm to 4pm.",
                                        tax = "5"
                                    }
                                }
                            }
                        }
                    }
                        })),
                                        Encoding.UTF8, "application/json");
                    var response = new HttpResponseMessage();
                    try
                    {
                        response = await client.SendAsync(request);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }

                    //var response = restClient.Execute<PayPalGetTokenResponse>(restRequest);


                    //return response.Data;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<PayPalPaymentResponse>(content);
                        // Get the approval url from the links provided by the response
                        var links = from link in responseData.Links
                                    where link.Rel == "approval_url"
                                    select link.Href;

                        if (!String.IsNullOrEmpty(links.First()))
                        {
                            executePaymentResult.Url = links.First();
                            executePaymentResult.AccessToken = accessTokenData.Access_Token;

                            // Send the approval url along with the access token the paypal webview
                            return executePaymentResult;
                        }
                        else
                        {
                            executePaymentResult.DisplayError = "Something went wrong. Please try again.";
                        }
                    }


                }
                else
                {
                    executePaymentResult.DisplayError = accessTokenData.DisplayError;
                }

                return executePaymentResult;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }

        }

        // Validate a payment using webview for user to enter his credentials and to review the purchase
        public async Task<string> ExecuteApprovedPayment(string payerId, string accessToken, string paymentId)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, String.Format("payments/payment/{0}/execute", paymentId));

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var payer = JObject.FromObject(new { payer_id = payerId });
                request.Content = new StringContent(JsonConvert.SerializeObject(payer), Encoding.UTF8, "application/json");

                //var response = restClient.Execute<PayPalPaymentResponse>(restRequest);
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine("OK");
                    BonosManager bonoManager = new BonosManager();
                    await bonoManager.buyBono(b);
                    return "OK";
                }
                else
                {
                    Debug.WriteLine("Ha ocurrido un error " + response);
                    return "Ha ocurrido un error";
                }

                //return CheckResponseStatus(response, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Ha ocurrido un error " + e.ToString());
                return "Ha ocurrido un error " + e.ToString();
            }

        }



    }
}
