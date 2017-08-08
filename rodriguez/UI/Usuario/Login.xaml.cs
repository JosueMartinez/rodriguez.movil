using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using rodriguez.Data;
using rodriguez.UI.Usuario;
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
            var contrasena = txtContrasena.Text;

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
                    var clienteResponse = client.GetAsync(Constants.baseUrl + "clienteU/" + usuario).Result;
                    var clienteContent = clienteResponse.Content.ReadAsStringAsync().Result;
                    var cliente = JsonConvert.DeserializeObject<cliente>(clienteContent);

                    // Deserialize the JSON into a Dictionary<string, string>
                    Dictionary<string, string> tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    authorizationKey = tokenDictionary["access_token"];
                    Application.Current.Properties["IsLoggedIn"] = true;
                    Application.Current.Properties["token"] = authorizationKey;
                    Application.Current.Properties["usuario"] = usuario;
                    Application.Current.Properties["cliente"] = cliente;
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

            newAccountLabel.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PushModalAsync(new Register());
                    })
                }
            );

            forgottenLabel.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PushModalAsync(new PasswordRecover());
                    })
                }
            );
        }

    }
}