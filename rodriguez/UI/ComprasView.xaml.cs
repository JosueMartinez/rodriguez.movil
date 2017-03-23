using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class ComprasView : ContentPage
	{
		public ComprasView()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Click", null, () =>
			{
				Debug.WriteLine("Clicked");
			}));
		}
	}
}
