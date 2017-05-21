using System;
using Xamarin.Forms;
using System.Collections.Generic;
using XLabs.Ioc;
using rodriguez.Data.PayPal;
using rodriguez.UI;

namespace rodriguez
{

    public class PayPalWebView : ContentPage
    {

        WebView browser;
        string accessToken;

        public PayPalWebView(string url, string token)
        {
            accessToken = token;
            browser = new WebView();
            browser.Source = url;

            browser.Navigating += HandleNavigating;

            var layout = new ContentView();
            layout.Content = browser;

            Content = layout;
        }

        async void HandleNavigating(object sender, WebNavigationEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(e.Url))
            {
                // convert url to uri which gives us additional functionality and more flexability over the url
                var uri = new Uri(e.Url);

                if (uri.Host == Constants.ReturnHost)
                {
                    // prevent navigating to execute again and to come back here
                    browser.Navigating -= HandleNavigating;

                    // get the query string GET parameters in dictionary key-value format
                    var queryItems = GetQueryStringKeyValues(uri);

                    //string executeResonseError = null;
                    string result = "";

                    if (queryItems.ContainsKey("PayerID") && queryItems.ContainsKey("paymentId"))
                    {

                        // execute the approved payment
                        PayPalClient client = new PayPalClient();

                        result = await client.ExecuteApprovedPayment(queryItems["PayerID"], accessToken, queryItems["paymentId"]);
                        var i = result;
                        //executeResonseError = await Resolver
                        //.Resolve<PayPalClient>()
                        //.ExecuteApprovedPayment(queryItems["PayerID"], accessToken, queryItems["paymentId"]);
                    }

                    // check if the api call returned any errors
                    if (result != "OK")
                    {
                        // prevent navigating to execute again and to come back here
                        //browser.Navigating -= HandleNavigating;
                        // display executeResonseError
                        await Navigation.PushAsync(new Error());
                        // navigate back to the previous page
                        //await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        // prevent navigating to execute again and to come back here
                        //browser.Navigating -= HandleNavigating;
                        // display success
                        await Navigation.PushAsync(new Success());
                        // navigate back to the previous page
                        //await Navigation.PopToRootAsync();
                    }
                    // check if the webview is navigating to the cancel url -- user canceled the purchase
                }
                else if (uri.Host == Constants.CancelHost)
                {
                    // prevent navigating to execute again and to come back here
                    browser.Navigating -= HandleNavigating;
                    // navigate back to the previous page
                    await Navigation.PopModalAsync();
                }
            }
            else
            {
                // display error message
                await Navigation.PushAsync(new ComprasView());
                // navigate back to the previous page
                //await Navigation.PopToRootAsync();
            }
        }

        Dictionary<string, string> GetQueryStringKeyValues(Uri uri)
        {
            var queryItems = new Dictionary<string, string>();
            // remove the dollar sign (?) from the beginning
            var query = uri.Query.Substring(1);

            string[] itemArray;
            foreach (var item in query.Split('&'))
            {
                itemArray = item.Split('=');

                queryItems[itemArray[0]] = itemArray[1];
            }

            return queryItems;
        }
    }
}

