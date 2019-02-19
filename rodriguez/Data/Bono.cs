using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace rodriguez.Data
{
    public class Bono
    {
        public Bono()
        {
        }

        public int id { get; set; }
        public double Monto { get; set; }
        public int tasaId { get; set; }
        public int clienteId { get; set; }
        public string nombreDestino { get; set; }
        public string apellidoDestino { get; set; }
        public string cedulaDestino { get; set; }
        public string telefonoDestino { get; set; }
        public DateTime fechaCompra { get; set; }
        public cliente cliente { get; set; }
        public tasa tasa { get; set; }
        public estadobono estadobono { get; set; }

        public string paypalId { get; set; }

        public ICollection<historialbono> historialbonoes { get; set; }

        #region custom
        public string nombreCompleto
        {
            get { return nombreDestino + " " + apellidoDestino; }
        }

        [JsonIgnoreAttribute]
        public string MontoStr
        {
            get { return String.Format("{0}$ {1:##,##0.00}", tasa.moneda.simbolo, Monto); }
        }

        [JsonIgnoreAttribute]
        public string MontoRD
        {
            get { return String.Format("RD$ {0:##,##0.00}", Monto * tasa.valor); }
        }

        [JsonIgnoreAttribute]
        public string destinoCompleto
        {
            get
            {
                return nombreCompleto + "\n" +
                    Utils.formatCedula(cedulaDestino) + "\n" +
                         Utils.formaTel(telefonoDestino);

            }
            set
            {
                destinoCompleto = value;
            }
        }

        [JsonIgnoreAttribute]
        public string MetodoPago
        {
            get
            {
                return "**** - 1756"; //TODO: obtener pago
            }
        }

        [JsonIgnoreAttribute]
        public string Estado
        {
            get { return estadobono.descripcion; }
        }

        #endregion
    }
}
