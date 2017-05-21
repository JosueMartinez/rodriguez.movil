using System;

using Xamarin.Forms;

namespace rodriguez.UI
{
    public class Success : ContentPage
    {
        public Success()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Exito" }
                }
            };
        }
    }
}

