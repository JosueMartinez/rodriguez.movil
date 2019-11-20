using rodriguez.Classes;

namespace rodriguez.Data
{
    public class ProductoCompra : BaseFodyObservable
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Comprado { get; set; }
    }
}
