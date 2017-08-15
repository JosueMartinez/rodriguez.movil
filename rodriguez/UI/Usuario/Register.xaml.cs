using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using rodriguez.Data;
using Xamarin.Forms;

namespace rodriguez.UI.Usuario
{
    public partial class Register : ContentPage
    {
        private string authorizationKey;
        HttpClient client { get; set; }

        public Register()
        {
            InitializeComponent();

            GoToLogin.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Navigation.PopModalAsync();
                    })
                }
            );
        }

        async void crearCuenta(object sender, System.EventArgs e)
        {
            await isRunning((true));
            var c = new cliente()
            {
                usuario = Usuario.Text,
                nombreCompleto = NombreCompleto.Text,
                password = Contrasena.Text,
                confirmPassword = ConfirmarContrasena.Text,
                cedula = Cedula.Text,
                celular = celular.Text,
                email = Email.Text
            };

            if (validarCliente(c))
            {
                HttpClient httpClient = new HttpClient();
                var response = httpClient.PostAsync(Constants.baseUrl + "account/registerClient",
                                                    new StringContent(
                                                        JsonConvert.SerializeObject(c),
                                                        Encoding.UTF8, "application/json")).Result;

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Exito", "El usuario ha sido creado. Por favor inicie sesión", "Ok");

                }
                else
                {

                    await DisplayAlert("Error", "Ha ocurrido un error.  Por favor intente más tarde", "Ok");
                }

                await isRunning(false);
                await Navigation.PopModalAsync();

            }

            await isRunning(false);
        }

        private bool validarCliente(cliente c)
        {
            if (string.IsNullOrEmpty(c.usuario) || string.IsNullOrEmpty(c.nombreCompleto) ||
                string.IsNullOrEmpty(c.password) || string.IsNullOrEmpty(c.confirmPassword) ||
                string.IsNullOrEmpty(c.cedula) || string.IsNullOrEmpty(c.celular) ||
                string.IsNullOrEmpty(c.email))
            {
                DisplayAlert("Error", "Debe llenar todos los campos", "Ok");
                return false;
            }


            if (!c.password.Equals(c.confirmPassword))
            {
                DisplayAlert("Eror", "Las contraseñas no coinciden", "Ok");
                return false;
            }

            //TODO verificar cedula

            //TODO verificar telefono

            //TODO verificar email

            return true;
        }

        private Task isRunning(bool value)
        {
            ActivityIndicator.IsVisible = value;
            ActivityIndicator.IsRunning = value;
            return Task.FromResult<object>(null);
        }


    }
}
