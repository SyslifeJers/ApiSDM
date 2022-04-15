using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Carrito = new HashSet<Carrito>();
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
