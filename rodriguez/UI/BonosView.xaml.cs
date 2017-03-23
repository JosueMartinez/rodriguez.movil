using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class BonosView : ContentPage
	{
		public BonosView()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Click", null, () =>
			{
				Debug.WriteLine("Clicked");
			}));
		}
	}
}
