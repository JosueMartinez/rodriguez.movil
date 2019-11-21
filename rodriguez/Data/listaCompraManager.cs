using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using rodriguez.Data;
using SQLite;
using Xamarin.Forms;

namespace rodriguez
{
    public class listaCompraManager
    {
        private readonly SQLiteAsyncConnection _db;

        public listaCompraManager()
        {
            _db = new SQLiteAsyncConnection(DependencyService.Get<IFileHelper>().GetLocalFilePath("RodriguezSQLite.db3")); 
            _db.CreateTableAsync<ProductoCompra>().Wait();
        }

        private List<ProductoCompra> _listaCompra { get; set; } = new List<ProductoCompra>
        {
            new ProductoCompra {Id = 0, Nombre = "Arroz", Comprado = false},
            new ProductoCompra {Id = 1, Nombre = "Habichuelas Rojas", Comprado = false},
            new ProductoCompra {Id = 2, Nombre = "Carne", Comprado = true}
        };

        public async Task<List<ProductoCompra>> GetList()
        {

            if(await _db.Table<ProductoCompra>().CountAsync() == 0)
            {
                await _db.InsertAllAsync(_listaCompra);
            }

            return await _db.Table<ProductoCompra>().ToListAsync();
        }

        public Task DeleteProducto(ProductoCompra item)
        {
            return _db.DeleteAsync(item);
        }

        public Task ChangeProductoComprado(ProductoCompra item)
        {
            item.Comprado = !item.Comprado;
            return _db.UpdateAsync(item);
        }

        public Task AgregarProducto(ProductoCompra item)
        {
            return _db.InsertAsync(item);
        }
    }
}
