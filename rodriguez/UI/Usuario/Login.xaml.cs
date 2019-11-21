using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Threading.Tasks;
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


        async void iniciarSesion(object sender, System.EventArgs e)
        {

            await isRunning(true);
            var usuario = txtUsuario.Text;
            var contrasena = txtContrasena.Text;

            if (String.IsNullOrEmpty(usuario) || String.IsNullOrEmpty(contrasena))
            {
                await DisplayAlert("Error", "Proporcione sus credenciales de acceso", "Ok");
                await isRunning(false);
                return;

            }

            client = new HttpClient();
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", usuario),
                new KeyValuePair<string, string> ( "password", contrasena )
            };
            var content = new FormUrlEncodedContent(pairs);

            if (string.IsNullOrEmpty(authorizationKey))
            {
                try
                {
                    var response = await client.PostAsync(Constants.baseUrl + "token", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        var clienteResponse = client.GetAsync(Constants.baseUrl + "clienteU/" + usuario).Result;
                        var clienteContent = clienteResponse.Content.ReadAsStringAsync().Result;
                        var cliente = JsonConvert.DeserializeObject<cliente>(clienteContent);

                        if (cliente == null)
                        {  //no es un cliente
                            await DisplayAlert("Error", "Este cliente no existe", "Ok");
                            txtUsuario.Text = ""; txtContrasena.Text = "";   //limpiando campos
                            await isRunning(false);
                            return;
                        }

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
                        await DisplayAlert("Error", "Usuario y/o contraseña incorrecta", "Ok");
                        Debug.WriteLine(response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Ha ocurrido un error.  Por favor intente más tarde", "Ok");
                }
                

            }

            await isRunning(false);
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



        private Task isRunning(bool value)
        {
            ActivityIndicator.IsVisible = value;
            ActivityIndicator.IsRunning = value;
            return Task.FromResult<object>(null);
        }


    }
}