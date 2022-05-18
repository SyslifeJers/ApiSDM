﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ApiSDM.Models
{
    public partial class Adminitrador
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public string Token { get; set; }
        public DateTime Registro { get; set; }
    }
}
