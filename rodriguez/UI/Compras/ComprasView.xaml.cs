using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;
using rodriguez.Data;
using System.Collections.ObjectModel;
using System.Linq;
using FormsPlugin.Iconize;

namespace rodriguez
{
    public partial class ComprasView : ContentPage
    {
        public ComprasView()
        {
            InitializeComponent();
            BindingContext = new ComprasViewModel();
        }
    }
}
