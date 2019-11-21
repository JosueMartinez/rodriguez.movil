using System;
using System.Collections.Generic;
using rodriguez.Data;
using Xamarin.Forms;

namespace rodriguez.UI.Compras
{
    public partial class AgregarProducto : ContentPage
    {
        public AgregarProducto()
        {
            InitializeComponent();
            BindingContext = new AgregarProductoViewModel(Navigation);
        }
    }
}
