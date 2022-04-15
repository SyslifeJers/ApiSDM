using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class DetalleOrden
    {
        public int IdDetalleOrden { get; set; }
        public int? Cantidad { get; set; }
        public string Nota { get; set; }
        public decimal? Subtotal { get; set; }
        public int OrdenId { get; set; }
        public int? ProductoId { get; set; }
        public int? ProductoDelDiaId { get; set; }

        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
