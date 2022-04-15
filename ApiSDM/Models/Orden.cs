using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Orden
    {
        public Orden()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }
        public decimal? Total { get; set; }
        public string Ordencol { get; set; }
        public string Notas { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Cantidad { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modifcado { get; set; }
        public decimal? CostoEnvio { get; set; }
        public int? Estatus { get; set; }
        public int TipoPago { get; set; }
        public int RutaId { get; set; }
        public int ClienteId { get; set; }
        public int? RepartidorId { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Repartidor Repartidor { get; set; }
        public virtual Ruta Ruta { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
