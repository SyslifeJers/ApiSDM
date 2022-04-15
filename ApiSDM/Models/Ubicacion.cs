using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Ubicacion
    {
        public int Id { get; set; }
        public string Direccion { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public int? Activo { get; set; }
        public int ClienteId { get; set; }
        public string Nota { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}
