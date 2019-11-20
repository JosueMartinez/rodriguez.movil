using rodriguez.Classes;
using SQLite;

namespace rodriguez.Data
{
    public class ProductoCompra : BaseFodyObservable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Comprado { get; set; }
    }
}
