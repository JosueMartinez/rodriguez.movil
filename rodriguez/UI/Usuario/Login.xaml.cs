using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace rodriguez.UI
{
    public partial class Login : ContentPage
    {
        private string authorizationKey;
        HttpClient client { get; set; }

        void iniciarSesion(object sender, System.EventArgs e)
        {
            var usuario = txtUsuario.Text;
            var contrasena = txtContraseña.Text;

            if (String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(contrasena))
            {
                DisplayAlert("Error", "Proporcione sus credenciales de acceso", "Ok");
            }

            HttpClient client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", usuario),
                new KeyValuePair<string, string> ( "password", contrasena )
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
                    Application.Current.Properties["IsLoggedIn"] = true;
                    Application.Current.Properties["token"] = authorizationKey;

                    App.Current.ShowMainPage();

                }
                else
                {
                    Debug.WriteLine(response.StatusCode);
                }

            }
        }

        public Login()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constants.baseUrl);
            InitializeComponent();
        }

    }
}