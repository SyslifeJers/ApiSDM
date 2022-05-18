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
            Presentacion = new HashSet<Presentacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modificado { get; set; }
        public int? Categoria { get; set; }

        public virtual Categoria CategoriaNavigation { get; set; }
        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Presentacion> Presentacion { get; set; }
    }
}
