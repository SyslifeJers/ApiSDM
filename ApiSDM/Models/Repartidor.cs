using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Repartidor
    {
        public Repartidor()
        {
            Orden = new HashSet<Orden>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Rango { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int? Activo { get; set; }
        public DateTime? Modificado { get; set; }

        public virtual ICollection<Orden> Orden { get; set; }
    }
}
