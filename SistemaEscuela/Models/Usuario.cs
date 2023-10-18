using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public string? Email { get; set; }

    public string? Pass { get; set; }

    public bool? Bhabilitado { get; set; }
}
