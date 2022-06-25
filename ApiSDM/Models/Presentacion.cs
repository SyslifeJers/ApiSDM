using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Presentacion
    {
        public Presentacion()
        {
            Carrito = new HashSet<Carrito>();
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }
        public string Precentacion { get; set; }
        public string Medida { get; set; }
        public decimal Precio { get; set; }
        public int IdProducto { get; set; }

        public virtual Producto IdProductoNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
