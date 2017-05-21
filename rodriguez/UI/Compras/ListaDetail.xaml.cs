using System;
using System.Diagnostics;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using rodriguez.Data;

namespace rodriguez
{
	public partial class ListaDetail : ContentPage
	{

		List<categoria> categoriasLista;

		public class SectionCategorias
		{
			public string Title { get; set; }
			public IEnumerable<producto> List { get; set; }
		}

		public class ViewModelProd
		{
			public IEnumerable<SectionCategorias> List { get; set; }
		}

		public ListaDetail()
		{
			InitializeComponent();
		}

		public ListaDetail(listacompra lista)
		{
			InitializeComponent();
			BindingContext = lista;

			organizeCategories(lista.productosLista);

			var template = new DataTemplate(typeof(ProductosTemplate));
			var view = new AccordionView(template);
			view.SetBinding(AccordionView.ItemsSourceProperty, "List");
			view.Template.SetBinding(AccordionSectionView.TitleProperty, "Title");
			view.Template.SetBinding(AccordionSectionView.ItemsSourceProperty, "List");

			List<SectionCategorias> seccionesCategorias = new List<SectionCategorias>();
			foreach (var c in categoriasLista)
			{
				var n = new SectionCategorias()
				{
					Title = c.descripcion,
					List = c.productos
				};
				seccionesCategorias.Add(n);
			}


			view.BindingContext = new ViewModelProd
			{
				List = seccionesCategorias
			};

			this.Content = view;

		}

		public void organizeCategories(ICollection<listacompraproducto> listado){
			
			categoriasLista = new List<categoria>();

			foreach(var l in listado){
				//var productosCategoria = new List<producto>();
				var categoria = l.producto.categoria;
				//categoria
				if(!categoriasLista.Contains(categoria)){
					categoriasLista.Add(categoria);
				}
			}
		}
	}
}
