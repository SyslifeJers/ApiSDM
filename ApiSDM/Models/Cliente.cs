using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Carrito = new HashSet<Carrito>();
            Orden = new HashSet<Orden>();
            Ubicacion = new HashSet<Ubicacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Contrasena { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modificado { get; set; }
        public string Token { get; set; }

        public virtual ICollection<Carrito> Carrito { get; set; }
        public virtual ICollection<Orden> Orden { get; set; }
        public virtual ICollection<Ubicacion> Ubicacion { get; set; }
    }
}
