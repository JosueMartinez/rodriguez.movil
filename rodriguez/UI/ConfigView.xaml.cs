﻿using System;
using System.Diagnostics;
using FormsPlugin.Iconize;
using rodriguez.Data;
using Xamarin.Forms;

namespace rodriguez
{
    public partial class ConfigView : ContentPage
    {
        cliente cliente { get; set; }
        public ConfigView()
        {
            InitializeComponent();

            // color bonotes social
            fb.IconColor = Color.FromHex("#3C5798");
            twitter.IconColor = Color.FromHex("#2093E0");

            //gesture botones
            fb.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Device.OpenUri(new Uri("https://www.facebook.com/Supermercado-Rodriguez-561717717293234/"));
                    })
                }
            );

            twitter.GestureRecognizers.Add(
                new TapGestureRecognizer()
                {
                    Command = new Command(() =>
                    {
                        Device.OpenUri(new Uri("https://twitter.com/Sm_rodriguezs"));
                    })
                }
            );

            //geting info
            if (Application.Current.Properties.ContainsKey("cliente"))
            {
                getUserData();
                BindingContext = cliente;
            }

            //Toolbar Items
            ToolbarItems.Add(new IconToolbarItem
            {
                Icon = "fa-sign-out",
                IconColor = Color.White,
                Command = new Command(this.logout)
            });


        }

        public void getUserData()
        {

            cliente = (rodriguez.Data.cliente)Application.Current.Properties["cliente"];
        }

        void addBono(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AddBono());
        }

        void logout()
        {
            Debug.WriteLine("saliendo");
            Application.Current.Properties["IsLoggedIn"] = null;
            Application.Current.Properties["token"] = null;
            Application.Current.Properties["usuario"] = null;
            Application.Current.Properties["cliente"] = null;
            App.Current.Logout();
        }
    }


}
