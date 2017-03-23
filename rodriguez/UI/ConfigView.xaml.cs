using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace rodriguez
{
	public partial class ConfigView : ContentPage
	{
		public ConfigView()
		{
			InitializeComponent();
			ToolbarItems.Add(new ToolbarItem("Click", null, () =>
			{
				Debug.WriteLine("Clicked");
			}));
		}
	}
}
