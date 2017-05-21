using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace rodriguez.Data
{
    public class moneda
    {

        public int id { get; set; }
        public string descripcion { get; set; }
        public string simbolo { get; set; }

        public Collection<tasa> tasas { get; set; }

    }
}