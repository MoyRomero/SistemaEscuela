using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Rol
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public bool? Bhabilitado { get; set; }

    public string? Vista { get; set; }
}
