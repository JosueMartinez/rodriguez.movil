using System;

using Xamarin.Forms;

namespace rodriguez.UI
{
    public class Error : ContentPage
    {
        public Error()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Error" }
                }
            };
        }
    }
}

