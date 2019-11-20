﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using rodriguez.Classes;
using Xamarin.Forms;

namespace rodriguez.Data
{
    public class ComprasViewModel : BaseFodyObservable
    {
        public static listaCompraManager manager = new listaCompraManager();
        public ComprasViewModel()
        {
            //ListaCompraGrupos = GetListaCompraGrupos();
            GetListaCompraGrupos().ContinueWith(t =>
            {
                ListaCompraGrupos = t.Result;
            });
            Delete = new Command<ProductoCompra>(HandleDelete);
            CambioComprado = new Command<ProductoCompra>(HandleCambioComprado);
        }        

        public ILookup<string, ProductoCompra> ListaCompraGrupos { get; set; }
        public string Title => "Lista de Compras";
                
        private async Task<ILookup<string, ProductoCompra>> GetListaCompraGrupos()
        {
            return (await manager.GetList()).OrderBy(t => t.Comprado).ToLookup(t => t.Comprado ? "Comprado" : "Pendiente");
        }

        public Command<ProductoCompra> Delete { get; set; }
        public async void HandleDelete(ProductoCompra item)
        {
            await manager.DeleteProducto(item);
            //update displayed list
            ListaCompraGrupos = await GetListaCompraGrupos();
        }

        public Command<ProductoCompra> CambioComprado { get; set; }
        public async void HandleCambioComprado(ProductoCompra item)
        {
            await manager.ChangeProductoComprado(item);
            //update displayed list
            ListaCompraGrupos = await GetListaCompraGrupos();
        }
    }
}
