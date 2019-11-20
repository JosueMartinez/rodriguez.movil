using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using rodriguez.Classes;
using Xamarin.Forms;

namespace rodriguez.Data
{
    public class ComprasViewModel : BaseFodyObservable
    {
        public ComprasViewModel()
        {
            ListaCompraGrupos = GetListaCompraGrupos();
            Delete = new Command<ProductoCompra>(HandleDelete);
            CambioComprado = new Command<ProductoCompra>(HandleCambioComprado);
        }        

        public ILookup<string, ProductoCompra> ListaCompraGrupos { get; set; }
        public string Title => "Lista de Compras";

        private List<ProductoCompra> _listaCompra { get; set; } = new List<ProductoCompra>
        {
            new ProductoCompra {Id = 0, Nombre = "Arroz", Comprado = false},
            new ProductoCompra {Id = 1, Nombre = "Habichuelas Rojas", Comprado = false},
            new ProductoCompra {Id = 2, Nombre = "Carne", Comprado = true}
        };

        private ILookup<string, ProductoCompra> GetListaCompraGrupos()
        {
            return _listaCompra.OrderBy(t => t.Comprado).ToLookup(t => t.Comprado ? "Comprado" : "Pendiente");
        }

        public Command<ProductoCompra> Delete { get; set; }
        public void HandleDelete(ProductoCompra item)
        {
            //Remove Item from private list
            _listaCompra.Remove(item);
            //update displayed list
            ListaCompraGrupos = GetListaCompraGrupos();
        }

        public Command<ProductoCompra> CambioComprado { get; set; }
        public void HandleCambioComprado(ProductoCompra item)
        {
            //change item flag
            item.Comprado = !item.Comprado;
            //update displayed list
            ListaCompraGrupos = GetListaCompraGrupos();
        }
    }
}
