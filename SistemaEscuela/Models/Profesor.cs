using System;
using System.Collections.Generic;

namespace SistemaEscuela.Models;

public partial class Profesor
{
    public int Id { get; set; }

    public int? IdRol { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public bool? Bhabilitado { get; set; }
}
