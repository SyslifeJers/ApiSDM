using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Ruta
    {
        public Ruta()
        {
            Orden = new HashSet<Orden>();
        }

        public int Id { get; set; }
        public decimal? Lat { get; set; }
        public decimal? Lon { get; set; }
        public string Google { get; set; }
        public string Direccion { get; set; }

        public virtual ICollection<Orden> Orden { get; set; }
    }
}
